using System;
using System.Text;
using System.Security;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace GenerateEncryptionKey
{
    class App
    {
        [STAThread]
        static void Main(string[] argv)
        {
            Console.WriteLine("\nWould you like to generate a key?  [y/n] ");
            var genNewKey = (Console.ReadKey(true).Key == ConsoleKey.Y);
            while (genNewKey)
            {
                var newKey = GereateKey(PromptSize());
                Clipboard.SetText(newKey);
                Console.WriteLine("\nHere is your new key (it is also ready to paste from your clipboard)\n\t" + newKey);
                Console.WriteLine("\nWould you like to generate a key?  [y/n] ");
                genNewKey = (Console.ReadKey(true).Key == ConsoleKey.Y);                
            };          
                
        }

        private static int PromptSize()
        {
            Console.WriteLine("Pick Key Size");
            Console.WriteLine(string.Format("\n\t1 - {0} bytes ({1} hexadecimal characters)", 64, 128));
            Console.WriteLine(string.Format("\n\t2 - {0} bytes ({1} hexadecimal characters)", 32, 64));
            Console.WriteLine(string.Format("\n\t3 - {0} bytes ({1} hexadecimal characters)", 24, 48));

            int len = 128;
            var input = Console.ReadKey(true).KeyChar;
            switch (input)
            {
                case '1':
                    len = 128;
                    break;
                case '2':
                    len = 64;
                    break;
                case '3':
                    len = 48;
                    break;
                default:
                    len = 128;
                    break;
            }
            return len;
        }

        private static string GereateKey(int len)
        {
            byte[] buff = new byte[len / 2];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buff);
            StringBuilder sb = new StringBuilder(len);
            for (int i = 0; i < buff.Length; i++)
                sb.Append(string.Format("{0:X2}", buff[i]));

            return sb.ToString();
        }



    }
}
