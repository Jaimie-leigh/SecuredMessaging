using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace SMWebAPI.Models
{
    public class Broker
    {
        //public int BrokerID { get; set; }
        //public string BrokerForename { get; set; }
        //public string BrokerSurname { get; set; }

        //public Broker()
        //{
        //    Application = new HashSet<Application>();
        //}

        public int BrokerId { get; set; }
        public string BrokerForename { get; set; }
        public string BrokerSurname { get; set; }

        public ICollection<Application> Application { get; set; }
       // [WriteOnlyArray]
        //public virtual ICollection<Application> Application { get; set; }

    }
}
