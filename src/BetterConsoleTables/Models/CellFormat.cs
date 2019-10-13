using BetterConsole.Colors;
using BetterConsole.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Models
{
    /// <summary>
    /// The Format configuration that will be applied to a table cell
    /// In addition to <seealso cref="Format"/> this controls alignment
    /// </summary>
    public class CellFormat : Format, ICellFormat
    {
        public CellFormat() { }

        public CellFormat(Alignment alignment = Constants.DefaultAlignment,
            Color foregroundColor = default,
            Color backgroundColor = default,
            FontStyleExt fontStyle = FontStyleExt.None)
            : base(foregroundColor, backgroundColor, fontStyle)
        {
            Alignment = alignment;
        }

        public Alignment Alignment { get; set; } = Constants.DefaultAlignment;

        new public static CellFormat Default()
        {
            return new CellFormat();
        }
    }
}
