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
        public static IBasicBuilder<ValueFormat> CellConfig => new BasicBuilder<ValueFormat>();
        public static IBasicBuilder<TableConfig> TableConfig => new BasicBuilder<TableConfig>();
    }


}
