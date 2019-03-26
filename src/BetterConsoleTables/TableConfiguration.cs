using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                case Style.MySqlSimple:
                    SetMySqlSimple();
                    break;
                case Style.Unicode:
                    SetUnicode();
                    break;
                case Style.UnicodeAlt:
                    SetUnicodeAlt();
                    break;
            }
            SetDefaults();
        }

        public bool wrapText = false;
        public int textWrapLimit = 25;

        public bool hasTopRow = true;
        public bool hasBottomRow = true;
        public bool hasHeaderRow = true;
        public bool hasInnerRows = true;

        public bool hasOuterColumns = true;
        public bool hasInnerColumns = true;

        /// <summary>
        /// Seperates each column of data inside the table
        /// </summary>
        public char innerColumnDelimiter = '|';
        public char outerColumnDelimiter = char.MinValue;

        public char innerRowDivider = '-';
        public char outerRowDivider = char.MinValue;

        /// <summary>
        /// Each inner row/column intersection
        /// </summary>
        public char innerIntersection = '-';

        public char outerRightVerticalIntersection = char.MinValue;
        public char outerLeftVerticalIntersection = char.MinValue;

        /// <summary>
        /// Only used if here is no header
        /// </summary>
        public char outerTopHorizontalIntersection = char.MinValue;
        public char outerBottomHorizontalIntersection = char.MinValue;

        public char topLeftCorner = char.MinValue;
        public char topRightCorner = char.MinValue;
        public char bottomLeftCorner = char.MinValue;
        public char bottomRightCorner = char.MinValue;


        /// <summary>
        /// The top and bottom header rows
        /// </summary>
        public char headerRowDivider = '-';
        
        /// <summary>
        /// Top and bottom header row/column intersections. Only used if top and bottom are not defined
        /// </summary>
        public char headerIntersection = '-';

        /// <summary>
        /// Top header row/column intersections 
        /// </summary>
        public char headerTopIntersection = char.MinValue;
        /// <summary>
        /// Bottom header row/column intersections 
        /// </summary>
        public char headerBottomIntersection = char.MinValue;


        //Sets the default values based on existing values
        private void SetDefaults()
        {
            SetDefault(ref outerColumnDelimiter, innerColumnDelimiter);
            SetDefault(ref outerRowDivider, innerRowDivider);
            SetDefault(ref outerRightVerticalIntersection, innerIntersection);
            SetDefault(ref outerLeftVerticalIntersection, innerIntersection);
            SetDefault(ref outerTopHorizontalIntersection, innerIntersection);
            SetDefault(ref outerBottomHorizontalIntersection, innerIntersection);

            SetDefault(ref topLeftCorner, innerIntersection);
            SetDefault(ref topRightCorner, innerIntersection);
            SetDefault(ref bottomLeftCorner, innerIntersection);
            SetDefault(ref bottomRightCorner, innerIntersection);

            SetDefault(ref headerTopIntersection, headerIntersection);
            SetDefault(ref headerBottomIntersection, headerIntersection);
        }

        private void SetDefault(ref char character, char def)
        {
            if(character == Char.MinValue)
            {
                character = def;
            }
        }

        private void SetMarkdown()
        {
            hasInnerRows = false;
            hasTopRow = false;
            hasBottomRow = false;
            innerColumnDelimiter = '|';
            innerRowDivider = ' ';
            innerIntersection = '|';
            headerIntersection = '|';
        }

        private void SetSimple()
        {
            hasInnerRows = false;
            hasTopRow = false;
            hasBottomRow = false;
            hasOuterColumns = false;
            innerColumnDelimiter = ' ';
            innerRowDivider = ' ';
            headerIntersection = ' ';
        }

        private void SetMySql()
        {
            innerColumnDelimiter = '|';
            innerIntersection = '+';
            headerIntersection = '+';
        }

        private void SetMySqlSimple()
        {
            hasInnerRows = false;
            innerColumnDelimiter = '|';
            innerIntersection = '+';
            headerIntersection = '+';
        }

        private void SetUnicode()
        {
            hasInnerRows = false;

            headerTopIntersection = '┬';
            headerBottomIntersection = '┼';

            outerLeftVerticalIntersection = '├';
            outerRightVerticalIntersection = '┤';
            outerBottomHorizontalIntersection = '┴';

            topLeftCorner = '┌';
            topRightCorner = '┐';
            bottomLeftCorner = '└';
            bottomRightCorner = '┘';

            innerColumnDelimiter = '│';
            outerColumnDelimiter = '│';
            innerRowDivider = '─';

            headerRowDivider = '─';
            headerTopIntersection = '┬';
            headerBottomIntersection = '┼';
            innerIntersection = '┼';

            if (ConsoleAvailable && !Console.OutputEncoding.Equals(Encoding.UTF8))
            {
                Console.OutputEncoding = Encoding.UTF8;
            }
        }

        private void SetUnicodeAlt()
        {
            hasInnerRows = false;

            headerTopIntersection = '╦';
            headerBottomIntersection = '╬';

            outerLeftVerticalIntersection = '╠';
            outerRightVerticalIntersection = '╣';
            outerBottomHorizontalIntersection = '╩';

            topLeftCorner = '╔';
            topRightCorner = '╗';
            bottomLeftCorner = '╚';
            bottomRightCorner = '╝';

            innerColumnDelimiter = '║';
            outerColumnDelimiter = '║';
            innerRowDivider = '═';

            headerRowDivider = '═';
            headerTopIntersection = '╦';
            headerBottomIntersection = '╬';
            innerIntersection = '╬';

            if (ConsoleAvailable && !Console.OutputEncoding.Equals(Encoding.UTF8))
            {
                Console.OutputEncoding = Encoding.UTF8;
            }
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

        public static TableConfiguration MySqlSimple()
        {
            return new TableConfiguration(Style.MySqlSimple);
        }

        public static TableConfiguration Unicode()
        {
            return new TableConfiguration(Style.Unicode);
        }

        public static TableConfiguration UnicodeAlt()
        {
            return new TableConfiguration(Style.UnicodeAlt);
        }
        
        /// <summary>
        /// Set to false when console is not available to avoid exceptions.
        /// </summary>
        public static bool ConsoleAvailable { get; set; } = true;
    }
}
