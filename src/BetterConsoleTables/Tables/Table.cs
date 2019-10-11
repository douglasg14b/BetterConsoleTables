using BetterConsoleTables.Builders;
using BetterConsoleTables.Common;
using BetterConsoleTables.Configuration;
using BetterConsoleTables.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace BetterConsoleTables
{
   /*public class Table<TModel> : TableBase<Table, Column, TModel>
    {
        public Table() : base() { }
        public Table(TableConfig config) : base(config) { }
        public Table(TableConfig config, params Column[] columns) : base(config, columns) { }
        public Table(TableConfig config, Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left, params object[] columns)
            : base(config, rowsAlignment, headerAlignment, columns) { }
        public Table(params Column[] columns) : base(columns) { }
        public Table(Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left, params object[] columns)
            : base(rowsAlignment, headerAlignment, columns) { }
    }*/

    public class Table : TableBase<Table, Column, string>
    {

        #region Constructors

        private void Create()
        {
            m_cellFormats = new List<List<ValueFormat>>();
            _ = PlatformInfo.HasFormattingSupport; //TODO: TEMP
        }

        public Table() 
            : base()
        {
            Create();
        }

        public Table(TableConfig config)
            : base(config)
        {
            Create();
        }

        public Table(TableConfig config, params Column[] columns)
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

            foreach (var columnItem in columns)
            {
                m_headers.Add(Column.Simple(columnItem, rowsAlignment, headerAlignment));
            }
        }


        public Table(params Column[] columns)
            : this(new TableConfig(), columns) { }

        public Table(Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left, params object[] columns)
            : this(new TableConfig(), rowsAlignment, headerAlignment, columns) { }


        // -------------------------------
        // ----- Static Constructors -----

        public static Table From<T>(T[] items, Column[] columns)
        {
            Table table = new Table(columns);
            table.ProcessReflectionData(items);
            return table;
        }

        #endregion

        private List<List<ValueFormat>> m_cellFormats { get; set; }

        private void AddFormatRow(int length)
        {
            var formatRow = new List<ValueFormat>(length);
            for(int i = 0; i < length; i++)
            {
                formatRow.Add(m_headers[i].RowsFormat);
            }
        }

        #region Public Method API

        /// <summary>
        /// Adds a row to the bottom of the list with the provided column values
        /// Expected that the provided values count is <= the number of columns in the table
        /// Converts the <param name="values"> to strings via ToString()
        /// </summary>
        /// <param name="values">The column values.</param>
        /// <returns>This Table</returns>
        public override Table AddRow(params object[] rowValues)
        {
            if (rowValues is null) throw new ArgumentNullException(nameof(rowValues), "Cannot add a null row to a table");

            AddFormatRow(rowValues.Length);

            string[] stringValues = new string[rowValues.Length];
            for (int i = 0; i < rowValues.Length; i++)
            {
                stringValues[i] = rowValues.ToString();
            }
            return AddRow(stringValues);
        }

        /// <summary>
        /// Adds an array of rows to the bottom of the list
        /// Converts the provided objects to strings via ToString()
        /// </summary>
        /// <param name="rows"></param>
        /// <returns>This Table</returns>
        public override Table AddRows(IEnumerable<object[]> rows)
        {
            if (rows is null) throw new ArgumentNullException(nameof(rows), "Cannot add null rows to a table");
            if (!rows.Any()) throw new ArgumentException("Cannot add an empty collection of rows to a table", nameof(rows));

            foreach (object[] row in rows)
            {
                AddRow(row);
            }
            return this;
        }

        public override Table AddColumn(Column column)
        {
            if (m_cellFormats.Count == 0)
            {
                m_cellFormats.Add(new List<ValueFormat>());
            }

            m_headers.Add(column);
            m_cellFormats[0].Add(column.HeaderFormat);

            if (m_rows.Count > 0 && LongestRow == m_headers.Count)
            {
                IncrementRowElements(1);
            }
            return this;
        }

        public Table AddColumn(string title, Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left)
        {
            return AddColumn(Column.Simple(title, rowsAlignment, headerAlignment));
        }

        /// <summary>
        /// Adds a new column to the right of existing columns
        /// </summary>
        /// <param name="title"></param>
        /// <returns>This Table</returns>
        public override Table AddColumn(object title, Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left)
        {
            return AddColumn(title.ToString(), rowsAlignment, headerAlignment);
        }

        public Table AddColumns(Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left, params string[] columns)
        {
            foreach (var column in columns)
            {
                // Not calling AddColumn() to avoid multiple IncrementRowElements calls
                m_headers.Add(Column.Simple(column, rowsAlignment, headerAlignment));
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
                m_headers.Add(Column.Simple(column, rowsAlignment, headerAlignment));
            }

            if (m_rows.Count > 0 && LongestRow == m_headers.Count)
            {
                IncrementRowElements(columns.Length);
            }

            return this;
        }



        #endregion

        #region Table Generation

        /// <summary>
        /// Outputs the table structure in accordance with the config
        /// </summary>
        public override string ToString()
        {
            int[] columnLengths = GetColumnLengths(m_headers.ToArray());
            return ToString(columnLengths);
        }

        public override string ToString(int[] columnLengths)
        {
            StringBuilder builder = new StringBuilder();

            Alignment[] columnAlignments = m_headers.Select(x => x.RowsAlignment).ToArray();
            string formattedHeaders;

            //Temp?
            if (m_cellFormats.Count > 0)
            {
                formattedHeaders = FormatHeader2(m_headers.Select(x => x.Title).ToList(), m_cellFormats[0], columnLengths, ref Config.innerColumnDelimiter, ref Config.outerColumnDelimiter);
            }
            else
            {
                formattedHeaders = FormatHeader(m_headers, columnLengths, Config.innerColumnDelimiter, Config.outerColumnDelimiter);
            }

            string[] formattedRows = FormatDataRows(m_rows, columnLengths, columnAlignments, Config.innerColumnDelimiter, Config.outerColumnDelimiter);

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

        //TEMP FOR NOW
        private string FormatHeader(IList<Column> values, int[] columnLengths, char innerDelimiter, char outerDelimiter)
        {
            string output = String.Empty;
            output = String.Concat(output, outerDelimiter, " ", PadString(values[0].ToString(), columnLengths[0], m_headers[0].HeaderAlignment), " ");
            for (int i = 1; i < m_headers.Count; i++)
            {
                output = String.Concat(output, innerDelimiter, " ", PadString(values[i].ToString(), columnLengths[i], m_headers[i].HeaderAlignment), " ");
            }
            output = String.Concat(output, outerDelimiter);
            return PadRowInConsole(output);
        }

        //TEMP FOR NOW
        private string FormatHeader2(IList<string> values, IList<ValueFormat> formats, int[] columnLengths, ref char innerDelimiter, ref char outerDelimiter)
        {
            string output = String.Empty;
            int rowWidth = 0; //Will be used for padding

            for (int i = 0; i < m_headers.Count; i++)
            {
                ref char delimiter = ref (i == 0 ? ref outerDelimiter : ref innerDelimiter);
                string paddedValue = PadString(values[i], columnLengths[i], formats[i].Alignment);
                rowWidth += 1 + 1 + paddedValue.Length + 1; // delimiter, space, value, space

                if (!formats[i].DefaultForeground)
                {
                    paddedValue = paddedValue.WithForegroundColor(formats[i].ForegroundColor);
                }

                if (!formats[i].DefaultBackground)
                {
                    paddedValue = paddedValue.WithBackgroundColor(formats[i].BackgroundColor);
                }

                if (formats[i].Bold)
                {
                    paddedValue = paddedValue.Bold();
                }

                if (formats[i].Underline)
                {
                    paddedValue = paddedValue.Underline();
                }

                output = String.Concat(output, delimiter, " ", paddedValue, " ");
            }
            output = String.Concat(output, outerDelimiter);
            //return PadRowInConsole(output); //Need to only pad the ACTUAL string length, not the one with variations baked in
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
