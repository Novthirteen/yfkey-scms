using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeanEngine.Entity
{
    public class BusinessException : ApplicationException
    {
        private string[] messageParams;
        public string[] MessageParams
        {
            get
            {
                return messageParams;
            }
        }

        public BusinessException()
            : base()
        {
        }

        public BusinessException(string message)
            : base(message)
        {
        }

        public BusinessException(string message, System.Exception inner)
            : base(message, inner)
        {
        }

        public BusinessException(string message, params string[] messageParams)
            : base(message)
        {
            this.messageParams = messageParams;
        }

        public BusinessException(string message, System.Exception inner, params string[] messageParams)
            : base(message, inner)
        {
            this.messageParams = messageParams;
        }
    }
}
