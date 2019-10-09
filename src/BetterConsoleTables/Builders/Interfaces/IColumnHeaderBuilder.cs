using BetterConsoleTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Builders.Interfaces
{
    public interface IColumnHeaderBuilder
    {        
        IColumnValueFormatBuilder WithHeaderFormat();
        IColumnValueFormatBuilder WithHeaderFormat(ValueFormat format);

        IColumnBuilder WithHeaderAlignment(Alignment alignment);
    }
}
