using System;
using System.Collections.Generic;
using System.Text;

namespace BetterConsoleTables
{
    public class Table
    {
        //Expose interfaces over concrete classes, also CA2227
        private List<object> m_columns;
        public IList<object> Columns
        {
            get
            {
                return m_columns;
            }
        }

        private List<object[]> m_rows;
        public IList<object[]> Rows
        {
            get
            {
                return m_rows;
            }
        }

        public TableConfiguration Config { get; set; }

        #region Constructors & Addings

        public Table() : this(new TableConfiguration()) { }

        public Table(TableConfiguration config)
        {
            Config = config;
        }

        public Table(params object[] columns)
            : this(new TableConfiguration())
        {
            if (columns == null)
            {
                throw new ArgumentNullException(nameof(columns));
            }

            m_columns = new List<object>(columns);
            m_rows = new List<object[]>();

        }

        public Table AddRow(params object[] values)
        {
            if(values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if(Columns.Count == 0)
            {
                //TODO: assign first row as columns by defualt later?
                throw new Exception("No columns exist, please add columns before adding rows");
            }

            if(Columns.Count != values.Length)
            {
                throw new Exception(
                    $"The number columns in the row ({Columns.Count}) does not match the values ({values.Length}");
            }
            m_rows.Add(values);

            return this;
        }

        public Table AddRows(IEnumerable<object[]> rows)
        {
            m_rows.AddRange(rows);
            return this;
        }

        #endregion

        #region Table Generation

        /// <summary>
        /// Outputs the table structure in accordance with the config
        /// </summary>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            int[] columnLengths = GetColumnLengths();
            string formattedHeaders = FormatRow(columnLengths, m_columns);
            string[] formattedRows = FormatRows(columnLengths, m_rows);

            string headerDivider = GenerateDivider(columnLengths, Config.headerRowColumnDelimiter, Config.headerRowDivider);
            string divider = GenerateDivider(columnLengths, Config.rowColumsDelimiter, Config.rowDivider);

            if (Config.hasTopRow)
            {
                builder.AppendLine(headerDivider);
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
                    builder.AppendLine(divider);
                }
                builder.AppendLine(formattedRows[i]);
            }

            if (Config.hasBottomRow)
            {
                builder.AppendLine(divider);
            }

            return builder.ToString();
        }

        #endregion

        #region Generation Utility

        private int[] GetColumnLengths()
        {
            int[] lengths = new int[m_columns.Count];
            for(int i = 0; i < m_columns.Count; i++)
            {
                int max = m_columns[i].ToString().Length;
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
            for(int i = 0; i < values.Count; i++)
            {
                output[i] = FormatRow(columnLengths, values[i]);
            }
            return output;
        }

        private string FormatRow(int[] columnLengths, IList<object> values)
        {
            string output = String.Empty;

            if (Config.hasOuterColumns)
            {
                output = String.Concat(output, Config.columnDelimiter, " ", values[0].ToString().PadRight(columnLengths[0]), " ");
            }
            else
            {
                output = String.Concat(output, " ", values[0].ToString().PadRight(columnLengths[0]), " ");
            }

            for (int i = 1; i < m_columns.Count; i++)
            {
                output = String.Concat(output, Config.columnDelimiter, " ", values[i].ToString().PadRight(columnLengths[i]), " ");
            }
            output = String.Concat(output, Config.columnDelimiter);
            return output;
        }

        private string FormatRow(int[] columnLengths, IList<object> values, char delimiter)
        {
            string output = String.Empty;

            for (int i = 0; i < m_columns.Count; i++)
            {
                output = String.Concat(output, delimiter, " ", values[i].ToString().PadRight(columnLengths[i]), " ");
            }
            output = String.Concat(output, delimiter);
            return output;
        }

        private string GenerateDivider(int[] columnLengths, char delimiter, char divider)
        {
            string output = String.Empty;
            for(int i = 0; i < m_columns.Count; i++)
            {
                output = String.Concat(output, delimiter, String.Empty.PadRight(columnLengths[i] + 2, divider)); //+2 for the 2 spaces around the delimiters
            }
            output = String.Concat(output, delimiter);
            return output;
        }


        //Unused now?
        private string[][] PadRows(int[] columnLengths, IList<object[]> values)
        {
            string[][] output = new string[values.Count][];
            for(int i = 0; i < values.Count; i++)
            {
                output[i] = PadRow(columnLengths, values[i]);
            }
            return output;
        }

        //Unused now?
        private string[] PadRow(int[] columnLengths, IList<object> values)
        {
            string[] output = new string[m_columns.Count];

            for(int i = 0; i < m_columns.Count; i++)
            {
                output[i] = String.Concat(values[i].ToString().PadRight(columnLengths[i]));
            }

            return output;
        }

        //Potentially will be unused.
        private string GetColumnsFormat(int[] columnLengths, char delimiter = '|')
        {
            string delmiterStr = delimiter == char.MinValue ? string.Empty : delimiter.ToString();
            string format = String.Empty;
            for(int i = 0; i < m_columns.Count; i++)
            {
                format = String.Concat(format, " ", delmiterStr, " {", i, ",-", columnLengths[i], "}");
            }
            format = String.Concat(" ", delmiterStr);
            return format;
        }

        #endregion
    }
}
