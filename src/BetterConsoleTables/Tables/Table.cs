using BetterConsoleTables.Configuration;
using BetterConsoleTables.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace BetterConsoleTables
{
    public class Table : TableBase<Table, Header, object>
    {

        #region Constructors

        public Table() : this(new TableConfig()) { }

        public Table(TableConfig config)
        {
            m_headers = new List<Header>();
            m_rows = new List<object[]>();
            Config = config;
        }

        public Table(TableConfig config, params Header[] columns)
            : this(config)
        {
            if (columns == null)
            {
                throw new ArgumentNullException(nameof(columns));
            }

            m_headers.AddRange(columns);
        }

        public Table(TableConfig config, Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left, params object[] columns)
            : this(config)
        {
            if (columns == null)
            {
                throw new ArgumentNullException(nameof(columns));
            }

            foreach (var column in columns)
            {
                m_headers.Add(new Header(column, rowsAlignment, headerAlignment));
            }
        }

        public Table(params Header[] columns)
            : this(new TableConfig(), columns) { }

        public Table(Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left, params object[] columns)
            : this(new TableConfig(), rowsAlignment, headerAlignment, columns) { }

        #endregion

        #region Public Method API

        /// <summary>
        /// Adds a row to the bottom of the list with the provided column values
        /// Expected that the provided values count is <= the number of columns in the table
        /// </summary>
        /// <param name="values">The column values.</param>
        /// <returns>This Table</returns>
        public override Table AddRow(params object[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (Headers.Count == 0)
            {
                //TODO: assign first row as columns by default later?
                throw new Exception("No columns exist, please add columns before adding rows");
            }

            if (values.Length > Headers.Count)
            {
                throw new Exception(
                    $"The number columns in the row ({values.Length}) is greater than the number of columns in the table ({m_headers.Count}");
            }

            if (values.Length < Headers.Count)
            {
                ResizeRow(ref values, Headers.Count);
            }

            m_rows.Add(values);

            return this;
        }

        /// <summary>
        /// Adds an array of rows to the bottom of the list
        /// </summary>
        /// <param name="rows"></param>
        /// <returns>This Table</returns>
        public override Table AddRows(IEnumerable<object[]> rows)
        {
            m_rows.AddRange(rows);
            return this;
        }

        public override Table AddHeader(string title)
        {
            return AddColumn(title, Alignment.Left, Alignment.Left);
        }

        public Table AddColumn(string title, Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left)
        {
            m_headers.Add(new Header(title, rowsAlignment, headerAlignment));

            if (m_rows.Count > 0 && LongestRow == m_headers.Count)
            {
                IncrementRowElements(1);
            }

            return this;
        }

        /// <summary>
        /// Adds a new column to the right of existing columns
        /// </summary>
        /// <param name="title"></param>
        /// <returns>This Table</returns>
        public Table AddColumn(object title, Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left)
        {
            return AddColumn(title.ToString(), rowsAlignment, headerAlignment);
        }

        public Table AddColumns(Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left, params string[] columns)
        {
            foreach (var column in columns)
            {
                // Not calling AddColumn() to avoid multiple IncrementRowElements calls
                m_headers.Add(new Header(column, rowsAlignment, headerAlignment));
            }

            if (m_rows.Count > 0 && LongestRow == m_headers.Count)
            {
                IncrementRowElements(columns.Length);
            }

            return this;
        }

        /// <summary>
        /// Adds multiple columns to the table, to the right of any existing columns.
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public Table AddColumns(Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left, params object[] columns)
        {
            foreach (var column in columns)
            {
                // Not calling AddColumn() to avoid multiple IncrementRowElements calls
                m_headers.Add(new Header(column, rowsAlignment, headerAlignment));
            }

            if (m_rows.Count > 0 && LongestRow == m_headers.Count)
            {
                IncrementRowElements(columns.Length);
            }

            return this;
        }

        /// <summary>
        /// Derives the table from the provided types.
        /// Columns are derived from Property Names
        /// Rows are derived from Property Values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public Table From<T>(IList<T> items)
        {
            T[] array = new T[items.Count];
            items.CopyTo(array, 0);
            ProcessReflectionData(array);
            return this;
        }

        #endregion

        #region Table Generation

        /// <summary>
        /// Outputs the table structure in accordance with the config
        /// </summary>
        public override string ToString()
        {
            int[] columnLengths = GetColumnLengths();
            return ToString(columnLengths);
        }

        public override string ToString(int[] columnLengths)
        {
            StringBuilder builder = new StringBuilder();

            string formattedHeaders = FormatHeader(columnLengths, m_headers, Config.innerColumnDelimiter, Config.outerColumnDelimiter);
            string[] formattedRows = FormatRows(columnLengths, m_rows, Config.innerColumnDelimiter, Config.outerColumnDelimiter);

            string headerDivider = GenerateDivider(columnLengths, Config.headerBottomIntersection, Config.headerRowDivider, Config.outerLeftVerticalIntersection, Config.outerRightVerticalIntersection);
            string innerDivider = GenerateDivider(columnLengths, Config.innerIntersection, Config.innerRowDivider, Config.outerLeftVerticalIntersection, Config.outerRightVerticalIntersection);

            if (Config.hasTopRow)
            {
                string divider = GenerateDivider(columnLengths, Config.headerTopIntersection, Config.headerRowDivider, Config.topLeftCorner, Config.topRightCorner);
                builder.AppendLine(divider);
            }

            builder.AppendLine(formattedHeaders);

            if (Config.hasHeaderRow)
            {
                builder.AppendLine(headerDivider);
            }

            builder.AppendLine(formattedRows[0]);

            for (int i = 1; i < formattedRows.Length; i++)
            {
                if (Config.hasInnerRows)
                {
                    builder.AppendLine(innerDivider);
                }
                builder.AppendLine(formattedRows[i]);
            }

            if (Config.hasBottomRow)
            {
                string divider = GenerateDivider(columnLengths, Config.outerBottomHorizontalIntersection, Config.outerRowDivider, Config.bottomLeftCorner, Config.bottomRightCorner);
                builder.AppendLine(divider);
            }

            return builder.ToString();
        }

        #endregion

        #region Generation Utility

        internal int[] GetColumnLengths()
        {
            int[] lengths = new int[m_headers.Count];
            for (int i = 0; i < m_headers.Count; i++)
            {
                int max = m_headers[i].ToString().Length;
                for (int j = 0; j < m_rows.Count; j++)
                {
                    int length = m_rows[j][i].ToString().Length;
                    if (length > max)
                    {
                        max = length;
                    }
                }
                lengths[i] = max;
            }
            return lengths;
        }

        private string[] FormatRows(int[] columnLengths, IList<object[]> values)
        {
            string[] output = new string[values.Count];
            for (int i = 0; i < values.Count; i++)
            {
                output[i] = FormatRow(columnLengths, values[i]);
            }
            return output;
        }

        private string[] FormatRows(int[] columnLengths, IList<object[]> values, char innerDelimiter, char outerDelimiter)
        {
            string[] output = new string[values.Count];
            for (int i = 0; i < values.Count; i++)
            {
                output[i] = FormatRow(columnLengths, values[i], innerDelimiter, outerDelimiter);
            }
            return output;
        }

        /// <summary>
        /// Formats a row with the default delimiter fields
        /// </summary>
        private string FormatRow(int[] columnLengths, IList<object> values)
        {
            string output = String.Empty;

            if (Config.hasOuterColumns)
            {
                output = String.Concat(output, Config.innerColumnDelimiter, " ", values[0].ToString().PadRight(columnLengths[0]), " ");
            }
            else
            {
                output = String.Concat(output, " ", values[0].ToString().PadRight(columnLengths[0]), " ");
            }

            for (int i = 1; i < m_headers.Count; i++)
            {
                output = String.Concat(output, Config.innerColumnDelimiter, " ", values[i].ToString().PadRight(columnLengths[i]), " ");
            }
            output = String.Concat(output, Config.innerColumnDelimiter);
            return PadRow(output);
        }

        private string FormatRow(int[] columnLengths, IList<object> values, char delimiter)
        {
            string output = String.Empty;

            for (int i = 0; i < m_headers.Count; i++)
            {
                output = String.Concat(output, delimiter, " ", values[i].ToString().PadRight(columnLengths[i]), " ");
            }
            output = String.Concat(output, delimiter);
            return PadRow(output);
        }

        private string FormatRow(int[] columnLengths, IList<object> values, char innerDelimiter, char outerDelimiter)
        {
            string output = String.Empty;
            output = String.Concat(output, outerDelimiter, " ", PadString(values[0].ToString(), columnLengths[0], m_headers[0].RowsAlignment), " ");

            for (int i = 1; i < m_headers.Count; i++)
            {
                output = String.Concat(output, innerDelimiter, " ", PadString(values[i].ToString(), columnLengths[i], m_headers[i].RowsAlignment), " ");
            }

            output = String.Concat(output, outerDelimiter);
            return PadRow(output);
        }

        //TEMP FOR NOW
        private string FormatHeader(int[] columnLengths, IList<Header> values, char innerDelimiter, char outerDelimiter)
        {
            string output = String.Empty;
            output = String.Concat(output, outerDelimiter, " ", PadString(values[0].ToString(), columnLengths[0], m_headers[0].HeaderAlignment), " ");
            for (int i = 1; i < m_headers.Count; i++)
            {
                output = String.Concat(output, innerDelimiter, " ", PadString(values[i].ToString(), columnLengths[i], m_headers[i].HeaderAlignment), " ");
            }
            output = String.Concat(output, outerDelimiter);
            return PadRow(output);
        }




        private string GenerateDivider(int[] columnLengths, char delimiter, char divider)
        {
            string output = String.Empty;
            for (int i = 0; i < m_headers.Count; i++)
            {
                output = String.Concat(output, delimiter, String.Empty.PadRight(columnLengths[i] + 2, divider)); //+2 for the 2 spaces around the delimiters
            }
            output = String.Concat(output, delimiter);
            return PadRow(output);
        }


        /// <summary>
        /// Generates a dividing row between content
        /// </summary>
        /// <param name="columnLengths">The width of each of the columns</param>
        /// <param name="innerDelimiter">The inner intersection divider</param>
        /// <param name="divider">The horizontal divider</param>
        /// <param name="outerDelimiter">The left and right outer edge character</param>
        /// <returns></returns>  
        private string GenerateDivider(int[] columnLengths, char innerDelimiter, char divider, char outerDelimiter)
        {
            string output = String.Empty;

            output = String.Concat(output, outerDelimiter, String.Empty.PadRight(columnLengths[0] + 2, divider));
            for (int i = 1; i < m_headers.Count; i++)
            {
                output = String.Concat(output, innerDelimiter, String.Empty.PadRight(columnLengths[i] + 2, divider)); //+2 for the 2 spaces around the delimiters
            }
            output = String.Concat(output, outerDelimiter);
            return PadRow(output);
        }

        /// <summary>
        /// Generates a dividing row between content rows
        /// </summary>
        /// <param name="columnLengths">The width of each of the columns</param>
        /// <param name="innerDelimiter">The inner intersection divider</param>
        /// <param name="divider">The horizontal divider</param>
        /// <param name="left">The left outer edge character</param>
        /// <param name="right">The right outer edge character</param>
        /// <returns></returns>
        private string GenerateDivider(int[] columnLengths, char innerDelimiter, char divider, char left, char right)
        {
            string output = String.Empty;

            output = String.Concat(output, left, String.Empty.PadRight(columnLengths[0] + 2, divider));
            for (int i = 1; i < m_headers.Count; i++)
            {
                output = String.Concat(output, innerDelimiter, String.Empty.PadRight(columnLengths[i] + 2, divider)); //+2 for the 2 spaces around the delimiters
            }
            output = String.Concat(output, right);
            return PadRow(output);
        }

        //Potentially will be unused.
        private string GetColumnsFormat(int[] columnLengths, char delimiter = '|')
        {
            string delmiterStr = delimiter == char.MinValue ? string.Empty : delimiter.ToString();
            string format = String.Empty;
            for (int i = 0; i < m_headers.Count; i++)
            {
                format = String.Concat(format, " ", delmiterStr, " {", i, ",-", columnLengths[i], "}");
            }
            format = String.Concat(" ", delmiterStr);
            return format;
        }

        #endregion

        #region Reflection 

        private void ProcessReflectionData<T>(T[] genericData)
        {
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            string[] columns = GetColumnNames(properties);
            string[][] data = GetRowsData(genericData, properties);
            foreach (string column in columns)
            {
                m_headers.Add(new Header(column));
            }
            m_rows.AddRange(data);
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

        #endregion 

    }

    enum Side
    {
        top = 0,
        bottom = 1
    }
}
