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
using KPP.Core.Debug;
using System.Runtime.InteropServices;

namespace EpsonModule {
    public partial class EpsonConfigForm : DockContent {


        private static KPPLogger log = new KPPLogger(typeof(EpsonConfigForm));

        private EpsonModule m_epson = null;

        public EpsonModule Epson {
            get { return m_epson; }
            set {
                if (value!=m_epson) {
                    m_epson = value;
                    
                    

                    if (m_epson!=null) {
                        m_epson.OnEpsonStatusChanged += new EpsonModule.EpsonStatusChanged(m_epsonmodule_OnEpsonStatusChanged);
                    }
                }
            }
        }

        void m_epsonmodule_OnEpsonStatusChanged(EpsonStatus NewStatus) {
            try {
                switch (NewStatus) {
                    case EpsonStatus.Stopped:
                        __panelRb.Enabled = true;
                        __btmaintenance.Text = this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", "maintenancestart");
                        break;
                    case EpsonStatus.Started:
                        __panelRb.Enabled = false;
                        break;
                    case EpsonStatus.Maintenance:
                        __panelRb.Enabled = true;
                        break;
                    default:
                        break;
                }
            } catch (Exception exp) {

                log.Error(exp);
            }
        }

        public EpsonConfigForm() {
            switch (EpsonModule.Language) {
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

        private void __btJumpTo_Click(object sender, EventArgs e) {
            if (Epson != null) {
                if (__PointsList.SelectedIndex>-1) {
                    Epson.EpsonServer.Client.Write("JUMPTO|" + __PointsList.Text);
                }
                
            }
        }

        private void __btmaintenance_Click(object sender, EventArgs e) {
            if (Epson.Status== EpsonStatus.Maintenance) {
                __btmaintenance.Text = this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", "maintenancestart");
                Epson.EpsonServer.Client.Write("SET|MAINTENANCE|STOP");                
            } else {
                Epson.EpsonServer.Client.Write("SET|MAINTENANCE|START");
                __btmaintenance.Text = this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", "maintenancestop");
                checkBox1_CheckedChanged(sender, e);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            if (__stepmode.Checked) {
                Epson.EpsonServer.Client.Write("SET|MAINTENANCE|STEPMODE|ON");
            } else {
                Epson.EpsonServer.Client.Write("SET|MAINTENANCE|STEPMODE|OFF");
            }
            
        }


        void update_progressbar(ExtendedDotNET.Controls.Progress.ProgressBar progressbar, String message, int value, Color cl) {

            if (this.InvokeRequired) {
                BeginInvoke(new MethodInvoker(delegate {
                    update_progressbar(progressbar, message, value, cl);
                }));
            } else {
                //Thread.Sleep(250);

                progressbar.MainColor = cl;

                progressbar.Caption = message;
                progressbar.Value = value;
            }
        }


        private void button1_Click(object sender, EventArgs e) {
            __nextStep.Enabled = false;
            update_progressbar(__progressStepStatus, this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", "dostep"), 0, Color.DarkSeaGreen);
            Epson.EpsonServer.Client.Write("SET|NEXTSTEP");
        }

        private void __btUpdatSpeeds_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("SET|SPEED|"+__numericUpDownSpeed.Value.ToString());
            Epson.EpsonServer.Client.Write("SET|ACCEL|" + __numericUpDownAccel.Value.ToString());
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        private const uint MOUSEEVENTF_LEFTUP = 0x04;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x08;

        private const uint MOUSEEVENTF_RIGHTUP = 0x10;

       

        private void __btyup_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("JOG|Y|" + __numericUpDownIncrement.Value.ToString().Replace(",", "."));
        }

        private void __btydown_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("JOG|Y|-" + __numericUpDownIncrement.Value.ToString().Replace(",","."));
        }

        private void __btxup_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("JOG|X|" + __numericUpDownIncrement.Value.ToString().Replace(",","."));
        }

        private void __btxdown_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("JOG|X|-" + __numericUpDownIncrement.Value.ToString().Replace(",","."));
        }

        private void __btzup_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("JOG|Z|" + __numericUpDownIncrement.Value.ToString().Replace(",","."));
        }

        private void __btzdown_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("JOG|Z|-" + __numericUpDownIncrement.Value.ToString().Replace(",","."));
        }

        private void __btuup_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("JOG|U|" + __numericUpDownIncrement.Value.ToString().Replace(",","."));
        }

        private void __btudown_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("JOG|U|-" + __numericUpDownIncrement.Value.ToString().Replace(",","."));
        }

      //  ScrollBar vScrollBar1;

        private void EpsonConfigForm_Load(object sender, EventArgs e) {
            
            //vScrollBar1.Dock = DockStyle.Right;
            //vScrollBar1.Scroll += new ScrollEventHandler(vScrollBar1_Scroll); 
            //panel1.Controls.Add(vScrollBar1);
        }

        void vScrollBar1_Scroll(object sender, ScrollEventArgs e) {
            //panel1.VerticalScroll.Value = vScrollBar1.Value; 
        }

        private void EpsonConfigForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (true) {
                
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e) {

        }

        private void __groupJogging_Enter(object sender, EventArgs e) {

        }

        private void __btSavePos_Click(object sender, EventArgs e) {
            if (MessageBox.Show(this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", "confirm_save_pts_txt"), this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", "confirm_save_pts_cap"), MessageBoxButtons.YesNo)== System.Windows.Forms.DialogResult.Yes) {
                //if (!) {
                    
                //}
                Epson.EpsonServer.Client.Write("SAVECURREPOS|" + __PointsList.Text);
            }
            
        }

        private void __btOpenGear_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("SET|TOOL|GEAR|OPEN");
        }

        private void __btCloseGear_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("SET|TOOL|GEAR|CLOSE");
        }

        private void __btOpenRing_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("SET|TOOL|RING|OPEN");
        }

        private void __btCloseRing_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("SET|TOOL|RING|CLOSE");
        }

        private void __btUpRing_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("SET|TOOL|RING|UP");
        }

        private void __btDownRing_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("SET|TOOL|RING|DOWN");
        }

        private void __btfeedstart_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("SET|FEED|START");
        }

        private void __btfeedstop_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("SET|FEED|STOP");
        }

        private void __btgearlock_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("SET|FEED|LOCK");
        }

        private void __btgearunlock_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("SET|FEED|UNLOCK");
        }

        private void __btblowfronton_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("SET|FEED|AIRFRONT|ON");
        }

        private void __btblowfrontoff_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("SET|FEED|AIRFRONT|OFF");
        }

        private void __btblowsideon_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("SET|FEED|AIRSIDE|ON");
        }

        private void __btblowsideoff_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("SET|FEED|AIRSIDE|OFF");
        }

        private void __checkFreeJoint1_CheckedChanged(object sender, EventArgs e) {
            if (__checkFreeJoint1.Checked) {
                Epson.EpsonServer.Client.Write("SET|JOINT|1|OFF");
            } else {
                Epson.EpsonServer.Client.Write("SET|JOINT|1|ON");
            }
        }

        private void __checkFreeJoint2_CheckedChanged(object sender, EventArgs e) {
            if (__checkFreeJoint2.Checked) {
                Epson.EpsonServer.Client.Write("SET|JOINT|2|OFF");
            } else {
                Epson.EpsonServer.Client.Write("SET|JOINT|2|ON");
            }
        }

        private void __checkFreeJoint3_CheckedChanged(object sender, EventArgs e) {
            if (__checkFreeJoint3.Checked) {
                Epson.EpsonServer.Client.Write("SET|JOINT|3|OFF");
            } else {
                Epson.EpsonServer.Client.Write("SET|JOINT|3|ON");
            }
        }

        private void __checkFreeJoint4_CheckedChanged(object sender, EventArgs e) {
            if (__checkFreeJoint4.Checked) {
                Epson.EpsonServer.Client.Write("SET|JOINT|4|OFF");
            } else {
                Epson.EpsonServer.Client.Write("SET|JOINT|4|ON");
            }
        }

        private void __btReset_Click(object sender, EventArgs e) {
           
        }

        private void __btReset_Click_1(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("SET|RESET");
        }

        private void __palletept1_SelectedIndexChanged(object sender, EventArgs e) {
            if (__palletept1.Text!="") {
                Epson.Pallete.Point1 = __palletept1.Text;
            }
        }

        private void __palletept3_SelectedIndexChanged(object sender, EventArgs e) {
            if (__palletept3.Text != "") {
                Epson.Pallete.Point3 = __palletept3.Text;
            }
        }

        private void __palletept2_SelectedIndexChanged(object sender, EventArgs e) {
            if (__palletept2.Text != "") {
                Epson.Pallete.Point2 = __palletept2.Text;
            }
        }


        private void __palletenr_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                if (__palletenr.Text != "") {
                    Epson.Pallete.PalleteNumber = int.Parse(__palletenr.Text);
                }
            } catch (Exception exp) {

                log.Error(exp);
            }
        }

        private void __pallete_lines_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                if (__pallete_lines.Text != "") {
                    Epson.Pallete.PalleteLines = int.Parse(__pallete_lines.Text);
                }
            } catch (Exception exp) {

                log.Error(exp);
            }
        }

        private void __pallete_col_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                if (__pallete_col.Text != "") {
                    Epson.Pallete.PalleteCol = int.Parse(__pallete_col.Text);
                }
            } catch (Exception exp) {

                log.Error(exp);
            }
        }

        private void __btsetPallete_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("SET|PALLET|" + 
                Epson.Pallete.PalleteNumber.ToString() + 
                "|" + Epson.Pallete.Point1 + "|"+
                "|" + Epson.Pallete.Point2 + "|"+
                "|" + Epson.Pallete.Point3 + "|"+
                "|" + Epson.Pallete.Point4 + "|" +               
                "|" + Epson.Pallete.PalleteCol+ "|"+
                "|" + Epson.Pallete.PalleteLines
                );
        }

        private void __pallete_lines_SelectedValueChanged(object sender, EventArgs e) {
            try {
                __goLine.Items.Clear();
                for (int i = 0; i < int.Parse(__pallete_lines.Text); i++) {
                    __goLine.Items.Add((i + 1).ToString());
                }
            } catch (Exception exp) {

                log.Error(exp);
            }
        }

        private void __pallete_col_SelectedValueChanged(object sender, EventArgs e) {
            try {
                __goCol.Items.Clear();
                for (int i = 0; i < int.Parse(__pallete_col.Text); i++) {
                    __goCol.Items.Add((i + 1).ToString());
                }
            } catch (Exception exp) {

                log.Error(exp);
            }
        }

        private void __btgotopallete_Click(object sender, EventArgs e) {
            try {
                Epson.EpsonServer.Client.Write("JUMPTOPALLET|" +__goCol.Text+"|" +__goLine.Text);
            } catch (Exception exp) {

                log.Error(exp);
            }
        }

        private void __btLightRingsOn_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("SET|LIGHT|1|ON");
        }

        private void __btLightRingsOff_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("SET|LIGHT|1|OFF");
        }

        private void __checkFreeall_CheckedChanged(object sender, EventArgs e) {
            if (__checkFreeall.Checked) {
                Epson.EpsonServer.Client.Write("SET|JOINT|ALL|OFF");
            }
            else {
                Epson.EpsonServer.Client.Write("SET|JOINT|ALL|ON");
            }
        }

        private void __palletept4_SelectedIndexChanged(object sender, EventArgs e) {
            if (__palletept4.Text != "") {
                Epson.Pallete.Point4 = __palletept4.Text;
            }
        }

        private void EpsonConfigForm_KeyDown(object sender, KeyEventArgs e) {
            if (true) {
                
            }
        }

        private void __btBypassOn_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("SET|BYPASS|ON");
            __btBypassOn.BackColor = Color.Red;
            __btBypassOff.BackColor = SystemColors.Control;
        }

        private void __btBypassOff_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("SET|BYPASS|OFF");
            __btBypassOn.BackColor = SystemColors.Control;
            __btBypassOff.BackColor = Color.Red;
        }

       
    }
}
