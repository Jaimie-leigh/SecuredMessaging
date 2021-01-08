using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMWebAPI.Models
{
    public class Colleague
    {
        //[Key]
        //public int ColleagueID { get; set; }
        //public string ColleagueForename { get; set; }
        //public string ColleagueSurname { get; set; }

        //public List<Colleague_Message> Colleague_Message { get; set; }

        //public Colleague()
        //{
        //    Colleague_Message = new HashSet<Colleague_Message>();
        //}

        public int ColleagueId { get; set; }
        public string ColleagueForename { get; set; }
        public string ColleagueSurname { get; set; }

        public ICollection<Colleague_Message> Colleague_Message { get; set; }

    }
}
