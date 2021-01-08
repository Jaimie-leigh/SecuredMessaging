using System;
using System.Collections.Generic;

namespace SMWebAPI
{
    public partial class ColleagueMessage
    {
        public int ColleagueId { get; set; }
        public int MessageChainId { get; set; }

        public virtual Colleague Colleague { get; set; }
        public virtual MessageChain MessageChain { get; set; }
    }
}
