using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.Sconit.Entity.Exception
{
    public class BusinessErrorException : ApplicationException
    {
        private string[] messageParams;
        public string[] MessageParams
        {
            get
            {
                return messageParams;
            }
        }

        public BusinessErrorException()
            : base()
        { 
        }

        public BusinessErrorException(string message)
            : base(message)
        {
        }

        public BusinessErrorException(string message, System.Exception inner)
            : base(message, inner)
        { 
        }

        public BusinessErrorException(string message, params string[] messageParams)
            : base(message)
        {
            this.messageParams = messageParams;
        }

        public BusinessErrorException(string message, System.Exception inner, params string[] messageParams)
            : base(message, inner)
        {
            this.messageParams = messageParams;
        }
    }
}
