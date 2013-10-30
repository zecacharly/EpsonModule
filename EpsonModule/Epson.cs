using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.ComponentModel;
using IOModule;
using System.Resources;
using System.Threading;
using KPP.Core.Debug;
using System.IO;

namespace EpsonModule {


    public enum LanguageName { Unk, PT, EN }


    public enum Acesslevel { Admin, User, NotSet, Man }


    public static class StaticObjects {

        public delegate void AcesslevelChanged(Acesslevel NewLevel);
        public static event AcesslevelChanged OnAcesslevelChanged;




        private static Acesslevel _acessLevel = Acesslevel.NotSet;

        public static Acesslevel AcessLevel {
            get { return _acessLevel; }
            set {
                if (_acessLevel != value) {
                    _acessLevel = value;
                    if (OnAcesslevelChanged != null) {
                        OnAcesslevelChanged(value);
                    }
                }
            }
        }


        public static string GetResourceText(this Object from, String ResVar) {
            return GetResourceText(from, "OpenVisionSystem.Resources.Language.Res", ResVar);
        }

        public static string GetResourceText(this Object from, String ResLocation, String ResVar) {
            try {
                //ComponentResourceManager resources = new ComponentResourceManager(Program.);
                ResourceManager res_man = new ResourceManager(ResLocation, from.GetType().Assembly);
                return res_man.GetString(ResVar, Thread.CurrentThread.CurrentUICulture);
            }
            catch (Exception exp) {

                return "Error getting resource";
            }
        }

    }

    public enum EpsonStatus { Stopped, Started, Maintenance }

    public class PalleteDefinition {

        private string m_Point1 = "";
        [XmlAttribute, Browsable(false)]
        public string Point1 {
            get { return m_Point1; }
            set {
                if (m_Point1 != value) {
                    m_Point1 = value;
                }
            }
        }
        private string m_Point2 = "";
        [XmlAttribute, Browsable(false)]
        public string Point2 {
            get { return m_Point2; }
            set {
                if (m_Point2 != value) {
                    m_Point2 = value;
                }
            }
        }

        private string m_Point3 = "";
        [XmlAttribute, Browsable(false)]
        public string Point3 {
            get { return m_Point3; }
            set {
                if (m_Point3 != value) {
                    m_Point3 = value;
                }
            }
        }

        private string m_Point4 = "";
        [XmlAttribute, Browsable(false)]
        public string Point4 {
            get { return m_Point4; }
            set {
                if (m_Point4 != value) {
                    m_Point4 = value;
                }
            }
        }

        private int m_PalleteNumber = 1;
        [XmlAttribute, Browsable(false)]
        public int PalleteNumber {
            get { return m_PalleteNumber; }
            set {
                if (m_PalleteNumber != value) {
                    m_PalleteNumber = value;
                }
            }

        }

        private int m_PalleteLines = 1;
        [XmlAttribute, Browsable(false)]
        public int PalleteLines {
            get { return m_PalleteLines; }
            set {
                if (m_PalleteLines != value) {
                    m_PalleteLines = value;
                }
            }

        }

        private int m_PalleteCol = 1;
        [XmlAttribute, Browsable(false)]
        public int PalleteCol {
            get { return m_PalleteCol; }
            set {
                if (m_PalleteCol != value) {
                    m_PalleteCol = value;
                }
            }

        }

        public PalleteDefinition() {

        }
    }

    public class EpsonModule {

        private String _Name = "Epson Module";
        [XmlAttribute, ReadOnly(true)]
        public String Name {
            get { return _Name; }
            set { _Name = value; }
        }

        private TCPServer _EpsonServer = new TCPServer();
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("TCP Server")]
        public TCPServer EpsonServer {
            get { return _EpsonServer; }
            set {
                if (_EpsonServer != value) {
                    _EpsonServer = value;
                }
            }
        }

        private TCPServer _EpsonAndroidServer = new TCPServer();
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("TCP Android Server")]
        public TCPServer EpsonAndroidServer {
            get { return _EpsonAndroidServer; }
            set {
                if (_EpsonAndroidServer != value) {
                    _EpsonAndroidServer = value;
                }
            }
        }

        private PalleteDefinition m_Pallete = new PalleteDefinition();
        [Browsable(false)]
        public PalleteDefinition Pallete {
            get { return m_Pallete; }
            set { m_Pallete = value; }
        }

        public void Dispose() {
            EpsonServer.StopListening();
            EpsonAndroidServer.StopListening();
        }

        public override string ToString() {
            return Name;
        }



        public delegate void EpsonStatusChanged(EpsonStatus NewStatus);
        public event EpsonStatusChanged OnEpsonStatusChanged;

        private EpsonStatus m_Status = EpsonStatus.Stopped;
        [ReadOnly(true), XmlIgnore]
        public EpsonStatus Status {
            get { return m_Status; }
            set {
                if (m_Status != value) {
                    m_Status = value;
                    if (OnEpsonStatusChanged != null) {
                        OnEpsonStatusChanged(value);
                    }
                }
            }
        }




        private static LanguageName _Language = LanguageName.PT;

        public static LanguageName Language {
            get { return _Language; }
            set {
                if (_Language != value) {
                    _Language = value;
                    switch (value) {
                        case LanguageName.Unk:
                            break;
                        case LanguageName.PT:
                            break;
                        case LanguageName.EN:

                            break;
                        default:
                            break;
                    }
                }
            }
        }
        

        public EpsonModule() {

        }
    }


    public sealed class EpsonModuleSettings {

        #region -  Serialization attributes  -

        public static Int32 S_BackupFilesToKeep = 5;
        public static String S_BackupFolderName = "backup";
        public static String S_BackupExtention = "bkp";
        public static String S_DefaulFileExtention = "xml";

        private String _filePath = null;
        private String _defaultPath = null;

        [XmlIgnore]
        public Int32 BackupFilesToKeep { get; set; }
        [XmlIgnore]
        public String BackupFolderName { get; set; }
        [XmlIgnore]
        public String BackupExtention { get; set; }

        #endregion
        private static KPPLogger log = new KPPLogger(typeof(EpsonModuleSettings));

        [XmlAttribute]
        public String Name { get; set; }





        public EpsonModule Epson { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public EpsonModuleSettings() {
            Name = "Epson Settings";

            Epson = new EpsonModule();


        }


        //    StaticObjects.ListInspections.Add(item);

        #region Read Operations

        /// <summary>
        /// Reads the configuration.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static EpsonModuleSettings ReadConfigurationFile(string path) {
            //log.Debug(String.Format("Load Xml file://{0}", path));
            if (File.Exists(path)) {
                EpsonModuleSettings result = null;
                TextReader reader = null;

                try {
                    XmlSerializer serializer = new XmlSerializer(typeof(EpsonModuleSettings));
                    reader = new StreamReader(path);


                    EpsonModuleSettings config = serializer.Deserialize(reader) as EpsonModuleSettings;
             
                    config._filePath = path;

                    result = config;
                   
                }
                catch (Exception exp) {
                    log.Error(exp);
                }
                finally {
                    if (reader != null) {
                        reader.Close();
                    }
                }
                return result;
            }
            return null;
        }

        /// <summary>
        /// Reads the configuration.
        /// </summary>
        /// <param name="childtype">The childtype.</param>
        /// <param name="xmlString">The XML string.</param>
        /// <returns></returns>
        public static EpsonModuleSettings ReadConfigurationString(string xmlString) {
            try {
                XmlSerializer serializer = new XmlSerializer(typeof(EpsonModuleSettings));
                EpsonModuleSettings config = serializer.Deserialize(new StringReader(xmlString)) as EpsonModuleSettings;

                return config;
            }
            catch (Exception exp) {
                log.Error(exp);
            }
            return null;
        }

        #endregion

        #region Write Operations

        /// <summary>
        /// Writes the configuration.
        /// </summary>
        public void WriteConfigurationFile() {
            if (_filePath != null) {

                WriteConfigurationFile(_filePath);

            }
        }

        /// <summary>
        /// Writes the configuration.
        /// </summary>
        /// <param name="path">The path.</param>
        public void WriteConfigurationFile(string path) {

            WriteConfiguration(this, path, BackupFolderName, BackupExtention, BackupFilesToKeep);

        }

        /// <summary>
        /// Writes the configuration string.
        /// </summary>
        /// <returns></returns>
        public String WriteConfigurationToString() {

            return WriteConfigurationToString(this);
        }

        /// <summary>
        /// Writes the configuration.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <param name="path">The path.</param>
        public static void WriteConfiguration(EpsonModuleSettings config, string path) {
            WriteConfiguration(config, path, S_BackupFolderName, S_BackupExtention, S_BackupFilesToKeep);
        }

        /// <summary>
        /// Writes the configuration.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <param name="path">The path.</param>
        public static void WriteConfiguration(EpsonModuleSettings config, string path, string backupFolderName, String backupExtention, Int32 backupFilesToKeep) {
            if (File.Exists(path) && backupFilesToKeep > 0) {
                //Do a file backup prior to overwrite
                try {
                    //Check if valid backup folder name
                    if (backupFolderName == null || backupFolderName.Length == 0) {
                        backupFolderName = "backup";
                    }

                    //Check Backup folder
                    String bkpFolder = Path.Combine(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Config"), backupFolderName);
                    if (!Directory.Exists(bkpFolder)) {
                        Directory.CreateDirectory(bkpFolder);
                    }

                    //Check extention
                    String ext = backupExtention != null && backupExtention.Length > 0 ? backupExtention : Path.GetExtension(path);
                    if (!ext.StartsWith(".")) { ext = String.Format(".{0}", ext); }

                    //Delete existing backup file (This should not exist)
                    String bkpFile = Path.Combine(bkpFolder, String.Format("{0}_{1:yyyyMMddHHmmss}{2}", Path.GetFileNameWithoutExtension(path), DateTime.Now, ext));
                    if (File.Exists(bkpFile)) { File.Delete(bkpFile); }

                    //Delete excess backup files
                    String fileSearchPattern = String.Format("{0}_*{1}", Path.GetFileNameWithoutExtension(path), ext);
                    String[] bkpFilesList = Directory.GetFiles(bkpFolder, fileSearchPattern, SearchOption.TopDirectoryOnly);
                    if (bkpFilesList != null && bkpFilesList.Length > (backupFilesToKeep - 1)) {
                        bkpFilesList = bkpFilesList.OrderByDescending(f => f.ToString()).ToArray();
                        for (int i = (backupFilesToKeep - 1); i < bkpFilesList.Length; i++) {
                            File.Delete(bkpFilesList[i]);
                        }
                    }

                    //Backup current file
                    File.Copy(path, bkpFile);
                    //log.Debug(String.Format("Backup file://{0} to file://{1}", path, bkpFile));
                }
                catch (Exception exp) {
                    //log.Error(String.Format("Error copying file {0} to backup.", path), exp);
                }
            }
            try {
              
                XmlSerializer serializer = new XmlSerializer(config.GetType());
                TextWriter textWriter = new StreamWriter(path);
                serializer.Serialize(textWriter, config);
                textWriter.Close();
           
                //log.Debug(String.Format("Write Xml file://{0}", path));
            }
            catch (Exception exp) {
                log.Error("Error writing configuration. ", exp);
           
                Console.WriteLine(exp.ToString());
            }
        }

        /// <summary>
        /// Writes the configuration to string.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <returns></returns>
        public static String WriteConfigurationToString(EpsonModuleSettings config) {
            try {
                XmlSerializer serializer = new XmlSerializer(config.GetType());
                StringWriter stOut = new StringWriter();
          
                serializer.Serialize(stOut, config);
          
                return stOut.ToString();
            }
            catch (Exception exp) {
                //log.Error("Error writing configuration. ", exp);
            
            }
            return null;
        }

        #endregion
    }


}
