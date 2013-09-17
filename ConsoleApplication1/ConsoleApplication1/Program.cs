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
            int earnings = 0;

            for (int i = 0; i <= 100; i += 2)
            {
                earnings += i;
            }
            Console.WriteLine("Earned: " + earnings);
        }
    }
}
