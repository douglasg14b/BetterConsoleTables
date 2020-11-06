using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoles.Tables
{
    public static class Constants
    {
        public static readonly Color DefaultForegroundColor = Color.LightGray;
        public static readonly Color DefaultBackgroundColor = Color.Black;

        public const Alignment DefaultAlignment = Alignment.Left;

        public static Dictionary<string, Dictionary<string, Dictionary<string, char>>> Boxes = 
            new Dictionary<string, Dictionary<string, Dictionary<string, char>>>()
        {
            ["Light"] = new Dictionary<string, Dictionary<string, char>>()
            {
                ["Default"] = new Dictionary<string, char>()
                {
                    ["Horizontal"] = '─',
                    ["Vertical"] = '│',
                    ["UpperLeft"] = '┌',
                    ["UpperRight"] = '┐',
                    ["LowerRight"] = '┘',
                    ["LowerLeft"] = '└',
                    ["Intersection"] = '┼',
                    ["LeftOuterIntersection"] = '├',
                    ["RightOuterIntersection"] = '┤',
                    ["BottomOuterIntersection"] = '┴',
                    ["TopOuterIntersection"] = '┬',
                }
            },

            ["Heavy"] = new Dictionary<string, Dictionary<string, char>>()
            {
                ["Default"] = new Dictionary<string, char>()
                {
                    ["Horizontal"] = '━',
                    ["Vertical"] = '┃',
                    ["UpperLeft"] = '┏',
                    ["UpperRight"] = '┓',
                    ["LowerRight"] = '┛',
                    ["LowerLeft"] = '┗',
                    ["Intersection"] = '╋',
                    ["LeftOuterIntersection"] = '┣',
                    ["RightOuterIntersection"] = '┫',
                    ["BottomOuterIntersection"] = '┻',
                    ["TopOuterIntersection"] = '┳',
                },
            },

            ["Double"] = new Dictionary<string, Dictionary<string, char>>()
            {
                ["Default"] = new Dictionary<string, char>()
                {
                    ["Horizontal"] = '═',
                    ["Vertical"] = '║',
                    ["UpperLeft"] = '╔',
                    ["UpperRight"] = '╗',
                    ["LowerRight"] = '╝',
                    ["LowerLeft"] = '╚',
                    ["Intersection"] = '╬',
                    ["LeftOuterIntersection"] = '╠',
                    ["RightOuterIntersection"] = '╣',
                    ["BottomOuterIntersection"] = '╩',
                    ["TopOuterIntersection"] = '╦',
                },
            }
        };
    }
}
