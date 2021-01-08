using System;
using System.Collections.Generic;

namespace SMWebAPI
{
    public partial class Broker
    {
        public Broker()
        {
            Application = new HashSet<Application>();
        }

        public int BrokerId { get; set; }
        public string BrokerForename { get; set; }
        public string BrokerSurname { get; set; }

        public virtual ICollection<Application> Application { get; set; }
    }
}
