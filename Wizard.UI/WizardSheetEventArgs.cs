using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Wizard.UI
{
    public class WizardSheetEventArgs : CancelEventArgs
    {
        public WizardPage ActivePage { get; set; }

        public WizardSheetEventArgs(WizardPage activePage)
        {
            ActivePage = activePage;
        }
    }

    public delegate void WizardSheetEventHandler(object sender, WizardSheetEventArgs e);

}
