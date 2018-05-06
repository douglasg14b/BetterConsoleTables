using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BetterConsoleTables
{
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
        public char InnerColumnDivider { get => _innerColumnDelimiter; set => _innerColumnDelimiter = value; }
        /// <summary>
        /// Seperates each column of data on the outer edges of the table (Vertical)
        /// </summary>
        public char OuterColumnDelimiter { get => _outerColumnDelimiter; set => _outerColumnDelimiter = value; }
        /// <summary>
        /// Seperates each row of data inside the table (Horizontal)
        /// </summary>
        public char InnerRowDivider { get => _innerRowDivider; set => _innerRowDivider = value; }      
        /// <summary>
        /// The outer horizontal edge of the table
        /// </summary>
        public char OuterRowDivider { get => _outerRowDivider; set => _outerRowDivider = value; }      
        /// <summary>
        /// Each inner row/column intersection
        /// </summary>
        public char InnerIntersection { get => _innerIntersection; set => _innerIntersection = value; }
        public char OuterRightVerticalIntersection { get => _outerRightVerticalIntersection; set => _outerRightVerticalIntersection = value; }
        public char OuterLeftVerticalIntersection { get => _outerLeftVerticalIntersection; set => _outerLeftVerticalIntersection = value; }
        /// <summary>
        /// Only used if here is no header
        /// </summary>
        public char OuterTopHorizontalIntersection { get => _outerTopHorizontalIntersection; set => _outerTopHorizontalIntersection = value; }
        public char OuterBottomHorizontalIntersection { get => _outerBottomHorizontalIntersection; set => _outerBottomHorizontalIntersection = value; }
        public char TopLeftCorner { get => _topLeftCorner; set => _topLeftCorner = value; }
        public char TopRightCorner { get => _topRightCorner; set => _topRightCorner = value; }
        public char BottomLeftCorner { get => _bottomLeftCorner; set => _bottomLeftCorner = value; }
        public char BottomRightCorner { get => _bottomRightCorner; set => _bottomRightCorner = value; }

        /// <summary>
        /// The top and bottom header rows
        /// </summary>
        public char HeaderRowDivider { get => _headerRowDivider; set => _headerRowDivider = value; }
        /// <summary>
        /// Top and bottom header row -> column intersections. Only used if top and bottom are not defined
        /// </summary>
        public char HeaderIntersection { get => _headerIntersection; set => _headerIntersection = value; }
        /// <summary>
        /// Top header row -> column intersections 
        /// </summary>
        public char HeaderTopIntersection { get => _headerTopIntersection; set => _headerTopIntersection = value; }
        /// <summary>
        /// Bottom header row -> column intersections 
        /// </summary>
        public char HeaderBottomIntersection { get => _headerBottomIntersection; set => _headerBottomIntersection = value; }

        private char _innerColumnDelimiter = '|';
        private char _outerColumnDelimiter = char.MinValue;
        private char _innerRowDivider = '-';
        private char _outerRowDivider = char.MinValue;
        private char _innerIntersection = '-';
        private char _outerRightVerticalIntersection = char.MinValue;
        private char _outerLeftVerticalIntersection = char.MinValue;
        private char _outerTopHorizontalIntersection = char.MinValue;
        private char _outerBottomHorizontalIntersection = char.MinValue;
        private char _topLeftCorner = char.MinValue;
        private char _topRightCorner = char.MinValue;
        private char _bottomLeftCorner = char.MinValue;
        private char _bottomRightCorner = char.MinValue;
        private char _headerRowDivider = '-';
        private char _headerIntersection = '-';
        private char _headerTopIntersection = char.MinValue;
        private char _headerBottomIntersection = char.MinValue;


        //Sets the default values based on existing values
        private void SetDefaults()
        {
            SetDefault(ref _outerColumnDelimiter, InnerColumnDivider);
            SetDefault(ref _outerRowDivider, InnerRowDivider);
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
            InnerColumnDivider = '|';
            InnerRowDivider = ' ';
            InnerIntersection = '|';
            HeaderIntersection = '|';
        }

        private void SetSimple()
        {
            hasInnerRows = false;
            hasTopRow = false;
            hasBottomRow = false;
            hasOuterColumns = false;
            InnerColumnDivider = ' ';
            InnerRowDivider = ' ';
            HeaderIntersection = ' ';
        }

        private void SetMySql()
        {
            InnerColumnDivider = '|';
            InnerIntersection = '+';
            HeaderIntersection = '+';
        }

        private void SetMySqlSimple()
        {
            hasInnerRows = false;
            InnerColumnDivider = '|';
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

            InnerColumnDivider = '│';
            OuterColumnDelimiter = '│';
            InnerRowDivider = '─';

            HeaderRowDivider = '─';
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

            InnerColumnDivider = '║';
            OuterColumnDelimiter = '║';
            InnerRowDivider = '═';

            HeaderRowDivider = '═';
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
