using OpenVMSysPluginSystem;
namespace OpenVMSys_Console.Module
{
    internal class Decoder
    {
        private static bool serviceIsRunning = false;
        private static System.Diagnostics.Process? process;
        private static OperatingSystem operatingSystem = Environment.OSVersion;
        private static PluginManager pluginManager = new();
        public static void Decode(string mainArg, string[] args, string[] appStartArgs)
        {
            SecurityKeyService Service = new();
            switch (mainArg.ToLower())
            {
                case "service":
                    {
                        if (args.Contains("start"))
                        {
                            if (serviceIsRunning)
                            {
                                Output.PrintError("Service already running!",null);
                            }
                            else
                            {
                                pluginManager.RenewConfig();
                                process = System.Diagnostics.Process.Start("./OpenVMSys-Core");
                                serviceIsRunning = true;
                            }
                        }
                        else if (args.Contains("path"))
                        {
                            System.Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
                        }
                        else if (args.Contains("stop"))
                        {
                            try
                            {
                                process.Kill();
                                Output.PrintResult("Service terminated");
                                serviceIsRunning = false;
                            }
                            catch(Exception ex)
                            {
                                Output.PrintError(ex.Message, null);
                            }
                        }
                        else if (args.Contains("status"))
                        {
                            var memoeryUsage = process == null ? "0 bytes" : process.WorkingSet.ToString()+" bytes";
                            Output.PrintResult("Running: " + serviceIsRunning + "\nPhysical Memory Usage: " + memoeryUsage);
                        }
                        break;
                    }
                case "exit":
                case "quit":
                    System.Environment.Exit(0);
                    break;
                case "apikey":
                    {
                        if (args.Contains("add") && args.Length > 2)
                        {
                            try
                            {
                                Service.Create(args[1], int.Parse(args[2]));
                                Output.PrintResult("key added");
                            }
                            catch
                            {
                                Output.PrintError("\nBroken argument(s), add -help to get help list.", HelpList.ApiKeyAdd());
                            }
                        }
                        else if (args.Contains("del") && args.Length > 1)
                        {
                            try
                            {
                                Service.Delete(args[1]);
                            }
                            catch (Exception e)
                            {
                                Output.PrintError("\nBroken argument(s), add -help to get help list.", HelpList.ApiKeyDel());
                                Output.PrintError(e.Message, null);
                            }
                        }
                        else if (args.Contains("gen") && args.Length > 1)
                        {
                            try
                            {
                                var value = Service.Generate(int.Parse(args[1]));
                                if (value == "false")
                                {
                                    Output.PrintError("Fail: an unknown error occured", null);
                                }
                                Output.PrintResult("Apikey generated with value - " + value);
                            }
                            catch (Exception e)
                            {
                                Output.PrintError("\n" + e.ToString() + "\n add -help to get help list.", HelpList.ApiKeyGen());
                            }
                        }
                        else if (args.Contains("l"))
                        {
                            Service.Get();
                        }
                        break;
                    }
                case "plugin":
                    {
                        var IPM = new PluginManager();
                        if (args.Contains("l"))
                        {
                            var pluginCore = new OpenVMSysPluginCore();
                            var pluginList = pluginCore.LoadAllPlugins();
                            foreach(var plugin in pluginList)
                            {
                                Console.WriteLine("{0}\tEnabled: {1}",plugin.GetType().Name,IPM.IsEnabled(plugin.GetType().Name));
                            } ;
                        }
                        else if (args.Contains("toggle"))
                        {
                            IPM.TogglePlugin(args[1]);
                        }
                        break;
                    }
            }
        }
    }
}
