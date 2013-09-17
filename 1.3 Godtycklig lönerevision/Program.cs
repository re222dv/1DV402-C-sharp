using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1._3_Godtycklig_lönerevision {
    class Program {
        static void Main(string[] args) {
            int numberOfSalaries;
            int[] salaries;

            do {
                do {
                    numberOfSalaries = ReadInt("Ange antal löner att mata in: ");
                    if (numberOfSalaries < 2) {
                        ViewMessage("Du måste mata in minst två löner för att kunna göra en beräkning!",
                                    backgroundColor: ConsoleColor.Red);
                    } else {
                        break;
                    }
                } while (true);
                salaries = ReadSalaries(numberOfSalaries);
                ViewResult(salaries);
            } while (IsContinuing());
        }

        /// <summary>
        /// Ask the user if he wants to continue.
        /// </summary>
        /// <returns>true if he chooses to contine, false otherwise</returns>
        private static bool IsContinuing() {
            ViewMessage("Tryck tangent för att fortsätta - ESC avslutar.");
            if (Console.ReadKey(true).Key == ConsoleKey.Escape) {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Reads an integer from the console.
        /// The method handles all errors that may appear and will continue
        /// to ask the user to enter a new value until it's valid.
        /// </summary>
        /// <param name="prompt">A help text for the user</param>
        /// <returns>An integer</returns>
        private static int ReadInt(string prompt) {
            string line;
            int value;
            do {
                Console.Write(prompt);
                line = Console.ReadLine();
                try {
                    value = Int32.Parse(line);

                    break;
                } catch (FormatException) {
                    ViewMessage(String.Format("FEL! '{0}' kan inte tolkas som ett heltal!", line),
                                backgroundColor: ConsoleColor.Red);
                } catch (OverflowException) {
                    ViewMessage(String.Format("FEL! '{0}' kan inte tolkas som ett heltal!", line),
                                backgroundColor: ConsoleColor.Red);
                }
            } while (true);
            return value;
        }

        /// <summary>
        /// Reads all salaries from the console.
        /// The method handles all errors that may appear and will continue
        /// to ask the user to enter a new value until it's valid.
        /// </summary>
        /// <param name="count">The number of salaries to read</param>
        /// <returns>An integer array with all salaries</returns>
        private static int[] ReadSalaries(int count) {
            List<int> salaries = new List<int>();
            for (int i = 1; i <= count; i++) {
                salaries.Add(ReadInt(String.Format("Ange lön nummer {0}: ", i)));
            }
            return salaries.ToArray();
        }

        /// <summary>
        /// Writes an message to the user.
        /// </summary>
        /// <param name="message">The message to write</param>
        /// <param name="backgroundColor">The backgound color of the message, defaults to blue</param>
        /// <param name="backgroundColor">The foreground (text) color of the message, defaults to white</param>
        private static void ViewMessage(string message,
                                ConsoleColor backgroundColor = ConsoleColor.Blue,
                                ConsoleColor foregroundColor = ConsoleColor.White) {
            Console.WriteLine();
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// Writes the resut to the user.
        /// </summary>
        /// <param name="salaries">The array of salaries to output</param>
        private static void ViewResult(int[] salaries) {
            Console.WriteLine();
            Console.WriteLine("-------------------------");
            Console.WriteLine("Medianlön:    {0:c0}", salaries.Median());
            Console.WriteLine("Medellön:     {0:c0}", salaries.Average());
            Console.WriteLine("Lönespridnng: {0:c0}", salaries.Dispertion());
            Console.WriteLine("-------------------------");
            for (int i = 0; i < salaries.Length; i += 3) {
                for (int j = 0; i + j < salaries.Length && j < 3; j++) {
                    Console.Write("{0,7}", salaries[i + j]);
                }
                Console.WriteLine();
            }
        }
    }
}
