using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.Sconit.Entity.Exception
{
    public class TechnicalException : ApplicationException
    {
        public TechnicalException()
            : base()
        {
        }

        public TechnicalException(string message)
            : base(message)
        {
        }

        public TechnicalException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}
