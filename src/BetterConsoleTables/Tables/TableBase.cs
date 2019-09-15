using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables
{
    public abstract class TableBase<TTable, THeader, TRow>
    {
        protected const char paddingChar = ' ';

        protected List<THeader> m_headers;
        public IReadOnlyList<THeader> Headers => m_headers;

        protected List<TRow[]> m_rows;
        public IReadOnlyList<TRow[]> Rows => m_rows;

        public TableConfiguration Config { get; set; }

        /// <summary>
        /// Gets the row with the greatest number of elements
        /// </summary>
        public int LongestRow
        {
            get
            {
                int max = 0;
                for (int i = 0; i < m_rows.Count; i++)
                {
                    max = m_rows[i].Length > max ? m_rows[i].Length : max;
                }
                return max;
            }
        }

        public abstract TTable AddColumn(string value);

        public abstract TTable AddRow(params object[] values);
        public abstract TTable AddRows(IEnumerable<object[]> values);

        public abstract string ToString(int[] columnWidths);



        protected string PadString(string value, int maxLength, Alignment alignment)
        {
            if (value.Length == maxLength)
            {
                return value;
            }

            switch (alignment)
            {
                case Alignment.Left:
                    return value.PadRight(maxLength, paddingChar);
                case Alignment.Right:
                    return value.PadLeft(maxLength, paddingChar);
                case Alignment.Center:
                    return value.PadLeftAndRight(maxLength, paddingChar);
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Pads the row out to the edge of the console, if row is wider then console, expand console window
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        protected string PadRow(string row)
        {
            //No need to, and cannot pad out rows if there is no console
            if (!TableConfiguration.ConsoleAvailable)
            {
                return row;
            }

            try
            {
                if (row.Length < Console.WindowWidth)
                {
                    return row.PadRight(Console.WindowWidth - 1);
                }
                else
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        Console.WindowWidth = row.Length + 1;
                    }
                    return row;
                }
            }
            catch (IOException ex) //If a console is not available an IOException is thrown
            {
                TableConfiguration.ConsoleAvailable = false;
                return row;
            }
        }

        /// <summary>
        /// Increments the length of all row arrays
        /// Sets the new elements to default
        /// </summary>
        /// <param name="increments"></param>
        protected void IncrementRowElements(int increments)
        {
            for (int i = 0; i < m_rows.Count; i++)
            {
                TRow[] array = m_rows[i];
                int length = array.Length;

                Array.Resize(ref array, length + increments);

                m_rows[i] = array;
                for (int j = length; j < m_rows[i].Length; j++)
                {
                    m_rows[i][j] = default(TRow);
                }
            }
        }

        /// <summary>
        /// Resizes a row array to a specific length
        /// Sets the new elements to default
        /// </summary>
        /// <param name="row"></param>
        /// <param name="newSize"></param>
        protected void ResizeRow(ref TRow[] row, int newSize)
        {
            int length = row.Length;
            Array.Resize(ref row, newSize);
            for (int i = length; i < row.Length; i++)
            {
                row[i] = default(TRow);
            }
        }

        protected string WrapText(string text, int maxWidth)
        {
            throw new NotImplementedException();
        }

        protected void ProcessReflectionData<T>(T[] genericData)
        {
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            string[] columns = GetColumnNames(properties);
            object[][] data = GetRowsData2(genericData, properties);

            foreach (string column in columns)
            {
                AddColumn(column);
            }
            AddRows(data);
        }

        private string[] GetColumnNames(PropertyInfo[] properties)
        {
            string[] output = new string[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                output[i] = properties[i].Name;
            }
            return output;
        }

        private object[][] GetRowsData2<T>(T[] data, PropertyInfo[] properties)
        {
            object[][] output = new object[data.Length][];
            for (int i = 0; i < data.Length; i++)
            {
                object[] values = new object[properties.Length];

                // Is null or default. Value type default is 0, reference types is null
                // If the row is null, fill all row values with the default
                if (EqualityComparer<T>.Default.Equals(data[i], default(T)))
                {
                    object elementValue = String.Empty;
                    // Cannot ToString() null
                    if (default(T) == null)
                    {
                        elementValue = "null";
                    }
                    else
                    {
                        elementValue = default(T);
                    }
                    for (int j = 0; j < properties.Length; j++)
                    {
                        values[j] = elementValue;
                    }

                    continue;
                }

                for (int j = 0; j < properties.Length; j++)
                {
                    object columnValue = properties[j].GetValue(data[i]);

                    if (columnValue is null)
                    {
                        values[j] = "null";
                        continue;
                    }

                    values[j] = columnValue;
                }
                output[i] = values;
            }
            return output;
        }

        [Obsolete]
        private string[][] GetRowsData<T>(T[] data, PropertyInfo[] properties)
        {
            string[][] output = new string[data.Length][];
            for (int i = 0; i < data.Length; i++)
            {
                string[] values = new string[properties.Length];

                // Is null or default. Value type default is 0, reference types is null
                // If the row is null, fill all row values with the default
                if (EqualityComparer<T>.Default.Equals(data[i], default(T)))
                {
                    string elementValue = String.Empty;
                    // Cannot ToString() null
                    if (default(T) == null)
                    {
                        elementValue = "null";
                    }
                    else
                    {
                        elementValue = default(T).ToString();
                    }
                    for (int j = 0; j < properties.Length; j++)
                    {
                        values[j] = elementValue;
                    }

                    continue;
                }


                for (int j = 0; j < properties.Length; j++)
                {
                    object columnValue = properties[j].GetValue(data[i]);

                    if (columnValue is null)
                    {
                        values[j] = "null";
                        continue;
                    }

                    values[j] = columnValue.ToString();
                }
                output[i] = values;
            }
            return output;
        }
    }
}
