using System;
using System.Collections.Generic;
using System.Text;

namespace BetterConsoleTables
{
    public class TableConfiguration
    {
        public TableConfiguration() { }
        public TableConfiguration(Style style)
        {
            switch (style)
            {
                case Style.Markdown:
                    SetMarkdown();
                    break;
                case Style.Simple:
                    SetSimple();
                    break;
                case Style.MySql:
                    SetMySql();
                    break;

            }
        }

        public bool hasTopRow = true;
        public bool hasBottomRow = true;
        public bool hasHeaderRow = true;
        public bool hasInnerRows = true;

        public bool hasOuterColumns = true;
        public bool hasInnerColumns = true;

        /// <summary>
        /// Seperates each column of data
        /// </summary>
        public char columnDelimiter = '|';

        /// <summary>
        /// Seperates each column of row dividers
        /// </summary>
        public char rowColumsDelimiter = '-';

        /// <summary>
        /// Seperates each row
        /// </summary>
        public char rowDivider = '-';

        /// <summary>
        /// Seperates each row in the header
        /// </summary>
        public char headerRowDivider = '-';

        /// <summary>
        /// Seperates each column of row dividers in the header
        /// </summary>
        public char headerRowColumnDelimiter = '-';

        private void SetMarkdown()
        {
            hasInnerRows = false;
            hasTopRow = false;
            hasBottomRow = false;
            columnDelimiter = '|';
            rowDivider = ' ';
            rowColumsDelimiter = '|';
            headerRowColumnDelimiter = '|';
        }

        private void SetSimple()
        {
            hasInnerRows = false;
            hasTopRow = false;
            hasBottomRow = false;
            hasOuterColumns = false;
            columnDelimiter = ' ';
            rowDivider = ' ';
            headerRowColumnDelimiter = ' ';
        }

        private void SetMySql()
        {
            columnDelimiter = '|';
            rowColumsDelimiter = '+';
            headerRowColumnDelimiter = '+';
        } 

        public static TableConfiguration Default()
        {
            return new TableConfiguration(Style.Default);
        }

        public static TableConfiguration Markdown()
        {
            return new TableConfiguration(Style.Markdown);
        }

        public static TableConfiguration Simple()
        {
            return new TableConfiguration(Style.Simple);
        }

        public static TableConfiguration MySql()
        {
            return new TableConfiguration(Style.MySql);
        }
    }
}
