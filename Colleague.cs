using System;
using System.Collections.Generic;

namespace SMWebAPI
{
    public partial class Colleague
    {
        public Colleague()
        {
            ColleagueMessage = new HashSet<ColleagueMessage>();
        }

        public int ColleagueId { get; set; }
        public string ColleagueForename { get; set; }
        public string ColleagueSurname { get; set; }

        public virtual ICollection<ColleagueMessage> ColleagueMessage { get; set; }
    }
}
