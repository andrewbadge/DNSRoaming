using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNS_Roaming_Common
{
    [Serializable]
    class ActionForService
    {
        public string Action;
    }

    [Serializable]
    class ServiceStatus
    {
        public string ServiceState;
        public string ActiveNetwork;
    }

}
