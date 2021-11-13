using System;
using System.Collections.Generic;
using System.Text;

namespace Wizard.UI
{
    public class WizardPageCollection : List<WizardPage>
    {
        public WizardPage this[string pageName]
        {
            get
            {
                return this.Find(delegate(WizardPage page) { return pageName == page.Name; });
            }
        }
    }
}
