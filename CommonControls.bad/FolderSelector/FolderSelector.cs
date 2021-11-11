using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace CommonControls
{
    public delegate void FolderSelectedEventHandler(object sender, FolderSelectedEventArgs e);

    public partial class FolderSelector : UserControl
    {
        public event FolderSelectedEventHandler FolderSelected;
        Settings settings = new Settings();
        private string selectedFolder = string.Empty;

        public bool SaveFolderHistoryOnSelection { get; set; }

        public string SelectedFolder
        {
            get
            {
                return selectedFolder;
            }
            set
            {
                cbPath.Text = selectedFolder = value;
                FolderSelectedEventArgs args = new FolderSelectedEventArgs();
                args.Path = cbPath.Text;
                OnFolderSelected(args);
            }
        }

        public FolderSelector()
        {
            InitializeComponent();

            if (settings.FolderSelectorPaths == null)
            {
                settings.FolderSelectorPaths = new StringCollection();
            }

            BindPathComboToSettings();
        }

        protected virtual void OnFolderSelected(FolderSelectedEventArgs e)
        {
            if (SaveFolderHistoryOnSelection)
            {
                SaveSettings();
                e.SettingsSaved = true;
            }
            if (FolderSelected != null)
                FolderSelected(this, e);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                SelectedFolder = folderBrowserDialog1.SelectedPath;
            }
        }

        private void AddHistoryItem(string path)
        {
            if (settings.FolderSelectorPaths != null && settings.FolderSelectorPaths.Contains(path))
            {
                settings.FolderSelectorPaths.Remove(path);
            }

            if (path.Trim().Length != 0)
            {
                settings.FolderSelectorPaths.Insert(0, path);
            }

            while (settings.FolderSelectorPaths.Count > settings.MaxHistory)
            {
                settings.FolderSelectorPaths.RemoveAt(settings.FolderSelectorPaths.Count - 1);
            }

            settings.Save();

            BindPathComboToSettings();
        }

        private void BindPathComboToSettings()
        {
            cbPath.Items.Clear();

            for (int count = 0; count < settings.FolderSelectorPaths.Count; count++)
            {
                cbPath.Items.Add(settings.FolderSelectorPaths[count]);
            }

            if (cbPath.Items.Count > 0)
            {
                cbPath.SelectedIndex = 0;
                selectedFolder = cbPath.Text;
            }
        }

        public void SaveSettings()
        {
            AddHistoryItem(cbPath.Text);
            settings.Save();
        }

        //private void cbPath_Leave(object sender, EventArgs e)
        //{
        //    FolderSelectedEventArgs args = new FolderSelectedEventArgs();
        //    args.Path = cbPath.Text;

        //    if (string.Compare(selectedFolder, cbPath.Text, true) != 0)
        //    {
        //        selectedFolder = cbPath.Text;
        //        OnFolderSelected(args);
        //    }
        //}

        private void cbPath_SelectedValueChanged(object sender, EventArgs e)
        {
            FolderSelectedEventArgs args = new FolderSelectedEventArgs();
            args.Path = cbPath.Text;

            if (string.Compare(selectedFolder, cbPath.Text, true) != 0)
            {
                selectedFolder = cbPath.Text;
                OnFolderSelected(args);
            }
        }

        private void FolderSelector_Load(object sender, EventArgs e)
        {
            selectedFolder = cbPath.Text;
        }
    }
}
