using System;
using System.Collections.Generic;
using System.Text;

namespace RsyncFHIR.Util
{
    public class MessageItem
    {
        public int No { get; set; }
        public string? SubDir { get; set; }
        public bool Result { get; set; }
        public DateTime PastTime { get; set; }
        public string? Comment { get; set; }
    }
}
