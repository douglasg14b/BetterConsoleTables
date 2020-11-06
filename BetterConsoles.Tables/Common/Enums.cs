using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables
{
    /// <summary>
    /// Text alignment
    /// </summary>
    public enum Alignment
    {
        Left = 0,
        Right = 1,
        Center = 2
    }


    /// <summary>
    /// Table style
    /// </summary>
    public enum Style
    {
        Default = 0,
        Markdown = 1,
        Simple = 2,
        MySql = 3,
        MySqlSimple = 4,
        Unicode = 5,
        UnicodeAlt = 6
    }
}
