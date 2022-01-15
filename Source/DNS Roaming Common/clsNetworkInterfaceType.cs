using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace DNS_Roaming_Common
{
    [Serializable]
    public class DNSRoamingNetworkInterfaceType
    {
        #region Properties

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        #endregion

        #region Contructors

        public DNSRoamingNetworkInterfaceType()
        {

        }

        public DNSRoamingNetworkInterfaceType(int newID, string newName)
        {
            id = newID;
            name = newName;
        }

        #endregion
    }

    public static class DNSRoamingNetworkInterfaces
    {
        private static IList<DNSRoamingNetworkInterfaceType> NetworkInterfaceTypes;

        public static IList<DNSRoamingNetworkInterfaceType> GetNetworkInterfaceTypes()
        {
            return NetworkInterfaceTypes;
        }

        public static void IntialiseNetworkInterfaceTypes()
        {
            Logger.Debug("IntialiseNetworkInterfaceTypes");

            try
            {
                PathsandData pathsandData = new PathsandData();

                string filename = Path.Combine(pathsandData.BaseOptionsPath, "CustomNetworkInterfaceTypes.xml");
                XmlSerializer serializer = new XmlSerializer(typeof(List<DNSRoamingNetworkInterfaceType>));

                //Create the Default custom types file if it doesn't exist.
                //Reason is not just deployed is so a user's edit is not ovewritten
                if (!File.Exists(filename)) CreateCustomNetworkInterfaceTypesXMLFile(filename);

                //Check if it exists in case there is a permisions issue
                if (File.Exists(filename))
                {
                    using (FileStream stream = File.OpenRead(filename))
                    {
                        NetworkInterfaceTypes = (List<DNSRoamingNetworkInterfaceType>)serializer.Deserialize(stream);
                    }
                }
                else
                    Logger.Warn("CustomNetworkInterfaceTypes.xml was not found.");


            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        public static void CreateCustomNetworkInterfaceTypesXMLFile(string filename)
        {
            Logger.Debug("CreateCustomNetworkInterfaceTypesXMLFile");

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<DNSRoamingNetworkInterfaceType>));

                //Saving a new List
                using (FileStream stream = File.OpenWrite(filename))
                {
                    List<DNSRoamingNetworkInterfaceType> list = new List<DNSRoamingNetworkInterfaceType>();

                    //Add the custom type
                    DNSRoamingNetworkInterfaceType newType = new DNSRoamingNetworkInterfaceType(53, "Proprietary Virtual (VPN)");
                    list.Add(newType);

                    //Save it to the XML file
                    serializer.Serialize(stream, list);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        /// <summary>
        /// Compare the Custom Network Interface Type to the value the NIC will return
        /// </summary>
        /// <param name="currentNetworkType"></param>
        /// <param name="customNetworkType"></param>
        /// <returns></returns>
        public static bool MatchCustomNetworkInterfaceType(string currentNetworkType, string customNetworkType)
        {
            bool networkTypeMatched = false;

            foreach (DNSRoamingNetworkInterfaceType interfaceType in NetworkInterfaceTypes)
            {
                if (customNetworkType == interfaceType.Name && currentNetworkType == interfaceType.ID.ToString())
                {
                    networkTypeMatched = true;
                    break;
                }
            }

            return networkTypeMatched;
        }

        /// <summary>
        /// Take the Network Type the Interface describes and translate it to text
        /// Parse the ID into the DNSRoamingNetworkInterfaceType load from file
        /// </summary>
        /// <param name="networkType"></param>
        /// <returns></returns>
        public static string FormatNetworkInterfaceType(string networkType)
        {
            string formattedNetworkType = string.Empty;

            int networkTypeID;
            if (int.TryParse(networkType, out networkTypeID))
            {
                //A Type ID. Find the matching Network Type
                foreach (DNSRoamingNetworkInterfaceType interfaceType in NetworkInterfaceTypes)
                {
                    if (networkTypeID == interfaceType.ID)
                    {
                        formattedNetworkType = interfaceType.Name;
                        break;
                    }
                }
            }
            else
                //A descriptive Name
                formattedNetworkType = networkType;

            //If not a Descriptive name or an ID that matches the custom types
            if (formattedNetworkType == string.Empty) formattedNetworkType = string.Format("Unknown Type (ID:{0})", networkType);

            return formattedNetworkType;
        }
    }
}
