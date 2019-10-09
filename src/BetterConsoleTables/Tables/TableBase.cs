using BetterConsoleTables.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Clawfoot.Extensions;
using BetterConsoleTables.Models;

namespace BetterConsoleTables
{
    // Table class that uses a Type as its rows. Each column is a property on TModel
    public abstract class TableBase<TTable, THeader, TModel> : TableBase<TTable, THeader>
        where TTable : TableBase<TTable, THeader, TModel>
    {
        protected List<TModel> m_typedRows;
        public IReadOnlyList<TModel> TypedRows => m_typedRows;

        public abstract TTable AddRow(TModel rowModel);
        public abstract TTable AddRows(IEnumerable<TModel> rowValues);
    }

    // Table class that uses strings as it's row items. Each row is a string[]
    public abstract class TableBase<TTable, THeader>
        where TTable: TableBase<TTable, THeader>
    {
        protected const char paddingChar = ' ';
        protected List<THeader> m_headers;
        protected List<string[]> m_rows;
        

        public IReadOnlyList<THeader> Headers => m_headers;
        public IReadOnlyList<string[]> Rows => m_rows;
        
        public TableConfig Config { get; set; }

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

        public abstract TTable AddColumn(Column header);

        /// <summary>
        /// Adds a row to the bottom of the list with the provided column values
        /// Expected that the provided values count is <= the number of columns in the table
        /// </summary>
        /// <param name="values">The column values.</param>
        /// <returns>This Table</returns>
        public virtual TTable AddRow(params string[] rowValues)
        {
            if (rowValues is null) throw new ArgumentNullException(nameof(rowValues), "Cannot add a null row to a table");
            if (rowValues.Length == 0) throw new ArgumentException("Cannot add row with a length of 0 to a table", nameof(rowValues));
            if (Headers.Count == 0) throw new InvalidOperationException("No columns exist, please add columns before adding rows");
            if (rowValues.Length > Headers.Count)
            {
                throw new InvalidOperationException(
                    $"The number columns in the row ({rowValues.Length}) is greater than the number of columns in the table ({m_headers.Count})");
            }

            if (rowValues.Length < Headers.Count)
            {
                ResizeRow(ref rowValues, Headers.Count);
            }

            m_rows.Add(rowValues);

            return (TTable)this;
        }
        /// <summary>
        /// Adds a row to the bottom of the list with the provided column values
        /// Expected that the provided values count is <= the number of columns in the table
        /// Converts the <param name="values"> to strings via ToString()
        /// </summary>
        /// <param name="values">The column values.</param>
        /// <returns>This Table</returns>
        public virtual TTable AddRow(params object[] rowValues)
        {
            if (rowValues is null) throw new ArgumentNullException(nameof(rowValues), "Cannot add a null row to a table");

            string[] stringValues = new string[rowValues.Length];
            for(int i = 0; i < rowValues.Length; i++)
            {
                stringValues[i] = rowValues.ToString();
            }
            return AddRow(stringValues);
        }
        /// <summary>
        /// Adds an array of rows to the bottom of the list
        /// </summary>
        /// <param name="rows"></param>
        /// <returns>This Table</returns>
        public virtual TTable AddRows(IEnumerable<string[]> rows)
        {
            if (rows is null) throw new ArgumentNullException(nameof(rows), "Cannot add null rows to a table");
            if (!rows.Any()) throw new ArgumentException("Cannot add an empty collection of rows to a table", nameof(rows));

            foreach(var row in rows)
            {
                AddRow(row);
            }

            return (TTable)this;
        }
        /// <summary>
        /// Adds an array of rows to the bottom of the list
        /// Converts the provided objects to strings via ToString()
        /// </summary>
        /// <param name="rows"></param>
        /// <returns>This Table</returns>
        public virtual TTable AddRows(IEnumerable<object[]> rows)
        {
            if (rows is null) throw new ArgumentNullException(nameof(rows), "Cannot add null rows to a table");
            if (!rows.Any()) throw new ArgumentException("Cannot add an empty collection of rows to a table", nameof(rows));

            foreach(object[] row in rows)
            {
                AddRow(row);
            }
            return (TTable)this;
        }


        public abstract string ToString(int[] columnWidths);

        #region TableFormatters

        #endregion

        #region Helpers

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
        protected string PadRowInConsole(string renderedRow)
        {
            //No need to, and cannot pad out rows if there is no console
            if (!TableConfig.ConsoleAvailable)
            {
                return renderedRow;
            }

            try
            {
                if (renderedRow.Length < Console.WindowWidth)
                {
                    return renderedRow.PadRight(Console.WindowWidth - 1);
                }
                else
                {
                    if (Config.ExpandConsole && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        Console.WindowWidth = Math.Min(renderedRow.Length + 1, Console.LargestWindowWidth - 1);
                    }
                    return renderedRow;
                }
            }
            catch (IOException ex) //If a console is not available an IOException is thrown
            {
                TableConfig.ConsoleAvailable = false;
                return renderedRow;
            }
        }

        /// <summary>
        /// Increments the length of all row arrays
        /// Sets the new elements to default
        /// </summary>
        /// <param name="increments"></param>
        protected virtual void IncrementRowElements(int increments)
        {
            for (int i = 0; i < m_rows.Count; i++)
            {
                string[] array = m_rows[i];
                int length = array.Length;

                Array.Resize(ref array, length + increments);

                m_rows[i] = array;
                for (int j = length; j < m_rows[i].Length; j++)
                {
                    m_rows[i][j] = default(string);
                }
            }
        }

        /// <summary>
        /// Resizes a row array to a specific length
        /// Sets the new elements to default
        /// </summary>
        /// <param name="row"></param>
        /// <param name="newSize"></param>
        protected virtual void ResizeRow(ref string[] row, int newSize)
        {
            int length = row.Length;
            Array.Resize(ref row, newSize);
            for (int i = length; i < row.Length; i++)
            {
                row[i] = default(string);
            }
        }

        protected string WrapText(string text, int maxWidth)
        {
            return text.Wrap(maxWidth);
        }

        #endregion

        #region Reflection

        // Note: Doesn't grab the reflection data at this time
        /*protected void ProcessReflectionData(IEnumerable<TModel> genericData)
        {
            PropertyInfo[] properties = typeof(TModel).GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            string[] columns = GetReflectionHeaders(properties);
            //object[][] data = GetReflectionRowsData(genericData, properties);

            foreach (string column in columns)
            {
                AddColumn(column);
            }
            AddRows(genericData);
        }

        private string[] GetReflectionHeaders(PropertyInfo[] properties)
        {
            string[] output = new string[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                output[i] = properties[i].Name;
            }
            return output;
        }

        private object[][] GetReflectionRowsData<T>(T[] data, PropertyInfo[] properties)
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
        }*/

        #endregion
    }
}
