using System;
using System.Collections.Generic;

namespace SMWebAPI
{
    public partial class Application
    {
        public Application()
        {
            MessageSubject = new HashSet<MessageSubject>();
        }

        public int RollNumber { get; set; }
        public string CustomerFullName { get; set; }
        public int BrokerId { get; set; }

        public virtual Broker Broker { get; set; }
        public virtual ICollection<MessageSubject> MessageSubject { get; set; }
    }
}
