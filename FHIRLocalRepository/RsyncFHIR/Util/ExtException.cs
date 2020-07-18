using System;
using System.Collections.Generic;
using System.Text;

namespace RsyncFHIR.Util
{

    [Serializable()]
    public class RebootException : System.Exception
    {
        public Exception BaseException { get; set; }
        public RebootException() : base() { BaseException = null; }
        public RebootException(Exception ex) : base()
        {
            this.BaseException = ex;
        }
    }
}
