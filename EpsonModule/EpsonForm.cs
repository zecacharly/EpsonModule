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
using KPPAutomationCore;

namespace EpsonModule {
    public partial class EpsonMainForm : DockContent,IModuleForm {

        
        public EpsonMainForm() {
            Restart = false;
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
                                  ((EpsonProject)(EpsonSettings.SelectedProject)).EpsonAndroidServer.Client.Write("SET|MANMODE|OK");
                                  String ptsequence = "";
                                  foreach (String item in _EpsonConfigForm.__PointsList.Items) {
                                      ptsequence = ptsequence+"|" + item;
                                  }
                                  ((EpsonProject)(EpsonSettings.SelectedProject)).EpsonAndroidServer.Client.Write("SET|POINTLIST|" + ptsequence);
                              }
                              else {
                                  ((EpsonProject)(EpsonSettings.SelectedProject)).EpsonAndroidServer.Client.Write("SET|MANMODE|NOK");
                              }
                          });
                    }
                    else if (Args[1]=="POINT") {
                        if (((EpsonProject)(EpsonSettings.SelectedProject)).EpsonServer.Client != null) {
                            ((EpsonProject)(EpsonSettings.SelectedProject)).EpsonServer.Client.Write("SAVECURREPOS|" + Args[2]);
                        }
                        
                    }
                }
                else if (Args[0]=="JUMPTO") {
                     if (Args[1]=="POINT") {
                         if (((EpsonProject)(EpsonSettings.SelectedProject)).EpsonServer.Client != null) {
                             ((EpsonProject)(EpsonSettings.SelectedProject)).EpsonServer.Client.Write("JUMPTO|POINT|" + Args[2]);
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
            if (((EpsonProject)(EpsonSettings.SelectedProject)).EpsonServer.Client != null) {
                ((EpsonProject)(EpsonSettings.SelectedProject)).EpsonAndroidServer.Client.Write("STATUS|CONNECTED");
            } else {
                ((EpsonProject)(EpsonSettings.SelectedProject)).EpsonAndroidServer.Client.Write("STATUS|NOTCONNECTED");
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
                                ((EpsonProject)(EpsonSettings.SelectedProject)).Status = EpsonStatus.Started;
                                __btStart.Enabled = false;
                                __btStop.Enabled = true;
                                update_progressbar(_EpsonStatusForm.__progressVisStatus, "", 0, SystemColors.InactiveCaption);
                                String msg = this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", "Operation_Started");
                                if (((EpsonProject)(EpsonSettings.SelectedProject)).EpsonAndroidServer.Client != null) {
                                    ((EpsonProject)(EpsonSettings.SelectedProject)).EpsonAndroidServer.Client.Write("STATUS|RB|" + msg);
                                }
                                update_progressbar(_EpsonStatusForm.__progressRBStatus, msg, 100, Color.DarkSeaGreen);
                                m_ErrorSet = false;
                            });


                        } else if (Args[1] == "STOPPED") {
                            
                            this.BeginInvoke((MethodInvoker)delegate {
                                ((EpsonProject)(EpsonSettings.SelectedProject)).Status = EpsonStatus.Stopped;
                                __btStart.Enabled = true;
                                __timerParagem.Enabled = false;
                                __btStop.Text = this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", "__btStopStopped");
                                __btStop.BackColor = SystemColors.Control;
                                __btStop.Enabled = false;
                                if (((EpsonProject)(EpsonSettings.SelectedProject)).EpsonAndroidServer.Client != null) {
                                    ((EpsonProject)(EpsonSettings.SelectedProject)).EpsonAndroidServer.Client.Write("STATUS|RB|" + __btStop.Text);
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
                                    if (((EpsonProject)(EpsonSettings.SelectedProject)).EpsonAndroidServer.Client != null) {
                                        ((EpsonProject)(EpsonSettings.SelectedProject)).EpsonAndroidServer.Client.Write("STATUS|RB|" + msg);
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

                                ((EpsonProject)(EpsonSettings.SelectedProject)).Status = EpsonStatus.Maintenance;
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


                            _EpsonConfigForm.__palletenr.SelectedIndex = _EpsonConfigForm.__palletenr.Items.IndexOf(((EpsonProject)(EpsonSettings.SelectedProject)).Pallete.PalleteNumber.ToString());
                            _EpsonConfigForm.__pallete_col.SelectedIndex = _EpsonConfigForm.__pallete_col.Items.IndexOf(((EpsonProject)(EpsonSettings.SelectedProject)).Pallete.PalleteCol.ToString());
                            _EpsonConfigForm.__pallete_lines.SelectedIndex = _EpsonConfigForm.__pallete_lines.Items.IndexOf(((EpsonProject)(EpsonSettings.SelectedProject)).Pallete.PalleteLines.ToString());
                            _EpsonConfigForm.__palletept1.SelectedIndex = _EpsonConfigForm.__palletept1.Items.IndexOf(((EpsonProject)(EpsonSettings.SelectedProject)).Pallete.Point1);
                            _EpsonConfigForm.__palletept2.SelectedIndex = _EpsonConfigForm.__palletept2.Items.IndexOf(((EpsonProject)(EpsonSettings.SelectedProject)).Pallete.Point2);
                            _EpsonConfigForm.__palletept3.SelectedIndex = _EpsonConfigForm.__palletept3.Items.IndexOf(((EpsonProject)(EpsonSettings.SelectedProject)).Pallete.Point3);
                            _EpsonConfigForm.__palletept4.SelectedIndex = _EpsonConfigForm.__palletept4.Items.IndexOf(((EpsonProject)(EpsonSettings.SelectedProject)).Pallete.Point4);
                            
                        });

                    }
                }
            } catch (Exception exp) {

                log.Error(exp);
            }
        }

        void __btok_Click(object sender, EventArgs e) {
            ((EpsonProject)(EpsonSettings.SelectedProject)).EpsonServer.Client.Write("SET|NEWBLISTER");
        }

        void EpsonServer_Disconnected(object sender, IOModule.TCPServerEventArgs e) {
            if (((EpsonProject)(EpsonSettings.SelectedProject)).EpsonAndroidServer.Client != null) {
                ((EpsonProject)(EpsonSettings.SelectedProject)).EpsonAndroidServer.Client.Write("STATUS|NOTCONNECTED");
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
            if (((EpsonProject)(EpsonSettings.SelectedProject)).EpsonAndroidServer.Client != null) {
                ((EpsonProject)(EpsonSettings.SelectedProject)).EpsonAndroidServer.Client.Write("STATUS|CONNECTED");
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

        public EpsonSettings EpsonSettings = null;
        public EpsonProjects EpsonProjectsConfig = null;        

        //public string _epsonfile { get; set; }

        private String ModuleName;

        public void InitModule(String moduleName, String epsonSettingsFile) {
            ModuleName = moduleName;          

            try {


                m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);


                Thread splashthread = new Thread(new ThreadStart(SplashScreen.ShowSplashScreen));
                splashthread.IsBackground = true;
                splashthread.Start();
                //TODO splash screen
                Thread.Sleep(100);
                SplashScreen.UdpateStatusTextWithStatus("[" + ModuleName + "] - " + this.GetResourceText("SplashScreen_0"), TypeOfMessage.Success);
                Thread.Sleep(100);


                this.Text = ModuleName;



                if (!File.Exists(epsonSettingsFile)) {
                    EpsonSettings.WriteConfiguration(new EpsonSettings(), epsonSettingsFile);
                }

                EpsonSettings = EpsonSettings.ReadConfigurationFile(epsonSettingsFile);
                EpsonSettings.BackupExtention = ".bkp";
                EpsonSettings.BackupFilesToKeep = 5;
                EpsonSettings.BackupFolderName = "Backup";

                EpsonSettings.DockFile = Path.Combine(Path.GetDirectoryName(epsonSettingsFile), ModuleName + ".dock");

                if (File.Exists(EpsonSettings.DockFile))
                    try {
                        __dockPanel1.LoadFromXml(EpsonSettings.DockFile, m_deserializeDockContent);
                    }
                    catch (Exception exp) {

                        __dockPanel1.SaveAsXml(EpsonSettings.DockFile);

                    }
                else {


                }

                if (_EpsonConfigForm.Visible == false) {
                    _EpsonConfigForm.Show(__dockPanel1);
                }
                if (_EpsonStatusForm.Visible == false) {
                    _EpsonStatusForm.Show(__dockPanel1);
                }




                _EpsonStatusForm.__btNewInsp.Click += new EventHandler(__btNewInsp_Click);

                #region project forms init



                SplashScreen.UdpateStatusTextWithStatus("[" + ModuleName + "] - " + this.GetResourceText("SplashScreen_1"), TypeOfMessage.Success);


                #endregion


                LoadProjectsFromFile(EpsonSettings.ProjectFile);


                AcessManagement.OnAcesslevelChanged += new AcessManagement.AcesslevelChanged(AcessManagement_OnAcesslevelChanged);

                AcessManagement_OnAcesslevelChanged(AcessManagement.AcessLevel);

                this.CloseButtonVisible = false;
                _EpsonConfigForm.CloseButtonVisible = false;
                _EpsonStatusForm.CloseButtonVisible = false;




                _EpsonConfigForm.__propertyGridEpson.SelectedObject = EpsonSettings.SelectedProject;
                if (EpsonSettings.SelectedProject != null) {

                    ((EpsonProject)(EpsonSettings.SelectedProject)).EpsonServer.Connected += new IOModule.TCPServer.TcpServerEventDlgt(EpsonServer_Connected);
                    ((EpsonProject)(EpsonSettings.SelectedProject)).EpsonServer.Disconnected += new IOModule.TCPServer.TcpServerEventDlgt(EpsonServer_Disconnected);
                    ((EpsonProject)(EpsonSettings.SelectedProject)).EpsonServer.ServerClientMessage += new IOModule.TCPServer.TcpServerClientMessageEvent(EpsonServer_ServerClientMessage);
                    ((EpsonProject)(EpsonSettings.SelectedProject)).EpsonServer.StartListening();

                    ((EpsonProject)(EpsonSettings.SelectedProject)).EpsonAndroidServer.Connected += new IOModule.TCPServer.TcpServerEventDlgt(EpsonAndroidServer_Connected);
                    ((EpsonProject)(EpsonSettings.SelectedProject)).EpsonAndroidServer.Disconnected += new IOModule.TCPServer.TcpServerEventDlgt(EpsonAndroidServer_Disconnected);
                    ((EpsonProject)(EpsonSettings.SelectedProject)).EpsonAndroidServer.ServerClientMessage += new IOModule.TCPServer.TcpServerClientMessageEvent(EpsonAndroidServer_ServerClientMessage);
                    ((EpsonProject)(EpsonSettings.SelectedProject)).EpsonAndroidServer.StartListening();

                    ((EpsonProject)(EpsonSettings.SelectedProject)).OnEpsonStatusChanged += new EpsonProject.EpsonStatusChanged(Epson_OnEpsonStatusChanged);
                    _EpsonConfigForm.SelectedProject = ((EpsonProject)(EpsonSettings.SelectedProject));

                }
                SplashScreen.UdpateStatusText("[" + ModuleName + "] - " +this.GetResourceText("SplashScreen_3"));


            }
            catch (Exception exp) {
                log.Error(exp);

                SplashScreen.UdpateStatusText(this.GetResourceText("[" + ModuleName + "] - " +"SplashScreen_4"));



            }
            Thread.Sleep(500);

            SplashScreen.CloseSplashScreen();
        }



        public void LoadProjectsFromFile(String newpath) {

            String appath = AppDomain.CurrentDomain.BaseDirectory;


            Uri fullPath = new Uri(new Uri(appath), newpath);

            String thefilepath = fullPath.LocalPath;// +Path.GetFileName(newpath);

            log.Status("Loading projects file : " + thefilepath);


            if (File.Exists(thefilepath)) {


                if (EpsonProjectsConfig != null) {
                    CloseCurrentConfiguration(true);
                }


                EpsonProjectsConfig = EpsonProjects.ReadConfigurationFile(thefilepath);

                EpsonProjectsConfig.BackupExtention = ".bkp";
                EpsonProjectsConfig.BackupFilesToKeep = 5;
                EpsonProjectsConfig.BackupFolderName = "Backup";

                log.Status("Projects file loaded");

                if (EpsonProjectsConfig.Projects.Count == 1) {
                    LoadProject(EpsonProjectsConfig.Projects[0].Name);
                }
            } else {
                log.Info("Projects file not found");
            }
        }


        private Boolean LoadProject(String ProjectName) {

            if (this.InvokeRequired) {
                Boolean ok = false;
                this.Invoke(new MethodInvoker(delegate {
                    ok = LoadProject(ProjectName);
                }));
                return ok;
            } else {
                try {


                    int prognumber = -1;
                    EpsonProject newselected = null;

                    if (int.TryParse(ProjectName, out prognumber)) {
                        newselected = EpsonProjectsConfig.Projects.Find(projname => projname.ProjectID == prognumber);
                    } else {
                        newselected = EpsonProjectsConfig.Projects.Find(projname => projname.Name == ProjectName);
                    }

                    if (newselected != null) {

                        if (((EpsonProject)(EpsonSettings.SelectedProject)) != null) {


                            if (newselected.Name == ((EpsonProject)(EpsonSettings.SelectedProject)).Name) {
                                return true;
                            } else {
                                CloseCurrentConfiguration(true);
                            }

                        }

                        EpsonSettings.SelectedProject = newselected;


                        this.Text = ModuleName + " : " + ((EpsonProject)(EpsonSettings.SelectedProject)).Name;

                    }
                    return true;
                } catch (Exception exp) {

                    log.Error(exp);
                    return false;
                }

            }


        }


        private void CloseCurrentConfiguration(bool overrideclose) {

            try {

                if (((EpsonProject)(EpsonSettings.SelectedProject)) != null) {




                    this.Text = ModuleName;



                    ((EpsonProject)(EpsonSettings.SelectedProject)).Dispose();


                    EpsonSettings.SelectedProject = null;



                }


            } catch (Exception exp) {


                log.Error(exp);
            }

        }

        void AcessManagement_OnAcesslevelChanged(Acesslevel NewLevel) {
            Boolean state = (NewLevel == Acesslevel.Admin || NewLevel == Acesslevel.Man);
           

            switch (NewLevel) {
                case Acesslevel.Admin:

                    break;
                case Acesslevel.Man:

                    break;
                case Acesslevel.User:

                    break;
                default:
                    break;
            }
        }


        internal static class SplashScreen {
            static SplashScreenForm sf = null;

            static Point _WindowLocation = new Point(0, 0);

            internal static Point WindowLocation {
                get {
                    return SplashScreen._WindowLocation;
                }
                set {

                    SplashScreen._WindowLocation = value;
                    if (sf != null) {
                        sf.UpdateLocation(value);

                    }


                }
            }

            /// <summary>
            /// Displays the splashscreen
            /// </summary>
            internal static void ShowSplashScreen() {
                if (sf == null) {
                    sf = new SplashScreenForm();
                    sf.Load += new EventHandler(sf_Load);
                    sf.ShowSplashScreen();



                }
            }

            static void sf_Load(object sender, EventArgs e) {
                WindowLocation = sf.Location;
            }

            /// <summary>
            /// Closes the SplashScreen
            /// </summary>
            internal static void CloseSplashScreen() {
                if (sf != null) {
                    sf.CloseSplashScreen();
                    sf = null;
                }
            }

            /// <summary>
            /// Update text in default green color of success message
            /// </summary>
            /// <param name="Text">Message</param>
            internal static void UdpateStatusText(string Text) {
                if (sf != null)
                    sf.UdpateStatusText(Text);

            }

            /// <summary>
            /// Update text with message color defined as green/yellow/red/ for success/warning/failure
            /// </summary>
            /// <param name="Text">Message</param>
            /// <param name="tom">Type of Message</param>
            internal static void UdpateStatusTextWithStatus(string Text, TypeOfMessage tom) {

                if (sf != null)
                    sf.UdpateStatusTextWithStatus(Text, tom);
            }
        }


        private void EpsonMainForm_Load(object sender, EventArgs e) {
           
            
            
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
            ((EpsonProject)(EpsonSettings.SelectedProject)).EpsonServer.Client.Write("NEWINSP|START");
        }

        private void EpsonMainForm_FormClosing(object sender, FormClosingEventArgs e) {
            try {

                if (File.Exists(EpsonSettings.DockFile)) {
                    __dockPanel1.SaveAsXml(EpsonSettings.DockFile);    
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
                ((EpsonProject)(EpsonSettings.SelectedProject)).EpsonServer.Client.Write("SET|OPERATION|START");                
            } catch (Exception exp) {

                log.Error(exp);
            }
        }

        private void __btStop_Click(object sender, EventArgs e) {
            try {
                __btStop.Enabled = false;
                __btStop.Text=this.GetResourceText("OpenVisionSystem.Resources.Language.Epson", "__btStopStopping");
                __timerParagem.Enabled = true;
                ((EpsonProject)(EpsonSettings.SelectedProject)).EpsonServer.Client.Write("SET|OPERATION|STOP");
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


        public bool Restart { get; set; }
    }
}
