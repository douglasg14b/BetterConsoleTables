using BetterConsoles.Colors.Extensions;
using BetterConsoles.Tables.Builders;
using BetterConsoles.Tables.Common;
using BetterConsoles.Tables.Configuration;
using BetterConsoles.Tables.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace BetterConsoles.Tables
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

    public class Table : TableBase<Table, IColumn, string>
    {

        #region Constructors

        private void Create()
        {
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

        public Table(TableConfig config, params IColumn[] columns)
            : this(config)
        {
            if (columns == null)
            {
                throw new ArgumentNullException(nameof(columns));
            }

            AddColumns(columns);
        }

        public Table(TableConfig config, params string[] columns)
        : this(config)
        {
            if (columns == null)
            {
                throw new ArgumentNullException(nameof(columns));
            }

            foreach (string title in columns)
            {
                AddColumn(Column.Default(title), false);
            }
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
                AddColumn(Column.Simple(columnItem, rowsAlignment, headerAlignment), false);
            }
        }


        public Table(params IColumn[] columns)
            : this(new TableConfig(), columns) { }

        public Table(params string[] columns)
            : this(new TableConfig(), columns) { }

        public Table(Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left, params object[] columns)
            : this(new TableConfig(), rowsAlignment, headerAlignment, columns) { }


        // -------------------------------
        // ----- Static Constructors -----

        public static Table From<T>(T[] items, IColumn[] columns)
        {
            Table table = new Table(columns);
            table.ProcessReflectionData(items);
            return table;
        }

        #endregion

        protected override void AddCellFormatsRow(int length)
        {
            var formatRow = new List<ICellFormat>(length);
            for (int i = 0; i < length; i++)
            {
                formatRow.Add(m_headers[i].RowsFormat);
            }

            m_formatMatrix.Add(formatRow);
        }

        protected void AddCellFormatsRow(ICell[] cells)
        {
            var formatRow = new List<ICellFormat>(cells.Length);
            for (int i = 0; i < cells.Length; i++)
            {
                formatRow.Add(CellFormat.Merge(cells[i].Format, m_headers[i].RowsFormat));
            }

            m_formatMatrix.Add(formatRow);
        }

        #region Public Method API

        public override Table ReplaceRows(IEnumerable<object[]> rows)
        {
            if (rows is null) throw new ArgumentNullException(nameof(rows), "Cannot use null values to replace a tables data");

            var headerFormats = m_formatMatrix[0];
            m_rows.Clear();

            return AddRows(rows);
        }

        /// <summary>
        /// Adds a row to the bottom of the list with the provided column values & formats
        /// Expected that the provided values count is <= the number of columns in the table
        /// </summary>
        /// <param name="values">The column values.</param>
        /// <returns>This Table</returns>
        public Table AddRow(params ICell[] rows)
        {
            if (rows is null) throw new ArgumentNullException(nameof(rows), "Cannot add a null row to a table");
            if (rows.Length == 0) throw new ArgumentException("Cannot add row with a length of 0 to a table", nameof(rows));
            if (Headers.Count == 0) throw new InvalidOperationException("No columns exist, please add columns before adding rows");
            if (rows.Length > Headers.Count)
            {
                throw new InvalidOperationException(
                    $"The number columns in the row ({rows.Length}) is greater than the number of columns in the table ({m_headers.Count})");
            }

            if (rows.Length < Headers.Count)
            {
                ResizeRow(ref rows, Headers.Count);
            }

            m_rows.Add(rows.Select(x => x.Value).ToArray());

            if (m_rows.Count > m_formatMatrix.Count - 1)
            {
                AddCellFormatsRow(rows); // Add a new row of formattings
            }

            return this;
        }

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

            string[] stringValues = new string[rowValues.Length];
            for (int i = 0; i < rowValues.Length; i++)
            {
                stringValues[i] = rowValues[i].ToString();
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

        private Table AddColumn(IColumn column, bool resizeRows = true)
        {
            if (m_formatMatrix.Count == 0)
            {
                m_formatMatrix.Add(new List<ICellFormat>());
            }

            m_headers.Add(column);
            m_formatMatrix[0].Add(column.HeaderFormat);

            if (resizeRows)
            {
                EnsureProperRowSize();
            }

            return this;
        }

        public override Table AddColumn(IColumn column)
        {
            return AddColumn(column);
        }

        public override Table AddColumn(string title, Alignment rowsAlignment = Alignment.Left, Alignment headerAlignment = Alignment.Left)
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
                AddColumn(Column.Simple(column, rowsAlignment, headerAlignment), false);
            }

            EnsureProperRowSize();
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
                AddColumn(Column.Simple(column, rowsAlignment, headerAlignment), false);
            }

            EnsureProperRowSize();
            return this;
        }

        public Table AddColumns(params IColumn[] columns)
        {
            foreach (var column in columns)
            {
                AddColumn(column, false);
            }

            EnsureProperRowSize();
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
            string[] formattedRows;

            //Temp?
            if (m_formatMatrix.Count > 0)
            {
                formattedHeaders = FormatRow(m_headers.Select(x => x.Title).ToList(), m_formatMatrix[0], columnLengths, ref Config.innerColumnDelimiter, ref Config.outerColumnDelimiter);
                formattedRows = FormatDataRows(m_rows, m_formatMatrix, columnLengths, ref Config.innerColumnDelimiter, ref Config.outerColumnDelimiter);
            }
            else
            {
                formattedHeaders = FormatHeader(m_headers, columnLengths, Config.innerColumnDelimiter, Config.outerColumnDelimiter);
                formattedRows = FormatDataRows(m_rows, columnLengths, columnAlignments, Config.innerColumnDelimiter, Config.outerColumnDelimiter);
            }

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


            if (formattedRows.Length > 0)
            {
                builder.AppendLine(formattedRows[0]);

                for (int i = 1; i < formattedRows.Length; i++)
                {
                    if (Config.hasInnerRows)
                    {
                        builder.AppendLine(innerDivider);
                    }
                    builder.AppendLine(formattedRows[i]);
                }
            }
            else // There are no rows/data
            {
                if (Config.hasInnerRows)
                {
                    builder.AppendLine(innerDivider);
                }
            }

            if (Config.hasBottomRow)
            {
                string divider = GenerateDivider(columnLengths, Config.outerBottomHorizontalIntersection, Config.outerRowDivider, Config.bottomLeftCorner, Config.bottomRightCorner);
                builder.AppendLine(divider);
            }

            return builder.ToString();
        }

        //TEMP FOR NOW
        private string FormatHeader(IList<IColumn> values, int[] columnLengths, char innerDelimiter, char outerDelimiter)
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
        private string FormatHeader2(IList<string> values, IList<ICellFormat> formats, int[] columnLengths, ref char innerDelimiter, ref char outerDelimiter)
        {
            string output = String.Empty;
            int rowWidth = 0; //Will be used for padding

            for (int i = 0; i < m_headers.Count; i++)
            {
                ref char delimiter = ref (i == 0 ? ref outerDelimiter : ref innerDelimiter);
                string paddedValue = PadString(values[i], columnLengths[i], formats[i].Alignment);
                rowWidth += 1 + 1 + paddedValue.Length + 1; // delimiter, space, value, space

                paddedValue = paddedValue.SetStyle(formats[i]);

                output = String.Concat(output, delimiter, " ", paddedValue, " ");
            }
            output = String.Concat(output, outerDelimiter);
            //return PadRowInConsole(output); //Need to only pad the ACTUAL string length, not the one with variations baked in
            return output;
        }

        private string[] FormatDataRows(IList<string[]> values, List<List<ICellFormat>> formats, int[] columnLengths, ref char innerDelimiter, ref char outerDelimiter)
        {
            string[] output = new string[values.Count];

            for (int i = 0; i < values.Count; i++)
            {
                if (formats != null && formats.Count > i + 1 && formats[i + 1] != null) // formats[i+1] since first element is header row
                {
                    output[i] = FormatRow(values[i], formats[i + 1], columnLengths, ref innerDelimiter, ref outerDelimiter);
                }
                else
                {
                    output[i] = FormatDataRow(values[i], columnLengths, innerDelimiter, outerDelimiter);
                }
            }

            return output;
        }

        // Does the more complex formatting as opposed to FormatDataRow
        private string FormatRow(IList<string> values, IList<ICellFormat> formats, int[] columnLengths, ref char innerDelimiter, ref char outerDelimiter)
        {
            string output = String.Empty;
            int rowWidth = 0; //Will be used for padding

            for (int i = 0; i < values.Count; i++)
            {
                ref char delimiter = ref (i == 0 ? ref outerDelimiter : ref innerDelimiter);
                string paddedValue = PadString(values[i], columnLengths[i], formats[i]?.Alignment ?? default, formats[i].InnerFormatting);

                rowWidth += 1 + 1 + paddedValue.Length + 1; // delimiter, space, value, space

                if (formats.Count > 0 && formats[i] != null)
                {
                    paddedValue = paddedValue.SetStyle(formats[i]);
                }

                output = String.Concat(output, delimiter, " ", paddedValue, " ");
            }
            output = String.Concat(output, outerDelimiter);
            //return PadRowInConsole(output); //Need to only pad the ACTUAL string length, not the one with variations baked in
            return output;
        }

        #endregion

        #region State Management

        /// <inheritdoc/>
        protected override void ExtendFormatRow(int matrixRowIndex, int elementsToAdd)
        {
            var formatRow = m_formatMatrix[matrixRowIndex];
            var lastIndex = formatRow.Count - 1;

            for (int i = 0; i < elementsToAdd; i++)
            {
                var format = m_headers[lastIndex + i].RowsFormat;
                formatRow.Add(format);
            }
        }

        #endregion

    }

    enum Side
    {
        top = 0,
        bottom = 1
    }
}
