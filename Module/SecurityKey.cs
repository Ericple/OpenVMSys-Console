using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenVMSys_Console.Module
{
    internal class SecurityKey
    {
        public string? Key;
        public int Permission;
        public SecurityKey(string? key, int permission)
        {
            Key = key;
            Permission = permission;
        }
    }
}
