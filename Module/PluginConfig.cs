using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenVMSys_Console.Module
{
    internal class PluginConfig
    {
        public string PluginName;
        public bool IsEnabled;
        public PluginConfig(string pluginName, bool isEnabled)
        {
            PluginName = pluginName;
            IsEnabled = isEnabled;
        }
    }
}
