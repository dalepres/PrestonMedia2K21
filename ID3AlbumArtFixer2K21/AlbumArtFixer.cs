using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using ID3Lib;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Security.AccessControl;
using System.Security.Principal;
using ID3AlbumArtFixer.Properties;
using System.Windows.Forms;
using System.ComponentModel;

namespace ID3AlbumArtFixer
{
    internal static class AlbumArtFixer
    {
        private static Settings settings = new Settings();
        private static byte[] thumbnail;
        private static int foldersProcessed = 0;
        private static ImageCodecInfo codecInfo;
        private static EncoderParameters encoderParams;

        internal static void UpdateAlbumArt(
            string path, 
            bool includeSubfolders, 
            Size maxSize, 
            BackgroundWorker worker, 
            DoWorkEventArgs e)
        {
            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            AlbumArtFixerJob job = (AlbumArtFixerJob)e.Argument;
            if (job.IsStarting)
            {
                job.IsStarting = false;
                foldersProcessed = 0;
            }

            if (includeSubfolders)
            {
                string[] childFolders = Directory.GetDirectories(path);
                foreach (string folder in childFolders)
                {
                    UpdateAlbumArt(folder, includeSubfolders, maxSize, worker, e);
                }
            }

            try
            {
                using (Image newAlbumArt = GetNewAlbumArt(path, job.AlbumArtSourceFileName), resizedAlbumArt = ResizeAlbumArt(newAlbumArt, job.MaxSize))
                {
                    if (newAlbumArt != null)
                    {
                        thumbnail = GetAlbumArtThumbnailBytes(newAlbumArt);
                        worker.ReportProgress(((int)foldersProcessed / job.FolderCount), thumbnail);

                        if (resizedAlbumArt != null && job.CreateAlbumArt)
                        {
                            StoreFolderAlbumArt(path, resizedAlbumArt, job);
                        }
                        else if (job.SetAlbumArtSecurity)
                        {
                            SetWorkingACL(Path.Combine(path, "Folder.jpg"), job.FullControlAccount, job.ReadOnlyAccount);
                        }

                        if (job.EmbedAlbumArt && job.EmbedPictureJob != null)
                        {
                            EmbedFolderAlbumArt(path, newAlbumArt, job);
                        }
                    }
                    else
                    {
                        GC.Collect();
                    }
                }
            }
            catch (Exception ex)
            {
            }

            // TODO:  Figure out the best way to report progress
            //// Report progress as a percentage of the total task.
            //int percentComplete =
            //    (int)((float)n / (float)numberToCompute * 100);
            //if (percentComplete > highestPercentageReached)
            //{
            //    highestPercentageReached = percentComplete;
            //    worker.ReportProgress(percentComplete);
            //}
            // UNTIL THEN:
            worker.ReportProgress(((int)foldersProcessed++/job.FolderCount), path);

            return; // return some value in the future for e.Result
        }

        private static byte[] GetAlbumArtThumbnailBytes(Image newAlbumArt)
        {
            Image thumbImage = APICFrame.ResizePicture(newAlbumArt, new Size(100, 100));
            MemoryStream ms = new MemoryStream();
            thumbImage.Save(ms, ImageFormat.Jpeg);
            thumbImage.Dispose();
            return ms.ToArray();
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();

            for(int count = 0; count < encoders.Length; count++)
            {
                if (encoders[count].MimeType == mimeType)
                {
                    return encoders[count];
                }
            }

            return null;
        }

        private static void StoreFolderAlbumArt(
            string path,
            Image resizedAlbumArt,
            AlbumArtFixerJob job)
        {
            FileSecurity originalSecurity = null;
            string folderImage = Path.Combine(path, "Folder.jpg");

            if (job.CreateAlbumArt && resizedAlbumArt != null)
            {
                if (File.Exists(folderImage))
                {
                    originalSecurity = File.GetAccessControl(folderImage, AccessControlSections.All);
                }

                SetWorkingACL(folderImage, "Everyone", "");
                BackupOldAlbumArt(path, folderImage);

                if (codecInfo == null)
                {
                    codecInfo = GetEncoderInfo("image/jpeg");
                }

                if (encoderParams == null)
                {
                    encoderParams = new EncoderParameters(1);
                    encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, job.ImageQuality);
                }

                resizedAlbumArt.Save(folderImage, codecInfo, encoderParams); 
            }

            if (job.SetAlbumArtSecurity)
            {
                SetWorkingACL(folderImage, job.FullControlAccount, job.ReadOnlyAccount, originalSecurity);
            }
            else if (originalSecurity != null)
            {
                File.SetAccessControl(folderImage, originalSecurity);
            }

            BackupNewAlbumArt(path, job, resizedAlbumArt, codecInfo, encoderParams);
        }

        private static void BackupNewAlbumArt(string path, AlbumArtFixerJob job, Image resizedAlbumArt, ImageCodecInfo codecInfo, EncoderParameters encoderParams)
        {
            string folderImage1000 = Path.Combine(path, "Folder"
                + Math.Max(job.MaxSize.Width, job.MaxSize.Height).ToString()
                + ".jpg");
            try
            {
                if (File.Exists(folderImage1000))
                {
                    File.Delete(folderImage1000);
                }
                resizedAlbumArt.Save(folderImage1000, codecInfo, encoderParams);
            }
            finally
            {
            }
        }

        private static void SetWorkingACL(string folderImage, string fullControlAccount, string readOnlyAccount)
        {
            SetWorkingACL(folderImage, fullControlAccount, readOnlyAccount, null);
        }

        private static void SetWorkingACL(string folderImage, string fullControlAccount, string readOnlyAccount, FileSecurity originalSecurity)
        {
            try
            {
                if (fullControlAccount.Trim().Length == 0)
                {
                    return;
                }
                if (!File.Exists(folderImage))
                {
                    return;
                }

                FileSecurity fs = File.GetAccessControl(folderImage);
                fs.SetAccessRuleProtection(true, false);
                AuthorizationRuleCollection rules = fs.GetAccessRules(true, true, typeof(NTAccount));

                foreach (AuthorizationRule rule in rules)
                {
                    if (rule is AccessRule)
                    {
                        fs.RemoveAccessRule((FileSystemAccessRule)rule);
                    }
                }

                fs.AddAccessRule(new FileSystemAccessRule(fullControlAccount, FileSystemRights.FullControl, AccessControlType.Allow));

                if (readOnlyAccount.Trim().Length > 0)
                {
                    fs.AddAccessRule(new FileSystemAccessRule(readOnlyAccount, FileSystemRights.ReadAndExecute, AccessControlType.Allow));
                }

                File.SetAccessControl(folderImage, fs);
            }
            catch (Exception ex)
            {
                try
                {
                    if (originalSecurity != null)
                    {
                        File.SetAccessControl(folderImage, originalSecurity);
                    }
                }
                finally { }
            }
        }

        private static void BackupOldAlbumArt(string path, string folderImage)
        {
            if (File.Exists(folderImage))
            {
                string backup = Path.Combine(path, "Folder_Bak_" + DateTime.Now.ToString("dd-MMM-yyyy_HH-mm-ss.fff") + ".jpg");
                File.Move(folderImage, backup);
            }
        }

        private static void EmbedFolderAlbumArt(string path, Image resizedAlbumArt, AlbumArtFixerJob job)
        {
            Mp3File.EmbedFolderImage(
                path, 
                job.EmbedPictureJob.ImageFileName, 
                "*.mp3", 
                job.EmbedPictureJob.Id3ImageType, 
                "image/jpeg", 
                job.EmbedPictureJob.ImageDescription, 
                false, 
                job.EmbedPictureJob.MaxSize,
                job.EmbedPictureJob.DeleteExistingImages);
        }

        private static Image ResizeAlbumArt(Image albumArt, Size maxSize)
        {
            if (albumArt == null)
            {
                return null;
            }

            if (albumArt.Width > maxSize.Width || albumArt.Height > maxSize.Height)
            {
                return APICFrame.ResizePicture(albumArt, maxSize);
            }
            else
            {
                return (Image)albumArt.Clone();
            }
        }

        private static Image GetNewAlbumArt(string path, string albumArtSourceFileName)
        {
            string[] fileNames = Directory.GetFiles(path, "*.jpg", SearchOption.TopDirectoryOnly);
            if (fileNames == null || fileNames.Length == 0)
            {
                return null;
            }

            int bigSize = 0;
            Bitmap bmp = null;


            if (!string.IsNullOrEmpty(albumArtSourceFileName) && File.Exists(Path.Combine(path, albumArtSourceFileName)))
            {
                bmp = GetImagFromFileName(Path.Combine(path, albumArtSourceFileName));
            }

            if (bmp == null) 
            {
                for (int count = 0; count < fileNames.Length; count++)
                {
                    string fileName = fileNames[count];

                    try
                    {
                        Image image = Image.FromFile(fileName);
                        int imageBigSize = Math.Max(image.Width, image.Height);
                        if (imageBigSize > bigSize)
                        {
                            bigSize = imageBigSize;
                            bmp = new Bitmap(image);
                            image.Dispose();
                        }
                        else
                        {
                            image.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return bmp;

         }

        public static Bitmap GetImagFromFileName(string albumArtSourceFileName)
        {
            Bitmap bmp = null;
            Image image = null;
            {
                try
                {
                    image = Image.FromFile(albumArtSourceFileName);
                    bmp = new Bitmap(image);
                }
                catch
                {
                    bmp = null;
                }
                finally
                {
                    if (image != null) { image.Dispose(); }
                }
            }

            return bmp;
        }

    }

}
