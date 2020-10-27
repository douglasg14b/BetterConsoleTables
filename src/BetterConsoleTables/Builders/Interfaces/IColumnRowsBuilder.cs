using BetterConsoleTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Builders.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValueFormatBuilder">The parent value format builder this can return the call chain to</typeparam>
    /// <typeparam name="TColumnBuilder">The parent column builder this can return the call chain to</typeparam>
    public interface IColumnRowsBuilder<TColumnBuilder, TValueFormatBuilder>
    {
        TValueFormatBuilder WithRowsFormat();
        TValueFormatBuilder WithRowsFormat(CellFormat format);

        TColumnBuilder WithRowsAlignment(Alignment alignment);
    }
}
