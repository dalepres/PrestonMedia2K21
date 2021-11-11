using System;
using System.IO;
using System.Text;

namespace Preston.Media
{
    /// <summary>
    /// Summary description for Logger.
    /// </summary>
    public class Logger
    {
        public Logger()
        {
        }

        public static void WriteLine()
        {
            WriteLine("");
        }

        public static void WriteLine(string message)
        {
            try
            {
                string appDataFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
                string copFolder = Path.Combine(appDataFolder, "Preston.Media");
                if (!Directory.Exists(copFolder))
                {
                    Directory.CreateDirectory(copFolder);
                }

                string logFolder = Path.Combine(copFolder, "MetadataBackup");
                if (!Directory.Exists(logFolder))
                {
                    Directory.CreateDirectory(logFolder);
                }

                string logFile = Path.Combine(logFolder, "MetadataBackup.log");

                FileStream fs;
                if (!File.Exists(logFile))
                {
                    fs = CreateNewLog(logFile);
                }
                else
                {
                    fs = OpenLog(logFile);
                }

                string fullMessage = string.Empty;

                if (message.Length > 0)
                {
                    fullMessage = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ": " + message;
                    Console.WriteLine(message);
                }

                byte[] msgBytes = Encoding.UTF8.GetBytes(fullMessage + System.Environment.NewLine);

                fs.Write(msgBytes, 0, msgBytes.Length);
                fs.Close();
            }
            catch { }
        }

        private static FileStream CreateNewLog(string logFile)
        {
            FileStream fs = fs = File.Open(logFile, FileMode.Append, FileAccess.Write, FileShare.Read);
            string openMessage = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ": "
                + "Preston.Media MetadataBackup - New log file created.\r\n\r\n"
                + "If this log file gets too large, you can delete old entries or delete the entire log file.  If you delete the file, a new log file will be created the next time the application is run.";

            byte[] msgBytes = Encoding.UTF8.GetBytes(openMessage + System.Environment.NewLine + System.Environment.NewLine);
            fs.Write(msgBytes, 0, msgBytes.Length);

            return fs;
        }

        private static FileStream OpenLog(string logFile)
        {
            return File.Open(logFile, FileMode.Append, FileAccess.Write, FileShare.Read);
        }

        public static void LogError(string message, Exception ex, bool writeStackTrace)
        {
            WriteLine((message.Length > 0
                ? message + ": " : "")
                + ex.Message
                + (writeStackTrace
                ? (Environment.NewLine + "StackTrace: " + ex.StackTrace)
                : ""));
        }

        public static void SetLogfileSize(string logFile)
        {
            try
            {
                if (File.Exists(logFile))
                {
                }
            }
            catch
            {
            }
        }
    }
}
