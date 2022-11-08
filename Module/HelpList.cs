namespace OpenVMSys_Console.Module
{
    internal class HelpList
    {
        public static string ApiKeyAdd()
        {
            return "\napikey add [keyValue]-string [Permission]-int";
        }

        public static string ApiKeyDel()
        {
            return "\napikey del [keyValue]-string";
        }

        public static string ApiKeyGen()
        {
            return "\napikey gen [Permission]-int";
        }
    }
}
