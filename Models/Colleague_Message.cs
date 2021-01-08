using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMWebAPI.Models
{
    public class Colleague_Message
    {
        //public Colleague Colleague { get; set; }
        //public int ColleagueID { get; set; }
        //public Message_Chain Message_Chain { get; set; }
        //public int MessageChainID { get; set; }

        public int ColleagueId { get; set; }
        public int MessageChainId { get; set; }

        public Colleague Colleague { get; set; }
        public Message_Chain Message_Chain { get; set; }
    }
}
