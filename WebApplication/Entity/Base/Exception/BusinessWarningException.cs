using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.Sconit.Entity.Exception
{
    public class BusinessWarningException : ApplicationException
    {
        private string[] messageParams;
        public string[] MessageParams
        {
            get
            {
                return messageParams;
            }
        }

        public BusinessWarningException()
            : base()
        {
        }

        public BusinessWarningException(string message)
            : base(message)
        {
        }

        public BusinessWarningException(string message, System.Exception inner)
            : base(message, inner)
        {
        }

        public BusinessWarningException(string message, params string[] messageParams)
            : base(message)
        {
            this.messageParams = messageParams;
        }

        public BusinessWarningException(string message, System.Exception inner, params string[] messageParams)
            : base(message, inner)
        {
            this.messageParams = messageParams;
        }
    }
}
