using System;
using System.Collections.Generic;

namespace SMWebAPI
{
    public partial class MessageSubject
    {
        public MessageSubject()
        {
            MessageChain = new HashSet<MessageChain>();
        }

        public int MessageSubjectId { get; set; }
        public int BrokerId { get; set; }
        public string Subject { get; set; }
        public int RollNumber { get; set; }
        public DateTime DateTime { get; set; }

        public virtual Application RollNumberNavigation { get; set; }
        public virtual ICollection<MessageChain> MessageChain { get; set; }
    }
}
