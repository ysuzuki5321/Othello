using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hash;
using Utilitys;
namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var sha256 = new System.Security.Cryptography.SHA256Managed();
            Console.WriteLine(HashBuilder.Build("abcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopq"));
            Console.ReadKey();
        }
    }
}
