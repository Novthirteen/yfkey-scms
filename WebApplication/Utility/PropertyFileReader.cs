using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace com.Sconit.Utility
{
    public class PropertyFileReader : IDisposable
    {
        #region Private variables

        private Stream stream;
        private StreamReader reader;

        public PropertyFileReader(Stream s) : this(s, null) { }

        public PropertyFileReader(Stream s, Encoding enc)
        {            
            this.stream = s;
            if (!s.CanRead)
            {
                throw new PropertyFileReaderException("Could not read the given property file stream!");
            }
            reader = (enc != null) ? new StreamReader(s, enc) : new StreamReader(s);
        }

        public PropertyFileReader(string filename) : this(filename, null) { }

        public PropertyFileReader(string filename, Encoding enc)
            : this(new FileStream(filename, FileMode.Open), enc) { }

        public bool EndOfStream
        {
            get
            {
                return reader.EndOfStream;
            }
        }

        public string[] GetPropertyLine()
        {
            string data = reader.ReadLine();
            if (data == null) return null;
            if (data.Length == 0) return null;

            string[] result = data.Split('=');

            if (result.Length != 2)
            {
                //不合法的property，忽略
                return null;
            }

            return result;
        }

        public void Dispose()
        {
            // Closing the reader closes the underlying stream, too
            if (reader != null) reader.Close();
            else if (stream != null)
                stream.Close(); // In case we failed before the reader was constructed
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class PropertyFileReaderException : ApplicationException
    {
        public PropertyFileReaderException(string message) : base(message) { }
    }
}
