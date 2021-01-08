using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMWebAPI.Models
{
    public class Message_Chain
    {
        [Key]
        public int MessageChainId { get; set; }
        public int MessageSubjectId { get; set; }
        public string MessageBody { get; set; }
        public int SentFromId { get; set; }
        public DateTime DateTime { get; set; }

        public Message_Subject Message_Subject { get; set; }
        public ICollection<Colleague_Message> Colleague_Message { get; set; }
    }
}
