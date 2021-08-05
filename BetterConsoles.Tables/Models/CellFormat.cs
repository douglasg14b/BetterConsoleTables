using BetterConsoles.Colors;
using BetterConsoles.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoles.Tables.Models
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
            FontStyleExt fontStyle = FontStyleExt.None,
            bool innerFormatting = false)
            : base(foregroundColor, backgroundColor, fontStyle)
        {
            Alignment = alignment;
            InnerFormatting = innerFormatting;
        }

        public Alignment Alignment { get; set; } = Constants.DefaultAlignment;

        /// <summary>
        /// If the string for this cell has console formatting already baked into it
        /// This triggers a more expensive string length check that doesn't count formatting data
        /// </summary>
        public bool InnerFormatting { get; set; }


        public static ICellFormat operator +(CellFormat a, CellFormat b)
        {
            return Merge(a, b);
        }

        public static ICellFormat Merge(ICellFormat a, ICellFormat b)
        {
            return new CellFormat(
                a.Alignment == Constants.DefaultAlignment ? b.Alignment : a.Alignment,
                a.DefaultForeground ? b.ForegroundColor : a.ForegroundColor,
                a.DefaultBackground ? b.BackgroundColor : a.BackgroundColor,
                a.FontStyle | b.FontStyle,
                a.InnerFormatting || b.InnerFormatting);
        }

        new public static CellFormat Default()
        {
            return new CellFormat();
        }
    }
}
