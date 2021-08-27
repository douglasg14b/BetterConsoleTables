using BetterConsoles.Tables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoles.Tables.Builders.Interfaces
{
    public interface ICellBuilder<TBuilder> : ICellFormatBuilder<TBuilder>
    {
        ICell GetCell();
    }
}
