using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMWebAPI.Models
{
    public class Application
    {
        //public string CustomerFullName { get; set; }
        //[Key]
        //public int RollNumber { get; set; }

        //public Broker Broker { get; set; }
        //public int BrokerID { get; set; }

        //public Application()
        //{
        //    Message_Subject = new HashSet<Message_Subject>();
        //}

        public int RollNumber { get; set; }
        public string CustomerFullName { get; set; }
        public int BrokerId { get; set; }

        public Broker Broker { get; set; }
        // public ICollection<Broker> Broker { get; set; }
        public ICollection<Message_Subject> Message_Subject { get; set; }
    }

}

