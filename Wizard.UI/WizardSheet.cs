using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Wizard.Controls;

namespace Wizard.UI
{
    /// <summary>
    /// Summary description for WizardSheet.
    /// </summary>
    public class WizardSheet : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public WizardSheet()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.backButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.finishButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.helpButton = new System.Windows.Forms.Button();
            this.etchedLine1 = new Wizard.Controls.EtchedLine();
            this.pagePanel = new System.Windows.Forms.Panel();
            this.aboutButton = new System.Windows.Forms.Button();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.backButton.Location = new System.Drawing.Point(189, 8);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 0;
            this.backButton.Text = "< &Back";
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nextButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.nextButton.Location = new System.Drawing.Point(263, 8);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 1;
            this.nextButton.Text = "&Next >";
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // finishButton
            // 
            this.finishButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.finishButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.finishButton.Location = new System.Drawing.Point(344, 8);
            this.finishButton.Name = "finishButton";
            this.finishButton.Size = new System.Drawing.Size(75, 23);
            this.finishButton.TabIndex = 2;
            this.finishButton.Text = "&Finish";
            this.finishButton.Click += new System.EventHandler(this.finishButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cancelButton.Location = new System.Drawing.Point(425, 8);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.aboutButton);
            this.buttonPanel.Controls.Add(this.helpButton);
            this.buttonPanel.Controls.Add(this.etchedLine1);
            this.buttonPanel.Controls.Add(this.cancelButton);
            this.buttonPanel.Controls.Add(this.backButton);
            this.buttonPanel.Controls.Add(this.finishButton);
            this.buttonPanel.Controls.Add(this.nextButton);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.Location = new System.Drawing.Point(0, 99);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(509, 40);
            this.buttonPanel.TabIndex = 4;
            // 
            // helpButton
            // 
            this.helpButton.Location = new System.Drawing.Point(12, 8);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(75, 23);
            this.helpButton.TabIndex = 5;
            this.helpButton.Text = "Help";
            this.helpButton.UseVisualStyleBackColor = true;
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // etchedLine1
            // 
            this.etchedLine1.Dock = System.Windows.Forms.DockStyle.Top;
            this.etchedLine1.Edge = Wizard.Controls.EtchEdge.Top;
            this.etchedLine1.Location = new System.Drawing.Point(0, 0);
            this.etchedLine1.Name = "etchedLine1";
            this.etchedLine1.Size = new System.Drawing.Size(509, 8);
            this.etchedLine1.TabIndex = 4;
            // 
            // pagePanel
            // 
            this.pagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pagePanel.Location = new System.Drawing.Point(0, 0);
            this.pagePanel.Name = "pagePanel";
            this.pagePanel.Size = new System.Drawing.Size(509, 99);
            this.pagePanel.TabIndex = 5;
            // 
            // aboutButton
            // 
            this.aboutButton.Location = new System.Drawing.Point(93, 8);
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(75, 23);
            this.aboutButton.TabIndex = 6;
            this.aboutButton.Text = "About";
            this.aboutButton.UseVisualStyleBackColor = true;
            this.aboutButton.Click += new System.EventHandler(this.aboutButton_Click);
            // 
            // WizardSheet
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(509, 139);
            this.Controls.Add(this.pagePanel);
            this.Controls.Add(this.buttonPanel);
            this.MinimumSize = new System.Drawing.Size(525, 175);
            this.Name = "WizardSheet";
            this.Text = "WizardSheet";
            this.Load += new System.EventHandler(this.WizardSheet_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.WizardSheet_Closing);
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private Button backButton;
        private Button nextButton;
        private Button finishButton;
        private Button cancelButton;
        private Panel buttonPanel;
        private EtchedLine etchedLine1;
        private Panel pagePanel;
        private Button helpButton;
        private Button aboutButton;
        private WizardPage _activePage;

        public WizardPage ActivePage
        {
            get { return _activePage; }
        }

        WizardPageCollection _pages = new WizardPageCollection();


        private void WizardSheet_Load(object sender, System.EventArgs e)
        {
            if (_pages.Count != 0)
            {
                ResizeToFit();
                SetActivePage(0);
            }
            else
                SetWizardButtons(WizardButtons.None);
        }

        private void ResizeToFit()
        {
            Size maxPageSize = new Size(buttonPanel.Width, 0);

            foreach (WizardPage page in _pages)
            {
                if (page.Width > maxPageSize.Width)
                    maxPageSize.Width = page.Width;
                if (page.Height > maxPageSize.Height)
                    maxPageSize.Height = page.Height;
            }

            foreach (WizardPage page in _pages)
            {
                page.Size = maxPageSize;
            }

            Size extraSize = this.Size;
            extraSize -= pagePanel.Size;

            Size newSize = maxPageSize + extraSize;
            this.Size = newSize;
        }

        public WizardPageCollection Pages
        {
            get { return _pages; }
        }

        private int GetActiveIndex()
        {
            WizardPage activePage = GetActivePage();

            for (int i = 0; i < _pages.Count; ++i)
            {
                if (activePage == _pages[i])
                    return i;
            }

            return -1;
        }

        private WizardPage GetActivePage()
        {
            return _activePage;
        }

        public void SetActivePage(int pageIndex)
        {
            if (pageIndex < 0 || pageIndex >= _pages.Count)
                throw new ArgumentOutOfRangeException("pageIndex");

            WizardPage page = (WizardPage)_pages[pageIndex];
            SetActivePage(page);
        }

        public void SetActivePage(string newPageName)
        {
            WizardPage newPage = _pages[newPageName];

            if (newPage == null)
                throw new Exception(string.Format("Can't find page named {0}", newPageName));

            SetActivePage(newPage);
        }

        private void SetActivePage(WizardPage newPage)
        {
            WizardPage oldActivePage = _activePage;

            // If this page isn't in the Controls collection, add it.
            // This is what causes the Load event, so we defer
            // it as late as possible.
            if (!pagePanel.Controls.Contains(newPage))
                pagePanel.Controls.Add(newPage);

            // Show this page.
            newPage.Visible = true;

            _activePage = newPage;
            CancelEventArgs e = new CancelEventArgs();
            newPage.OnSetActive(e);

            if (e.Cancel)
            {
                newPage.Visible = false;
                _activePage = oldActivePage;
            }

            // Hide all of the other pages.
            foreach (WizardPage page in _pages)
            {
                if (page != _activePage)
                    page.Visible = false;
            }
        }

        internal void SetWizardButtons(WizardButtons buttons)
        {
            backButton.Enabled = ((buttons & WizardButtons.Back) != 0);
            nextButton.Enabled = ((buttons & WizardButtons.Next) != 0);
            finishButton.Enabled = ((buttons & WizardButtons.Finish) != 0);
            helpButton.Visible = ((buttons & WizardButtons.Help) != 0);
            aboutButton.Visible = ((buttons & WizardButtons.About) != 0);

            if ((buttons & WizardButtons.Finish) != 0)
            {
                finishButton.Enabled = true;
                this.AcceptButton = finishButton;
            }

            if ((buttons & WizardButtons.Next) != 0)
            {
                nextButton.Enabled = true;
                this.AcceptButton = nextButton;
            }

            if (backButton.Enabled && !(nextButton.Enabled || finishButton.Enabled))
            {
                this.AcceptButton = backButton;
            }
        }

        private WizardPageEventArgs PreChangePage(int delta)
        {
            // Figure out which page is next.
            int activeIndex = GetActiveIndex();
            int nextIndex = activeIndex + delta;

            if (nextIndex < 0 || nextIndex >= _pages.Count)
                nextIndex = activeIndex;

            // Fill in the event args.
            WizardPage newPage = (WizardPage)_pages[nextIndex];

            WizardPageEventArgs e = new WizardPageEventArgs();
            e.NewPage = newPage.Name;
            e.Cancel = false;

            return e;
        }

        private void PostChangePage(WizardPageEventArgs e)
        {
            if (!e.Cancel)
                SetActivePage(e.NewPage);
            if (finishButton.Text == "Close")
            {
                finishButton.Text = "Finish";
            }
        }

        private void nextButton_Click(object sender, System.EventArgs e)
        {
            WizardPageEventArgs wpea = PreChangePage(+1);
            _activePage.OnWizardNext(wpea);
            PostChangePage(wpea);
        }

        private void backButton_Click(object sender, System.EventArgs e)
        {
            WizardPageEventArgs wpea = PreChangePage(-1);
            _activePage.OnWizardBack(wpea);
            PostChangePage(wpea);
        }

        private void finishButton_Click(object sender, System.EventArgs e)
        {
            WizardSheetEventArgs wsea = new WizardSheetEventArgs(_activePage);
            _activePage.OnWizardFinish(wsea);
            if (wsea.Cancel)
            {
                return;
            }

            OnWizardFinish(wsea);
            if (wsea.Cancel)
            {
                return;
            }

            if (finishButton.Text == "Close")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                finishButton.Text = "Close";
                AcceptButton = finishButton;
                CancelButton = null;
                //cancelButton.Enabled = false;
                //backButton.Enabled = nextButton.Enabled = false;
            }
        }

        internal void PressButton(WizardButtons buttons)
        {
            if ((buttons & WizardButtons.Finish) == WizardButtons.Finish)
                finishButton.PerformClick();
            else if ((buttons & WizardButtons.Next) == WizardButtons.Next)
                nextButton.PerformClick();
            else if ((buttons & WizardButtons.Back) == WizardButtons.Back)
                backButton.PerformClick();
        }

        internal void EnableCancelButton(bool enableCancelButton)
        {
            cancelButton.Enabled = enableCancelButton;
        }

        private void cancelButton_Click(object sender, System.EventArgs e)
        {
            CancelEventArgs cea = new CancelEventArgs();
            _activePage.OnWizardCancel(cea);
            if (cea.Cancel)
            {
                return;
            }

            WizardSheetEventArgs wsea = new WizardSheetEventArgs(_activePage);
            OnWizardCancel(wsea);
            if (!wsea.Cancel)
            {
                this.Close();
            }
        }

        private void WizardSheet_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!cancelButton.Enabled)
                e.Cancel = true;
            else if (!finishButton.Enabled)
                OnQueryCancel(e);
        }

        protected virtual void OnQueryCancel(CancelEventArgs e)
        {
            _activePage.OnQueryCancel(e);
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            WizardSheetEventArgs wsea = new WizardSheetEventArgs(_activePage);
            _activePage.OnWizardAbout(wsea);
            if (wsea.Cancel)
            {
                return;
            }

            OnWizardAbout(wsea);
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            WizardSheetEventArgs wsea = new WizardSheetEventArgs(_activePage);
            _activePage.OnWizardHelp(wsea);
            if (wsea.Cancel)
            {
                return;
            }

            OnWizardHelp(wsea);
        }

        [Category("Wizard")]
        public event WizardSheetEventHandler WizardCancel;

        public virtual void OnWizardCancel(WizardSheetEventArgs e)
        {
            if (WizardCancel != null)
                WizardCancel(this, e);
        }

        [Category("Wizard")]
        public event WizardSheetEventHandler WizardHelp;

        public virtual void OnWizardHelp(WizardSheetEventArgs e)
        {
            if (WizardHelp != null)
                WizardHelp(this, e);
        }

        [Category("Wizard")]
        public event EventHandler WizardAbout;

        public virtual void OnWizardAbout(EventArgs e)
        {
            if (WizardAbout != null)
                WizardAbout(this, e);
        }

        [Category("Wizard")]
        public event WizardSheetEventHandler WizardFinish;

        public virtual void OnWizardFinish(WizardSheetEventArgs e)
        {
            if (WizardFinish != null)
                WizardFinish(this, e);
        }
    }

    [Flags]
    public enum WizardButtons
    {
        None = 0x0000,
        Back = 0x0001,
        Next = 0x0002,
        Finish = 0x0004,
        Help = 0x0008,
        About = 0x0010,
    }
}
