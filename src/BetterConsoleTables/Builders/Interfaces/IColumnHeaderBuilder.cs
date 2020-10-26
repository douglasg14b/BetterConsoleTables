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
    public interface IColumnHeaderBuilder<TColumnBuilder>
    {        
        IColumnValueFormatBuilder<TColumnBuilder> WithHeaderFormat();
        IColumnValueFormatBuilder<TColumnBuilder> WithHeaderFormat(CellFormat format);
        TColumnBuilder WithHeaderAlignment(Alignment alignment);
    }
}
