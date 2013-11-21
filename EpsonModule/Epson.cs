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
using KPPAutomationCore;
using DejaVu;

namespace EpsonModule {

       

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



    public class EpsonProject:ModuleProject {


        private TCPServer m_EpsonAndroidServer;
        [DisplayName("Epson Android Server")]
        public TCPServer EpsonAndroidServer {
            get { return m_EpsonAndroidServer; }
            set { m_EpsonAndroidServer = value; }
        }

        private TCPServer m_EpsonServer;
        [DisplayName("Epson TCP Server")]
        public TCPServer EpsonServer {
            get { return m_EpsonServer; }
            set { m_EpsonServer = value; }
        }

        private PalleteDefinition m_Pallete = new PalleteDefinition();
        [Browsable(false)]
        public PalleteDefinition Pallete {
            get { return m_Pallete; }
            set { m_Pallete = value; }
        }

        public void Dispose() {
           
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




      

        public EpsonProject() {
            Name = "Epson Project";
        }
    }


    public sealed class EpsonProjects {

        #region -  Serialization attributes  -

        internal static Int32 S_BackupFilesToKeep = 5;
        internal static String S_BackupFolderName = "backup";
        internal static String S_BackupExtention = "bkp";
        internal static String S_DefaulFileExtention = "xml";

        private String _filePath = null;
        private String _defaultPath = null;

        [XmlIgnore]
        public Int32 BackupFilesToKeep { get; set; }
        [XmlIgnore]
        public String BackupFolderName { get; set; }
        [XmlIgnore]
        public String BackupExtention { get; set; }

        #endregion

        private static KPPLogger log = new KPPLogger(typeof(EpsonProjects));

        [XmlAttribute]
        public String Name { get; set; }

        public string ModuleName {
            get;
            set;
        }



        public List<EpsonProject> Projects { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public EpsonProjects() {
            Name = "Epson Projects";
            Projects = new List<EpsonProject>();
        }


        //    StaticObjects.ListInspections.Add(item);

        #region Read Operations

        /// <summary>
        /// Reads the configuration.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        internal static EpsonProjects ReadConfigurationFile(string path) {
            //log.Debug(String.Format("Load Xml file://{0}", path));
            if (File.Exists(path)) {
                EpsonProjects result = null;
                TextReader reader = null;

                try {
                    XmlSerializer serializer = new XmlSerializer(typeof(EpsonProjects));
                    reader = new StreamReader(path);

                    UndoRedoManager.StartInvisible("Init");

                    EpsonProjects config = serializer.Deserialize(reader) as EpsonProjects;

                    config._filePath = path;

                    result = config;
                    UndoRedoManager.Commit();
                } catch (Exception exp) {
                    log.Error(exp);
                } finally {
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
        internal static EpsonProjects ReadConfigurationString(string xmlString) {
            try {
                XmlSerializer serializer = new XmlSerializer(typeof(EpsonProjects));
                EpsonProjects config = serializer.Deserialize(new StringReader(xmlString)) as EpsonProjects;

                return config;
            } catch (Exception exp) {
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
        internal static void WriteConfiguration(EpsonProjects config, string path) {
            WriteConfiguration(config, path, S_BackupFolderName, S_BackupExtention, S_BackupFilesToKeep);
        }

        /// <summary>
        /// Writes the configuration.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <param name="path">The path.</param>
        internal static void WriteConfiguration(EpsonProjects config, string path, string backupFolderName, String backupExtention, Int32 backupFilesToKeep) {
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
                } catch (Exception exp) {
                    //log.Error(String.Format("Error copying file {0} to backup.", path), exp);
                }
            }
            try {

                XmlSerializer serializer = new XmlSerializer(config.GetType());
                TextWriter textWriter = new StreamWriter(path);
                serializer.Serialize(textWriter, config);
                textWriter.Close();

                //log.Debug(String.Format("Write Xml file://{0}", path));
            } catch (Exception exp) {
                log.Error("Error writing configuration. ", exp);

                Console.WriteLine(exp.ToString());
            }
        }

        /// <summary>
        /// Writes the configuration to string.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <returns></returns>
        internal static String WriteConfigurationToString(EpsonProjects config) {
            try {
                XmlSerializer serializer = new XmlSerializer(config.GetType());
                StringWriter stOut = new StringWriter();

                serializer.Serialize(stOut, config);

                return stOut.ToString();
            } catch (Exception exp) {
                //log.Error("Error writing configuration. ", exp);

            }
            return null;
        }

        #endregion


    }



    public sealed class EpsonSettings:ModuleSettings {

        
        private static KPPLogger log = new KPPLogger(typeof(EpsonSettings));

      
        /// <summary>
        /// 
        /// </summary>
        public EpsonSettings() {
            Name = "Epson Settings";

         


        }


        //    StaticObjects.ListInspections.Add(item);

        #region Read Operations

        /// <summary>
        /// Reads the configuration.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static EpsonSettings ReadConfigurationFile(string path) {
            //log.Debug(String.Format("Load Xml file://{0}", path));            
            if (File.Exists(path)) {
                EpsonSettings result = null;
                TextReader reader = null;

                try {
                    XmlSerializer serializer = new XmlSerializer(typeof(EpsonSettings));
                    reader = new StreamReader(path);


                    EpsonSettings config = serializer.Deserialize(reader) as EpsonSettings;
             
                    config.FilePath= path;

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
        public static EpsonSettings ReadConfigurationString(string xmlString) {
            
            try {
                XmlSerializer serializer = new XmlSerializer(typeof(EpsonSettings));
                EpsonSettings config = serializer.Deserialize(new StringReader(xmlString)) as EpsonSettings;

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
            if (FilePath != null) {

                WriteConfigurationFile(FilePath);

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
        public static void WriteConfiguration(EpsonSettings config, string path) {
            WriteConfiguration(config, path, S_BackupFolderName, S_BackupExtention, S_BackupFilesToKeep);
        }
        
        
        /// <summary>
        /// Writes the configuration.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <param name="path">The path.</param>
        public static void WriteConfiguration(EpsonSettings config, string path, string backupFolderName, String backupExtention, Int32 backupFilesToKeep) {
            
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
        public static String WriteConfigurationToString(EpsonSettings config) {
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
