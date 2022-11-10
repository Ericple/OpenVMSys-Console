using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenVMSys_Console.Module
{
    public class PluginManager
    {
        public OpenVMSys_Core.Plugins.OpenVMSysPluginCore pluginCore = new();
        private List<PluginConfig> GetConfig()
        {
            var pluginConfigList = new List<PluginConfig>();
            try
            {
                //使用文件流读取插件配置
                var configStream = new FileStream(Path.Join(new String[] { "pluginConfig" }), FileMode.OpenOrCreate);
                var configreader = new StreamReader(configStream, System.Text.Encoding.UTF8);
                var rawConfig = configreader.ReadToEnd();
                configStream.Flush();
                configreader.Close();
                configStream.Close();
                //在这里要完成后续的插件config格式化读取
                var pluginConfigs = rawConfig.Split("\n");
                foreach (string pluginConfig in pluginConfigs)
                {
                    if (pluginConfig.Split("\t").Length > 1)
                    {
                        pluginConfigList.Add(new PluginConfig(pluginConfig.Split("\t")[0], pluginConfig.Split("\t")[1] == "True" ? true : false));
                    }
                }
                return pluginConfigList;
            }
            catch (Exception e)
            {
                Output.PrintError("Plugin load error: " + e.Message + e.Data, null);
                try
                {
                    var configStream = new FileStream(Path.Join(new String[] { "pluginConfig" }), FileMode.OpenOrCreate);
                    var configWriter = new StreamWriter(configStream, System.Text.Encoding.UTF8);
                    configWriter.Write("");
                    configWriter.Flush();
                    configStream.Flush();
                    configWriter.Close();
                    configStream.Close();
                    return pluginConfigList;
                }
                catch
                {
                    Output.PrintError("Plugin Initialize Fail, please check your enviroument. Now exiting...", null);
                    System.Environment.Exit(203);
                }
                return pluginConfigList;
            }
        }
        private bool IsEnabled(List<PluginConfig> configs, string pluginName)
        {
            foreach(var config in configs)
            {
                if (config.PluginName == pluginName && config.IsEnabled == true)
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsEnabled(string pluginName)
        {
            return IsEnabled(GetConfig(), pluginName);
        }
        public void RenewConfig()
        {
            var nowConfigs = GetConfig();
            var newerPlugins = pluginCore.LoadAllPlugins();
            var nowList = new List<string>();
            foreach(var nowConfig in nowConfigs)
            {
                nowList.Add(nowConfig.PluginName);
            }
            foreach(var newerPlugin in newerPlugins)
            {
                if (nowList.Contains(newerPlugin.GetType().Name))
                {
                    continue;
                }
                else
                {
                    WriteConfig(newerPlugin.GetType().Name, false);
                }
            }
        }
        private bool WriteConfig(string pluginName,bool isEnabled)
        {
            try
            {
                var configFileStream = new FileStream(Path.Join(new string[] { "pluginCinfig" }), FileMode.Open);
                var configFileWriter = new StreamWriter(configFileStream, System.Text.Encoding.UTF8);
                configFileWriter.Write(pluginName + "\t" + isEnabled + "\n");
                configFileWriter.Flush();
                configFileWriter.Close();
                return true;
            }
            catch(Exception ex)
            {
                Output.PrintError("Plugin config set error: " + ex.Message, null);
                return false;
            }
            
        }
    }
}
