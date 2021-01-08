using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMWebAPI.Models
{
    public class Message_Subject
    {

        //[Key]
        //public int MessageSubjectID { get; set; }
        //public int BrokerID { get; set; }
        //public string Subject { get; set; }
        //public DateTime DateTime { get; set; }

        //public Application Application { get; set; }
        //public int RollNumber { get; set; }

        //public Message_Subject()
        //{
        //    Message_Chain = new HashSet<Message_Chain>();
        //}

        [Key]
        public int MessageSubjectId { get; set; }
        public int BrokerId { get; set; }
        public string Subject { get; set; }
        public int RollNumber { get; set; }
        public DateTime DateTime { get; set; }

        public Application Application { get; set; }
        public ICollection<Message_Chain> Message_Chain { get; set; }
    }
}
