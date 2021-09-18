using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace DNS_Roaming_Common
{
    [Serializable]
    public class DNSRoamingOption
    {
        private string optionFileNameFullPath;

        #region Properties

        private bool disableIPV6;
        public bool DisableIPV6
        {
            get { return disableIPV6; }
            set { disableIPV6 = value; }
        }

        #endregion

        public DNSRoamingOption()
        {
            PopulateOptionsFilename();
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
                StreamWriter w = new StreamWriter(optionFileNameFullPath);
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
                File.Delete(optionFileNameFullPath);
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

                if (File.Exists(optionFileNameFullPath))
                {
                    System.IO.FileInfo fileInfo = new FileInfo(optionFileNameFullPath);
                    if (fileInfo.Length == 0)
                        validFile = false;
                }
                else
                    validFile = false;

                if (validFile)
                {
                    StreamReader sr = new StreamReader(optionFileNameFullPath);
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

        private void PopulateOptionsFilename()
        {
            PathsandData pathsandData = new PathsandData();
            optionFileNameFullPath = String.Format(@"{0}\Options.xml", pathsandData.BaseOptionsPath);
        }

        #endregion

    }
}
