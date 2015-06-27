using System;
using System.Collections.Generic;
using System.Text;

namespace pssst.Client.Model
{
    public sealed class Message
    {
        public string Sender { get; set; }
        public string Text { get; set; }
        public DateTime Sent { get; set; }
    }
}
