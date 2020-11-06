using BetterConsoles.Tables.Common;
using BetterConsoles.Tables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoles.Tables.Configuration
{
    // Unused right now
    internal static class ConfigBuilder
    {
        public static IBasicBuilder<CellFormat> CellConfig => new BasicBuilder<CellFormat>();
        public static IBasicBuilder<TableConfig> TableConfig => new BasicBuilder<TableConfig>();
    }


}
