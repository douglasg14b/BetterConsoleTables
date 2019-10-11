using BetterConsoleTables.Configuration;
using BetterConsoleTables.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Builders.Interfaces
{
    public interface IValueFormatBuilder : IValueFormatBuilder<IValueFormatBuilder>
    {
        ValueFormat GetFormat();
    }

    public interface IValueFormatBuilder<TBuilder>
    {
        TBuilder WithForegroundColor(Color color);
        TBuilder WithBackgroundColor(Color color);
        TBuilder WithAlignment(Alignment alignment);
        TBuilder WithFormatting(FormatType formats);
    }
}
