using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace com.Sconit.Utility
{
    public class FlatFileWriter
    {
        private TextWriter writer;
        private string newLine;
        private string delimiter;
        private int columnIndex = 0;
        private int rowIndex = 0;

        public FlatFileWriter(TextWriter writer)
            : this(writer, Environment.NewLine, ",")
        {
        }

        public FlatFileWriter(TextWriter writer, string newLine, string delimiter)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            if (newLine == null || newLine == string.Empty)
            {
                throw new ArgumentNullException("newLine");
            }
            if (delimiter == null || delimiter == string.Empty)
            {
                throw new ArgumentNullException("delimiter");
            }
            this.writer = writer;
            this.newLine = newLine;
            this.delimiter = delimiter;
        }

        public void Close()
        {
            if (this.writer != null)
            {
                this.writer.Close();
                this.writer = null;
            }
        }

        public void Dispose()
        {
            if (this.writer != null)
            {
                this.writer.Close();
                this.writer = null;
            }
        }

        public bool IsClosed
        {
            get
            {
                return (this.writer == null);
            }
        }

        public int ColumnIndex
        {
            get
            {
                return this.columnIndex;
            }
        }

        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }

        public void Write(string value)
        {
            if (this.IsClosed)
            {
                throw new ObjectDisposedException("writer");
            }

            string v = "";
            if (value == null)
            {
                value = string.Empty;
            }
            if (value.IndexOf(this.newLine) != -1 || value.IndexOf(this.delimiter) != -1)
            {
                v += value.Replace("\"", "\"\"");
                v = "\"" + v + "\"";
            }
            else
            {
                v += value;
            }
            if (this.columnIndex > 0)
            {
                v = this.delimiter + v;
            }
            this.writer.Write(v);
            this.columnIndex++;
        }

        public void WriteNewLine()
        {
            if (this.IsClosed)
            {
                throw new ObjectDisposedException("writer");
            }

            this.writer.Write(this.newLine);
            this.columnIndex = 0;
            this.rowIndex++;
        }

        public void Write(string[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            foreach (string value in values)
            {
                this.Write(value);
            }
        }

        public void Write(string[][] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            foreach (string[] line in values)
            {
                if (line == null)
                {
                    throw new ArgumentNullException("values");
                }
            }
            if (this.columnIndex > 0)
            {
                this.WriteNewLine();
            }
            foreach (string[] line in values)
            {
                foreach (string value in line)
                {
                    this.Write(value);
                }
                this.WriteNewLine();
            }
        }

        public void Write(IDataReader dataReader)
        {
            this.Write(dataReader, null, int.MaxValue);
        }

        public void Write(IDataReader dataReader, string[] names, int count)
        {
            if (dataReader == null)
            {
                throw new ArgumentNullException("dataReader");
            }
            if (count < 0)
            {
                throw new ArgumentException("count less than 0");
            }
            if (this.columnIndex > 0)
            {
                this.WriteNewLine();
            }
            for (int row = 0; row < count; row++)
            {
                if (!dataReader.Read())
                {
                    break;
                }
                object[] values = null;
                if (names != null)
                {
                    values = new object[names.Length];
                    for (int nameIndex = 0; nameIndex < names.Length; nameIndex++)
                    {
                        values[nameIndex] = dataReader[names[nameIndex]];
                    }
                }
                else
                {
                    values = new object[dataReader.FieldCount];
                    dataReader.GetValues(values);
                }
                foreach (object value in values)
                {
                    if (value == DBNull.Value)
                    {
                        this.Write(string.Empty);
                    }
                    else if (value == null)
                    {
                        this.Write(string.Empty);
                    }
                    else
                    {
                        this.Write(value.ToString());
                    }
                }
                this.WriteNewLine();
            }
        }
    }
}
