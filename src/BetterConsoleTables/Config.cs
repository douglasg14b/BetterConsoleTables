using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BetterConsoleTables
{
    // Definitions for this context:
    //  -Areas:
    //      - Table: The main body of the table containing data
    //      - Header: The header of the table containing column names
    //  -Types:
    //      - Delimiter: The characters seperating rows/columns/cells
    //      - Edge: The characters representing the edge of the table or header
    //      - Intersection: The intersection of row & column delimiters
    //  -Locations:
    //      - Inner: Inside the table's bounds
    //      - Outer: The outside bounds of the table
    //  -Locations2:
    //      - Top: Top edge of table or header
    //      - Bottom: Bottom edge of table or header
    //      - Right: Right edge of table of header
    //      - Left: Left edge of table or header
    //  -Alignments:
    //      - Row: Horizontal (Interchangeable)
    //      - Column: Vertical (Interchangeable)
    //
    //
    // Naming Conventions:
    //  Names are made up of segments using a combination of the above definitions. 
    //  The following is the order these are placed in property names.
    //  NOTE: Not all segments may be used in every name. An excluded {Area} means it's for the table instead of the header
    //      - {Area}{Location}{Location2}{Alignment}{Type}
    public class Config
    {
        public Config() { }
        public Config(Style style)
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
        /// Seperates each column of data inside the table (data intersection)
        /// </summary>
        public char InnerColumnDelimiter { get => _innerColumnDelimiter; set => _innerColumnDelimiter = value; }
        /// <summary>
        /// Seperates each column of data on the outer edges of the table (Vertical)
        /// </summary>
        public char OuterColumnEdge { get => _outerColumnEdge; set => _outerColumnEdge = value; }
        /// <summary>
        /// Seperates each row of data inside the table (Horizontal)
        /// </summary>
        public char InnerRowDelimiter { get => _innerRowDelimiter; set => _innerRowDelimiter = value; }

        /// <summary>
        /// The bottom horizontal edge of the table
        /// Requires HasBottomRow == true
        /// </summary>
        public char OuterBottomRowEdge { get => _outerBottomRowEdge; set => _outerBottomRowEdge = value; }      
        /// <summary>
        /// Each inner row & column intersection
        /// </summary>
        public char InnerIntersection { get => _innerIntersection; set => _innerIntersection = value; }
        /// <summary>
        /// The outer right row & column intersections
        /// Default: InnerIntersection
        /// </summary>
        public char OuterRightVerticalIntersection { get => _outerRightVerticalIntersection; set => _outerRightVerticalIntersection = value; }
        /// <summary>
        /// The outer left row & column intersections
        /// Default: InnerIntersection
        /// </summary>
        public char OuterLeftVerticalIntersection { get => _outerLeftVerticalIntersection; set => _outerLeftVerticalIntersection = value; }
        
        //TODO: Use this setting
        /// <summary>
        /// Top horizontal intersections if there is no header
        /// Default: InnerIntersection
        /// </summary>
        public char OuterTopHorizontalIntersection { get => _outerTopHorizontalIntersection; set => _outerTopHorizontalIntersection = value; }
        /// <summary>
        /// Table bottom horizontal intersections
        /// Default: InnerIntersection
        /// </summary>
        public char OuterBottomHorizontalIntersection { get => _outerBottomHorizontalIntersection; set => _outerBottomHorizontalIntersection = value; }

        /// <summary>
        /// Default: InnerIntersection
        /// </summary>
        public char TopLeftCorner { get => _topLeftCorner; set => _topLeftCorner = value; }
        /// <summary>
        /// Default: InnerIntersection
        /// </summary>
        public char TopRightCorner { get => _topRightCorner; set => _topRightCorner = value; }
        /// <summary>
        /// Default: InnerIntersection
        /// </summary>
        public char BottomLeftCorner { get => _bottomLeftCorner; set => _bottomLeftCorner = value; }
        /// <summary>
        /// Default: InnerIntersection
        /// </summary>
        public char BottomRightCorner { get => _bottomRightCorner; set => _bottomRightCorner = value; }

        /// <summary>
        /// The top and bottom header rows
        /// </summary>
        public char HeaderRowDelimiter { get => _headerRowDelimiter; set => _headerRowDelimiter = value; }
        /// <summary>
        /// Top and bottom header delimiter rows, Row -> Column intersections
        /// Only used if HeaderTopIntersection and HeaderBottomIntersection are not defined
        /// </summary>
        public char HeaderIntersection { get => _headerIntersection; set => _headerIntersection = value; }
        /// <summary>
        /// Header top delimiter row. Row -> Column intersections
        /// </summary>
        public char HeaderTopIntersection { get => _headerTopIntersection; set => _headerTopIntersection = value; }
        /// <summary>
        /// Header bottom delimiter row. Row -> Colimn intersections 
        /// </summary>
        public char HeaderBottomIntersection { get => _headerBottomIntersection; set => _headerBottomIntersection = value; }

        private char _innerColumnDelimiter = '|';
        private char _outerColumnEdge = char.MinValue;
        private char _innerRowDelimiter = '-';
        private char _outerBottomRowEdge = char.MinValue;
        private char _innerIntersection = '-';
        private char _outerRightVerticalIntersection = char.MinValue;
        private char _outerLeftVerticalIntersection = char.MinValue;
        private char _outerTopHorizontalIntersection = char.MinValue;
        private char _outerBottomHorizontalIntersection = char.MinValue;
        private char _topLeftCorner = char.MinValue;
        private char _topRightCorner = char.MinValue;
        private char _bottomLeftCorner = char.MinValue;
        private char _bottomRightCorner = char.MinValue;
        private char _headerRowDelimiter = '-';
        private char _headerIntersection = '-';
        private char _headerTopIntersection = char.MinValue;
        private char _headerBottomIntersection = char.MinValue;


        //Sets the default values based on existing values
        private void SetDefaults()
        {
            SetDefault(ref _outerColumnEdge, InnerColumnDelimiter);
            SetDefault(ref _outerBottomRowEdge, InnerRowDelimiter);
            SetDefault(ref _outerRightVerticalIntersection, InnerIntersection);
            SetDefault(ref _outerLeftVerticalIntersection, InnerIntersection);
            SetDefault(ref _outerTopHorizontalIntersection, InnerIntersection);
            SetDefault(ref _outerBottomHorizontalIntersection, InnerIntersection);

            SetDefault(ref _topLeftCorner, InnerIntersection);
            SetDefault(ref _topRightCorner, InnerIntersection);
            SetDefault(ref _bottomLeftCorner, InnerIntersection);
            SetDefault(ref _bottomRightCorner, InnerIntersection);

            SetDefault(ref _headerTopIntersection, HeaderIntersection);
            SetDefault(ref _headerBottomIntersection, HeaderIntersection);
        }

        private void SetDefault(ref char character, char definition)
        {
            if(character == Char.MinValue)
            {
                character = definition;
            }
        }

        private void SetMarkdown()
        {
            hasInnerRows = false;
            hasTopRow = false;
            hasBottomRow = false;
            InnerColumnDelimiter = '|';
            InnerRowDelimiter = ' ';
            InnerIntersection = '|';
            HeaderIntersection = '|';
        }

        private void SetSimple()
        {
            hasInnerRows = false;
            hasTopRow = false;
            hasBottomRow = false;
            hasOuterColumns = false;
            InnerColumnDelimiter = ' ';
            InnerRowDelimiter = ' ';
            HeaderIntersection = ' ';
        }

        private void SetMySql()
        {
            InnerColumnDelimiter = '|';
            InnerIntersection = '+';
            HeaderIntersection = '+';
        }

        private void SetMySqlSimple()
        {
            hasInnerRows = false;
            InnerColumnDelimiter = '|';
            InnerIntersection = '+';
            HeaderIntersection = '+';
        }

        private void SetUnicode()
        {
            hasInnerRows = false;

            HeaderTopIntersection = '┬';
            HeaderBottomIntersection = '┼';

            OuterLeftVerticalIntersection = '├';
            OuterRightVerticalIntersection = '┤';
            OuterBottomHorizontalIntersection = '┴';

            TopLeftCorner = '┌';
            TopRightCorner = '┐';
            BottomLeftCorner = '└';
            BottomRightCorner = '┘';

            InnerColumnDelimiter = '│';
            OuterColumnEdge = '│';
            InnerRowDelimiter = '─';

            HeaderRowDelimiter = '─';
            HeaderTopIntersection = '┬';
            HeaderBottomIntersection = '┼';
            InnerIntersection = '┼';

            if (!Console.OutputEncoding.Equals(Encoding.UTF8))
            {
                Console.OutputEncoding = Encoding.UTF8;
            }
        }

        private void SetUnicodeAlt()
        {
            hasInnerRows = false;

            HeaderTopIntersection = '╦';
            HeaderBottomIntersection = '╬';

            OuterLeftVerticalIntersection = '╠';
            OuterRightVerticalIntersection = '╣';
            OuterBottomHorizontalIntersection = '╩';

            TopLeftCorner = '╔';
            TopRightCorner = '╗';
            BottomLeftCorner = '╚';
            BottomRightCorner = '╝';

            InnerColumnDelimiter = '║';
            OuterColumnEdge = '║';
            InnerRowDelimiter = '═';

            HeaderRowDelimiter = '═';
            HeaderTopIntersection = '╦';
            HeaderBottomIntersection = '╬';
            InnerIntersection = '╬';

            if (!Console.OutputEncoding.Equals(Encoding.UTF8))
            {
                Console.OutputEncoding = Encoding.UTF8;
            }
        }

        public static Config Default()
        {
            return new Config(Style.Default);
        }

        public static Config Markdown()
        {
            return new Config(Style.Markdown);
        }

        public static Config Simple()
        {
            return new Config(Style.Simple);
        }

        public static Config MySql()
        {
            return new Config(Style.MySql);
        }

        public static Config MySqlSimple()
        {
            return new Config(Style.MySqlSimple);
        }

        public static Config Unicode()
        {
            return new Config(Style.Unicode);
        }

        public static Config UnicodeAlt()
        {
            return new Config(Style.UnicodeAlt);
        }
    }
}
