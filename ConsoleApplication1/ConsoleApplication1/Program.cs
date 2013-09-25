using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] arr = { "hej", "blä", "lol", "hhh" };
            for (int i = arr.Length - 3; i < arr.Length; ++i) {
                Console.WriteLine(arr[i]);
            }
        }
    }
}
