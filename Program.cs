/*
 * Created at 2022/10/24 by Guo Tingjin dev@peercat.cn
 */
using OpenVMSys_Console.Module;
using OpenVMSysPluginSystem;

var app = new Interpreter();
PluginManager pm = new();
Version version = new Version(2,0,104);

pm.RenewConfig();
app.Welcome(version);
while (true)
{
    app.Get(args);
}