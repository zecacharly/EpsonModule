using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Threading;
using System.Globalization;
using KPPAutomationCore;


namespace EpsonModule {
    public partial class EpsonStatusForm : DockContent {



        public EpsonProject SelectedProject=null;

        public EpsonStatusForm() {
            switch (LanguageSettings.Language) {
                case LanguageName.Unk:
                    break;
                case LanguageName.PT:
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-PT");

                    break;
                case LanguageName.EN:
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("de");

                    break;
                default:
                    break;
            }
            InitializeComponent();
        }

        private void EpsonStatusForm_Load(object sender, EventArgs e) {
            
        }

        private void __btNewInsp_Click(object sender, EventArgs e) {
            //__btNewInsp.Enabled = false;
        }

        private void __btNewInsp_Click_1(object sender, EventArgs e) {
            __btNewInsp.Enabled = false;
        }

    }
}
