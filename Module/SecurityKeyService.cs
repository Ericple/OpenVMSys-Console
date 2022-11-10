using System.Text;

namespace OpenVMSys_Console.Module
{
    internal class SecurityKeyService
    {
        private int _rep = 0;
        public static List<SecurityKey> KeyCollection = new();
        private string keyFilePath = AppDomain.CurrentDomain.BaseDirectory + "key";
        public void Get()
        {
            FileStream getStream;
            StreamReader getReader;
            try
            {
                getStream = new FileStream(keyFilePath, FileMode.Open);
                getReader = new StreamReader(getStream, Encoding.UTF8);
                var rawData = getReader.ReadToEnd();
                var keys = rawData.Split("\n");
                KeyCollection.Clear();
                foreach (var key in keys)
                {
                    var obj = key.Split("\t");
                    if (obj.Length > 1)
                    {
                        KeyCollection.Add(new SecurityKey(key.Split("\t")[0], int.Parse(key.Split("\t")[1])));
                    }
                }
                var haveMulti = "s";
                if(KeyCollection.Count <= 1)
                {
                    haveMulti = "";
                }
                Console.WriteLine("{0} SecurityKey{1} found", KeyCollection.Count,haveMulti);
                foreach (var SecurityKey in KeyCollection)
                {
                    System.Console.WriteLine("{0}\t{1}", SecurityKey.Key, SecurityKey.Permission);
                }
                getStream.Close();
                getReader.Close();
            }
            catch
            {
                File.WriteAllText(keyFilePath, "");
                Console.WriteLine("SecurityKey initialized");
            }
        }
        public bool Auth(string SecurityKey, int permission)
        {
            Get();
            foreach (var key in KeyCollection)
            {
                if (SecurityKey == key.Key && permission >= key.Permission)
                {
                    Console.WriteLine("No Match key");
                    return true;
                }
            }

            return false;
        }

        public bool Create(string value, int permission)
        {
            KeyCollection.Insert(KeyCollection.Count, new SecurityKey(value, permission));
            Console.WriteLine("Key Count: {0}", KeyCollection.Count);
            var keyStream = new FileStream(keyFilePath, FileMode.Append);
            var keyWriter = new StreamWriter(keyStream);
            keyWriter.WriteLine("\n" + value + "\t" + permission);
            keyWriter.Flush();
            keyWriter.Close();
            keyStream.Close();
            return true;
        }

        public void Delete(string value)
        {
            Get();
            if(KeyCollection.Find(delegate (SecurityKey key) { return key.Key.Equals(value); }) != null)
            {
                KeyCollection.Remove(KeyCollection.Find(delegate (SecurityKey key) { return key.Key.Equals(value); }));
            }
            else
            {
                Output.PrintError("SecurityKey: " + value + " does not exist", null);
                return;
            }
            
            Console.WriteLine("Key Count: {0}", KeyCollection.Count);
            File.Delete(keyFilePath);
            var keyStream = new FileStream(keyFilePath, FileMode.CreateNew);
            var keyWriter = new StreamWriter(keyStream);
            foreach (var SecurityKey in KeyCollection)
            {
                keyWriter.WriteLine("\n" + SecurityKey.Key + "\t" + SecurityKey.Permission);
            }
            keyWriter.Flush();
            keyWriter.Close();
            keyStream.Close();
        }
        public string Random()
        {
            string str = string.Empty;
            long num2 = DateTime.Now.Ticks + this._rep;
            _rep++;
            Random random = new(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> _rep)));
            for (int i = 0; i < 20; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str += ch.ToString();
            }

            return str;
        }
        public string Generate(int permission)
        {
            var value = Random();
            if (Create(value, permission))
            {
                return value;
            }
            return "fail";
        }
    }
}
