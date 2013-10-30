using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using KPP.Core.Debug;
using System.Threading;
using System.Globalization;

namespace EpsonModule {
    public partial class EpsonMainForm : DockContent {

        EpsonModule Epson = null;
        public EpsonMainForm(EpsonModule mEpson) {
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
            this.CloseButtonVisible = false;
            _EpsonConfigForm.CloseButtonVisible = false;
            _EpsonStatusForm.CloseButtonVisible = false;

            Epson = mEpson;

            _EpsonConfigForm.__propertyGridEpson.SelectedObject = Epson;
            if (Epson != null) {
                Epson.EpsonServer.Connected += new IOModule.TCPServer.TcpServerEventDlgt(EpsonServer_Connected);
                Epson.EpsonServer.Disconnected += new IOModule.TCPServer.TcpServerEventDlgt(EpsonServer_Disconnected);
                Epson.EpsonServer.ServerClientMessage += new IOModule.TCPServer.TcpServerClientMessageEvent(EpsonServer_ServerClientMessage);
                Epson.EpsonServer.StartListening();

                Epson.EpsonAndroidServer.Connected += new IOModule.TCPServer.TcpServerEventDlgt(EpsonAndroidServer_Connected);
                Epson.EpsonAndroidServer.Disconnected += new IOModule.TCPServer.TcpServerEventDlgt(EpsonAndroidServer_Disconnected);
                Epson.EpsonAndroidServer.ServerClientMessage += new IOModule.TCPServer.TcpServerClientMessageEvent(EpsonAndroidServer_ServerClientMessage);
                Epson.EpsonAndroidServer.StartListening();

                Epson.OnEpsonStatusChanged += new EpsonModule.EpsonStatusChanged(Epson_OnEpsonStatusChanged);
                _EpsonConfigForm.Epson = Epson;

            }
        }

        void __dockPanel1_ActivePaneChanged(object sender, EventArgs e) {
            //try {
            //    DockPane pane = __dockPanel1.ActivePane;
            //    if (pane!=null) {
            //        __dockPanel1.VerticalScroll.Maximum = pane.Height;
            //        __dockPanel1.VerticalScroll.SmallChange = __dockPanel1.VerticalScroll.Maximum / 8;
            //        __dockPanel1.VerticalScroll.LargeChange = __dockPanel1.VerticalScroll.Maximum / 4;

            //    }

            //} catch (Exception exp) {

            //    log.Error(exp);
            //}
        }
        

        private static KPPLogger log = new KPPLogger(typeof(EpsonMainForm));
        private EpsonStatusForm _EpsonStatusForm = new EpsonStatusForm();
        private EpsonConfigForm _EpsonConfigForm = new EpsonConfigForm();

   

        void EpsonAndroidServer_ServerClientMessage(object sender, IOModule.TCPServerClientEventArgs e) {
            String[] Args = e.MessageReceived.Replace("\n", "").Replace("\t", "").Split(new String[] { "|" }, StringSplitOptions.None);
            if (Args.Count() > 0) {
                if (Args[0]=="SET") {
                    if (Args[1] == "MANMODE") {
                          this.BeginInvoke((MethodInvoker)delegate {
                              if (MessageBox.Show(this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", "pedido_man_remota_txt"), this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", "pedido_man_remota_cap"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) {                              
                                  Epson.EpsonAndroidServer.Client.Write("SET|MANMODE|OK");
                                  String ptsequence = "";
                                  foreach (String item in _EpsonConfigForm.__PointsList.Items) {
                                      ptsequence = ptsequence+"|" + item;
                                  }
                                  Epson.EpsonAndroidServer.Client.Write("SET|POINTLIST|" + ptsequence);
                              }
                              else {
                                  Epson.EpsonAndroidServer.Client.Write("SET|MANMODE|NOK");
                              }
                          });
                    }
                    else if (Args[1]=="POINT") {
                        if (Epson.EpsonServer.Client!=null) {
                            Epson.EpsonServer.Client.Write("SAVECURREPOS|" + Args[2]);
                        }
                        
                    }
                }
                else if (Args[0]=="JUMPTO") {
                     if (Args[1]=="POINT") {
                        if (Epson.EpsonServer.Client!=null) {
                            Epson.EpsonServer.Client.Write("JUMPTO|POINT|" + Args[2]);
                        }
                        
                    }
                }
               
            }
        }

        void EpsonAndroidServer_Disconnected(object sender, IOModule.TCPServerEventArgs e) {
            if (true) {
                
            }
        }

        void EpsonAndroidServer_Connected(object sender, IOModule.TCPServerEventArgs e) {
            if (Epson.EpsonServer.Client!=null) {
                Epson.EpsonAndroidServer.Client.Write("STATUS|CONNECTED");
            } else {
                Epson.EpsonAndroidServer.Client.Write("STATUS|NOTCONNECTED");
            }
        }


        private Boolean m_ErrorSet = false;

        void EpsonServer_ServerClientMessage(object sender, IOModule.TCPServerClientEventArgs e) {
            try {
                String[] Args = e.MessageReceived.Replace("\n", "").Replace("\t", "").Split(new String[] { "|" }, StringSplitOptions.None);
                if (Args.Count() > 0) {
                    if (Args[0] == "STATUS") {
                        if (Args[1] == "POS") {
                            this.BeginInvoke((MethodInvoker)delegate {
                                _EpsonStatusForm._posx.Text = Math.Round(double.Parse(Args[2].Replace(".",",")),2).ToString();
                                _EpsonStatusForm._posy.Text = Math.Round(double.Parse(Args[3].Replace(".", ",")), 2).ToString();
                                _EpsonStatusForm._posz.Text = Math.Round(double.Parse(Args[4].Replace(".", ",")), 2).ToString();
                                _EpsonStatusForm._posu.Text = Math.Round(double.Parse(Args[5].Replace(".", ",")), 2).ToString();
                            });                        
                        }
                        else if (Args[1] == "STARTED") {                            
                            this.BeginInvoke((MethodInvoker)delegate {
                                Epson.Status = EpsonStatus.Started;
                                __btStart.Enabled = false;
                                __btStop.Enabled = true;
                                update_progressbar(_EpsonStatusForm.__progressVisStatus, "", 0, SystemColors.InactiveCaption);
                                String msg = this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", "Operation_Started");
                                if (Epson.EpsonAndroidServer.Client != null) {
                                    Epson.EpsonAndroidServer.Client.Write("STATUS|RB|"+msg);
                                }
                                update_progressbar(_EpsonStatusForm.__progressRBStatus, msg, 100, Color.DarkSeaGreen);
                                m_ErrorSet = false;
                            });


                        } else if (Args[1] == "STOPPED") {
                            
                            this.BeginInvoke((MethodInvoker)delegate {
                                Epson.Status = EpsonStatus.Stopped;
                                __btStart.Enabled = true;
                                __timerParagem.Enabled = false;
                                __btStop.Text = this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", "__btStopStopped");
                                __btStop.BackColor = SystemColors.Control;
                                __btStop.Enabled = false;
                                if (Epson.EpsonAndroidServer.Client != null) {
                                    Epson.EpsonAndroidServer.Client.Write("STATUS|RB|" + __btStop.Text);
                                }
                                if (!m_ErrorSet ) {
                                    update_progressbar(_EpsonStatusForm.__progressRBStatus, this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", "No_operation_running"), 100, SystemColors.InactiveCaption); 
                                }
                                update_progressbar(_EpsonStatusForm.__progressVisStatus, "", 0, SystemColors.InactiveCaption);
                            });
                        } else if (Args[1] == "MSGRB") {
                            if (Args.Count() > 3) {
                                if (!m_ErrorSet ) {
                                    String msg = this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", Args[2]);
                                    if (Epson.EpsonAndroidServer.Client != null) {
                                        Epson.EpsonAndroidServer.Client.Write("STATUS|RB|" + msg);
                                    }
                                    update_progressbar(_EpsonStatusForm.__progressRBStatus, msg, int.Parse(Args[3]), Color.DarkSeaGreen); 
                                }
                            } else {
                                update_progressbar(_EpsonStatusForm.__progressRBStatus, this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", Args[2]), Color.DarkSeaGreen);
                            }
                        } else if (Args[1] == "MSGVIB") {
                            if (Args.Count() > 3) {
                                update_progressbar(_EpsonStatusForm.__progressVibStatus, this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", Args[2]), int.Parse(Args[3]), Color.DarkSeaGreen);
                            } else {
                                update_progressbar(_EpsonStatusForm.__progressVibStatus, this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", Args[2]), 100, Color.DarkSeaGreen);
                            }

                        } else if (Args[1] == "MSGVIS") {
                            if (Args.Count() > 3) {
                                update_progressbar(_EpsonStatusForm.__progressVisStatus, this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", Args[2]), int.Parse(Args[3]), Color.DarkSeaGreen);
                            } else {
                                update_progressbar(_EpsonStatusForm.__progressVisStatus, this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", Args[2]), 100, Color.DarkSeaGreen);
                            }

                        } else if (Args[1] == "ERRRB") {
                            m_ErrorSet = true;
                            update_progressbar(_EpsonStatusForm.__progressRBStatus, this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", Args[2]), 100, Color.Tomato);
                        } else if (Args[1] == "INVALIDPOS" || Args[1] == "INVALIDRINGS") {
                            
                            this.BeginInvoke((MethodInvoker)delegate {
                                _EpsonStatusForm.__btNewInsp.Enabled = true;
                            });


                        } else if (Args[1] == "WAITRINGS") {
                            this.BeginInvoke((MethodInvoker)delegate {
                                WaitBlister blister = new WaitBlister();
                                blister.Show();
                                blister.__btok.Click += new EventHandler(__btok_Click);
                            });
                        } else if (Args[1] == "ERRVIB") {
                            update_progressbar(_EpsonStatusForm.__progressVibStatus, this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", Args[2]), 100, Color.Tomato);
                        } else if (Args[1] == "ERRVIS") {
                            update_progressbar(_EpsonStatusForm.__progressVisStatus, this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", Args[2]), 100, Color.Tomato);
                        } else if (Args[1] == "MAINTENANCE") {
                            this.BeginInvoke((MethodInvoker)delegate {
                                //_EpsonStatusForm.__btNewInsp.Enabled = true;

                                Epson.Status = EpsonStatus.Maintenance;
                            });

                        } else if (Args[1] == "WAITSTEP") {
                            this.BeginInvoke((MethodInvoker)delegate {

                                update_progressbar(_EpsonConfigForm.__progressStepStatus, this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", "waitstep"), 100, Color.Tomato);

                                _EpsonConfigForm.__nextStep.Enabled = true;
                            });

                        }

                    } else if (Args[0] == "POINTLIST") {
                        List<String> newarray=Args.ToList();
                        newarray.RemoveAt(0);
                        this.BeginInvoke((MethodInvoker)delegate {
                            _EpsonConfigForm.__PointsList.Items.Clear();
                            _EpsonConfigForm.__PointsList.Items.AddRange(newarray.ToArray());

                            _EpsonConfigForm.__palletept1.Items.Clear();
                            _EpsonConfigForm.__palletept2.Items.Clear();
                            _EpsonConfigForm.__palletept3.Items.Clear();
                            _EpsonConfigForm.__palletept4.Items.Clear();

                            _EpsonConfigForm.__palletept1.Items.AddRange(newarray.ToArray());
                            _EpsonConfigForm.__palletept2.Items.AddRange(newarray.ToArray());
                            _EpsonConfigForm.__palletept3.Items.AddRange(newarray.ToArray());
                            _EpsonConfigForm.__palletept4.Items.AddRange(newarray.ToArray());
                            

                            _EpsonConfigForm.__palletenr.SelectedIndex = _EpsonConfigForm.__palletenr.Items.IndexOf(Epson.Pallete.PalleteNumber.ToString());
                            _EpsonConfigForm.__pallete_col.SelectedIndex = _EpsonConfigForm.__pallete_col.Items.IndexOf(Epson.Pallete.PalleteCol.ToString());
                            _EpsonConfigForm.__pallete_lines.SelectedIndex = _EpsonConfigForm.__pallete_lines.Items.IndexOf(Epson.Pallete.PalleteLines.ToString());
                            _EpsonConfigForm.__palletept1.SelectedIndex = _EpsonConfigForm.__palletept1.Items.IndexOf(Epson.Pallete.Point1);
                            _EpsonConfigForm.__palletept2.SelectedIndex = _EpsonConfigForm.__palletept2.Items.IndexOf(Epson.Pallete.Point2);
                            _EpsonConfigForm.__palletept3.SelectedIndex = _EpsonConfigForm.__palletept3.Items.IndexOf(Epson.Pallete.Point3);
                            _EpsonConfigForm.__palletept4.SelectedIndex = _EpsonConfigForm.__palletept4.Items.IndexOf(Epson.Pallete.Point4);
                            
                        });

                    }
                }
            } catch (Exception exp) {

                log.Error(exp);
            }
        }

        void __btok_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("SET|NEWBLISTER");
        }

        void EpsonServer_Disconnected(object sender, IOModule.TCPServerEventArgs e) {
            if (Epson.EpsonAndroidServer.Client != null) {
                Epson.EpsonAndroidServer.Client.Write("STATUS|NOTCONNECTED");
            }

            update_progressbar(_EpsonStatusForm.__progressRBStatus,this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", "Not_connected_to_robot"), 0, SystemColors.InactiveCaption);
            this.BeginInvoke((MethodInvoker)delegate {
                __toolStripDisconnected.Visible = true;
                __toolStripConnected.Visible = false;
                __btStart.Enabled = __btStop.Enabled = false;
                _EpsonConfigForm.__panelRb.Enabled = false;
            });    
        }

        void EpsonServer_Connected(object sender, IOModule.TCPServerEventArgs e) {
            if (Epson.EpsonAndroidServer.Client != null) {
                Epson.EpsonAndroidServer.Client.Write("STATUS|CONNECTED");
            }
            this.BeginInvoke((MethodInvoker)delegate {
                __toolStripDisconnected.Visible = false;
                __toolStripConnected.Visible = true;
                __btStart.Enabled = __btStop.Enabled = false;
                _EpsonConfigForm.__panelRb.Enabled = true;
                e.ConnectionState.Write("GET|STATUS");
                e.ConnectionState.Write("GET|POINTS");
                // ServerClient.Send("GET|STATUS");
            });
        }


        void update_progressbar(ExtendedDotNET.Controls.Progress.ProgressBar progressbar, String message, Color cl) {
            update_progressbar(progressbar, message,-1, cl);
        }
        void update_progressbar(ExtendedDotNET.Controls.Progress.ProgressBar progressbar, String message,int value,Color cl) {

            try {
                if (this.InvokeRequired) {
                    BeginInvoke(new MethodInvoker(delegate {
                        update_progressbar(progressbar, message, value, cl);
                    }));
                } else {
                    //Thread.Sleep(250);

                    progressbar.MainColor = cl;

                    progressbar.Caption = message;
                    if (value != -1) {
                        progressbar.Value = value;
                    }

                }
            } catch (Exception exp) {

                log.Error(exp);
                log.Warn("("+message+")");
            }
        }


        void StaticObjects_OnAcesslevelChanged(Acesslevel NewLevel) {
            switch (NewLevel) {
                case Acesslevel.Admin:
                    _EpsonConfigForm.Show(__dockPanel1);
                    break;
                case Acesslevel.User:
                    _EpsonConfigForm.Hide();
                    break;
                default:
                    break;
            }
        }
        
        DeserializeDockContent m_deserializeDockContent;
            
        private IDockContent GetContentFromPersistString(string persistString) {

            if (persistString == typeof(EpsonStatusForm).ToString())
                return _EpsonStatusForm;
            else if (persistString == typeof(EpsonConfigForm).ToString())
                return _EpsonConfigForm;

            else {

                return null;
            }
        }


        public string _epsonfile { get; set; }

        private void EpsonMainForm_Load(object sender, EventArgs e) {
            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

            _epsonfile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\Epson.config");



            if (File.Exists(_epsonfile))
                try {
                    __dockPanel1.LoadFromXml(_epsonfile, m_deserializeDockContent);
                } catch (Exception exp) {

                    __dockPanel1.SaveAsXml(_epsonfile);

                } else {



            }
            if (_EpsonConfigForm.Visible == false && StaticObjects.AcessLevel == Acesslevel.Admin) {
                _EpsonConfigForm.Show(__dockPanel1);
            }
            if (_EpsonStatusForm.Visible == false) {
                _EpsonStatusForm.Show(__dockPanel1);
            }


            StaticObjects.OnAcesslevelChanged += new StaticObjects.AcesslevelChanged(StaticObjects_OnAcesslevelChanged);

            _EpsonStatusForm.__btNewInsp.Click += new EventHandler(__btNewInsp_Click);
            
            
        }

        void Epson_OnEpsonStatusChanged(EpsonStatus NewStatus) {
            switch (NewStatus) {
                case EpsonStatus.Maintenance:
                        _EpsonConfigForm.__groupJogging.Enabled = true;
                    break;
                case EpsonStatus.Stopped:

                    _EpsonConfigForm.__groupJogging.Enabled = true;
                    break;
                case EpsonStatus.Started:
                    _EpsonConfigForm.__groupJogging.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        void __btNewInsp_Click(object sender, EventArgs e) {
            Epson.EpsonServer.Client.Write("NEWINSP|START");
        }

        private void EpsonMainForm_FormClosing(object sender, FormClosingEventArgs e) {
            try {

                if (File.Exists(_epsonfile)) {
                    __dockPanel1.SaveAsXml(_epsonfile);    
                }   
                
                
            } catch (Exception exp) {

                log.Error(exp);
            }
        }

        private void __btInit_Click(object sender, EventArgs e) {
            try {
                __btStart.Enabled = false;
                __timerParagem.Enabled = false;
                __btStop.Text = this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", "__btStopStopped");
                __btStop.BackColor = SystemColors.Control;
                Epson.EpsonServer.Client.Write("SET|OPERATION|START");                
            } catch (Exception exp) {

                log.Error(exp);
            }
        }

        private void __btStop_Click(object sender, EventArgs e) {
            try {
                __btStop.Enabled = false;
                __btStop.Text=this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", "__btStopStopping");
                __timerParagem.Enabled = true;
                Epson.EpsonServer.Client.Write("SET|OPERATION|STOP");
            } catch (Exception exp) {

                log.Error(exp);
            }
        }

        Boolean switchBoolean = false;

        private void __timerParagem_Tick(object sender, EventArgs e) {
            switchBoolean = !switchBoolean;
            if (switchBoolean) {
                __btStop.BackColor = SystemColors.Control;
            }
            else {
                __btStop.BackColor = Color.OrangeRed;
            }
        }

    }
}
