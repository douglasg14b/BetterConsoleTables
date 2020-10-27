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
    /// <typeparam name="TColumnBuilder">The parent column builder this can return the call chain to</typeparam>
    /// <typeparam name="TColumnValueFormatBuilder">The parent column format builder this can return the call chain to</typeparam>
    public interface IColumnHeaderBuilder<TColumnBuilder, TColumnValueFormatBuilder>
    {
        TColumnValueFormatBuilder WithHeaderFormat();
        TColumnValueFormatBuilder WithHeaderFormat(CellFormat format);
        TColumnBuilder WithHeaderAlignment(Alignment alignment);
    }
}
