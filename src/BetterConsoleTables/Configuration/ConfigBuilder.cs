using BetterConsoleTables.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Configuration
{
    public static class ConfigBuilder
    {
        public static IBasicBuilder<TableCellConfig> CellConfig => new BasicBuilder<TableCellConfig>();
        public static IBasicBuilder<TableConfiguration> TableConfig => new BasicBuilder<TableConfiguration>();
    }


}
