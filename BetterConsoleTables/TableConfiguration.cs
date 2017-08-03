using System;
using System.Collections.Generic;
using System.Text;

namespace BetterConsoleTables
{
    public class TableConfiguration
    {
        public bool hasTopRow = true;
        public bool hasBottomRow = true;
        public bool hasHeaderRow = true;
        public bool hasInnerRows = true;

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

        public static TableConfiguration Markdown()
        {
            return new TableConfiguration()
            {
                hasInnerRows = false,
                hasTopRow = false,
                hasBottomRow = false,
                columnDelimiter = '|',
                rowDivider = ' ',
                rowColumsDelimiter = '|',
                headerRowColumnDelimiter = '|'
            };
        }

        public static TableConfiguration Simple()
        {
            return new TableConfiguration()
            {
                hasInnerRows = false,
                hasTopRow = false,
                hasBottomRow = false,
                hasInnerColumns = false,
                columnDelimiter = ' ',
                rowDivider = ' ',
                headerRowColumnDelimiter = ' '
            };
        }
    }
}
