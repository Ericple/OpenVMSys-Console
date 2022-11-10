namespace OpenVMSys_Console.Module
{
    internal class Output
    {
        public static void PrintError(string error, string? usage)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.Write("\nError: \n");
            System.Console.ResetColor();
            System.Console.WriteLine(error);
            if (usage != null)
            {
                System.Console.ForegroundColor = ConsoleColor.Green;
                System.Console.Write("\nUsage: \n");
                System.Console.ResetColor();
                System.Console.WriteLine(usage);
            }
        }

        public static void PrintResult(string result)
        {
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.Write("\nSuccess: \n");
            System.Console.ResetColor();
            System.Console.WriteLine(result);
        }
    }
}
