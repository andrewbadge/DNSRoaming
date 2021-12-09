using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace DNS_Roaming_Common
{
    [Serializable]
    public class RuleSetData
    {
        private string ruleSetDataFileNameFullPath;

        #region Properties

        private DateTime autoUpdateLastCheck = DateTime.Now.AddDays(-30);
        public DateTime AutoUpdateLastCheck
        {
            get { return autoUpdateLastCheck; }
            set { autoUpdateLastCheck = value; }
        }

        #endregion

        public RuleSetData()
        {
            PopulateFilename();
        }

        #region Methods

        /// <summary>
        /// Save the objeect as a settings file in the Settings folder
        /// </summary>
        public virtual bool Save()
        {
            bool SaveSuccessful = false;
            try
            {
                //Write the object to the file
                StreamWriter w = new StreamWriter(ruleSetDataFileNameFullPath);
                XmlSerializer s = new XmlSerializer(GetType());
                s.Serialize(w, this);
                w.Close();

                SaveSuccessful = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return SaveSuccessful;

        }

        public virtual void RemoveSaved()
        {
            try
            {
                File.Delete(ruleSetDataFileNameFullPath);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

        }

        public virtual void Load()
        {
            try
            {
                bool validFile = true;

                if (File.Exists(ruleSetDataFileNameFullPath))
                {
                    System.IO.FileInfo fileInfo = new FileInfo(ruleSetDataFileNameFullPath);
                    if (fileInfo.Length == 0)
                        validFile = false;
                }
                else
                    validFile = false;

                if (validFile)
                {
                    StreamReader sr = new StreamReader(ruleSetDataFileNameFullPath);
                    XmlTextReader xr = new XmlTextReader(sr);
                    XmlSerializer xs = new XmlSerializer(GetType());
                    object c;
                    if (xs.CanDeserialize(xr))
                    {
                        c = xs.Deserialize(xr);
                        Type t = GetType();
                        PropertyInfo[] properties = t.GetProperties();
                        foreach (PropertyInfo p in properties)
                        {
                            p.SetValue(this, p.GetValue(c, null), null);
                        }
                    }
                    xr.Close();
                    sr.Close();
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private void PopulateFilename()
        {
            PathsandData pathsandData = new PathsandData();
            ruleSetDataFileNameFullPath = String.Format(@"{0}\RuleSetData.xml", pathsandData.BaseOptionsPath);
        }

        #endregion

    }
}
