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
   public class Table<TModel> : TableBase<Table, Column, TModel>
    {
        public Table() : base() { }
        public Table(TableConfig config) : base(config) { }
        public Table(TableConfig config, params Column[] columns) : base(config, columns) { }
        public Table(TableConfig config, Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left, params object[] columns)
            : base(config, rowsAlignment, headerAlignment, columns) { }
        public Table(params Column[] columns) : base(columns) { }
        public Table(Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left, params object[] columns)
            : base(rowsAlignment, headerAlignment, columns) { }
    }

    public class Table : TableBase<Table, Column>
    {

        #region Constructors

        public Table() : this(new TableConfig()) { }

        public Table(TableConfig config)
        {
            m_headers = new List<Column>();
            m_rows = new List<string[]>();
            Config = config;
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

            foreach (var column in columns)
            {
                m_headers.Add(new Column(column, rowsAlignment, headerAlignment));
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

        #region Public Method API

        public override Table AddColumn(Column column)
        {
            m_headers.Add(column);
            if (m_rows.Count > 0 && LongestRow == m_headers.Count)
            {
                IncrementRowElements(1);
            }
            return this;
        }

        public Table AddColumn(string title, Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left)
        {
            return AddColumn(new Column(title, rowsAlignment, headerAlignment));
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
                m_headers.Add(new Column(column, rowsAlignment, headerAlignment));
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
                m_headers.Add(new Column(column, rowsAlignment, headerAlignment));
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
            int[] columnLengths = GetColumnLengths();
            return ToString(columnLengths);
        }

        public override string ToString(int[] columnLengths)
        {
            StringBuilder builder = new StringBuilder();

            Alignment[] columnAlignments = m_headers.Select(x => x.RowsAlignment).ToArray();

            string formattedHeaders = FormatHeader(m_headers, columnLengths, Config.innerColumnDelimiter, Config.outerColumnDelimiter);
            string[] formattedRows = FormatRows(m_rows, columnLengths, columnAlignments, Config.innerColumnDelimiter, Config.outerColumnDelimiter);

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

        #endregion




    }

    enum Side
    {
        top = 0,
        bottom = 1
    }
}
