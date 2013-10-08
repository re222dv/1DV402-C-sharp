using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S2.L2C {
    class Program {
        public const string HorizontalLine = "\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550";

        private static void Main(string[] args) {
            AlarmClock ac;

            // Test 1
            ViewTestHeader("Test 1.\nTest av standardkonstruktorn.");
            ac = new AlarmClock();
            Console.WriteLine(ac.ToString());

            // Test 2
            ViewTestHeader("Test 2.\nTest av konstruktorn med två parametrar, (9, 42).");
            ac = new AlarmClock(9, 42);
            Console.WriteLine(ac.ToString());

            // Test 3
            ViewTestHeader("Test 3.\nTest av konstruktorn med fyra parametrar, (13, 24, 7, 35).");
            ac = new AlarmClock(13, 24, 7, 35);
            Console.WriteLine(ac.ToString());

            // Test 4
            ViewTestHeader("Test 4.\nTest av konstruktorn med minst två parametrar av typen string, (\"7:07\", \"7:10\", \"7:15\", \"7:30\").");
            ac = new AlarmClock("7:07", "7:10", "7:15", "7:30");
            Console.WriteLine(ac.ToString());

            // Test 5
            ViewTestHeader("Test 5.\nStäller befintligt AlarmClock-objekt till 23:58 och låter den gå 13 minuter.");
            ac.Time = "23:58";
            Run(ac, 13);

            // Test 6
            ViewTestHeader("Test 6.\nStäller befintligt AlarmClock-objekt till tiden 6:12 och alarmtiden till 6:15 och låter den gå 6 minuter.");
            ac.Time = "6:12";
            ac.AlarmTimes = new String[] { "6:15" };
            Run(ac, 6);

            // Test 7
            ViewTestHeader("Test 7.\nTestar egenskaperna så att ett undantag kastas då tid och alarmtid tilldelas felaktiga värden.");
            try {
                ac.Time = "24:89";
            } catch (Exception e) {
                ViewErrorMessage(e.Message);
            }
            try {
                ac.AlarmTimes = new String[] { "7:69" };
            } catch (Exception e) {
                ViewErrorMessage(e.Message);
            }

            // Test 8
            ViewTestHeader("Test 8.\nTestar konstruktorer så att ett undantag kastas då tid och alarmtid tilldelas felaktiga värden.");
            try {
                ac = new AlarmClock("32:03", "27:00");
            } catch (Exception e) {
                ViewErrorMessage(e.Message);
            }
        }

        /// <summary>
        /// Runs the clock the specified minutes and prints the result
        /// </summary>
        /// <param name="ac">The clock to run</param>
        /// <param name="minutes">The amount of minutes to run</param>
        private static void Run(AlarmClock ac, int minutes) {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" \u2554\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2557 ");
            Console.WriteLine(" \u2551       Väckarklockan URLED (TM)      \u2551 ");
            Console.Write(" \u2551        ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Modellnr.: 1DV402S2L2C");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("       \u2551 ");
            Console.WriteLine(" \u255A\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u255D ");
            Console.ResetColor();
            for (int i = 0; i < minutes; i++) {
                if (ac.TickTock()) {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine(" \u266B  {0}  BEEP! BEEP! BEEP! BEEP!", ac.ToString());
                    Console.ResetColor();
                } else {
                    Console.WriteLine("    {0}", ac.ToString());
                }
            }
        }

        /// <summary>
        /// Prints a formated error message
        /// </summary>
        /// <param name="message"></param>
        private static void ViewErrorMessage(string message) {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Prints a formatted test header
        /// </summary>
        /// <param name="header"></param>
        private static void ViewTestHeader(string header) {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n{0}\n{1}\n", HorizontalLine, header);
            Console.ResetColor();
        }
    }
}
