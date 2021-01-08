using System;
using System.Collections.Generic;

namespace SMWebAPI
{
    public partial class MessageChain
    {
        public MessageChain()
        {
            ColleagueMessage = new HashSet<ColleagueMessage>();
        }

        public int MessageChainId { get; set; }
        public int MessageSubjectId { get; set; }
        public string MessageBody { get; set; }
        public int SentFromId { get; set; }
        public DateTime DateTime { get; set; }

        public virtual MessageSubject MessageSubject { get; set; }
        public virtual ICollection<ColleagueMessage> ColleagueMessage { get; set; }
    }
}
