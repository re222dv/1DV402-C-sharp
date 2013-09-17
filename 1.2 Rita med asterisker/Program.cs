using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace _1._2_Rita_med_asterisker {
    class Program {
        // ReasourceManager that loads the strings from the resource file
        private static ResourceManager rm = Strings.ResourceManager;
        // The Maximum allowed width of the diamond
        private const byte MaxAsterisks = 79;

        static void Main(string[] args) {
            byte asterisks;

            do {
                asterisks = ReadOddByte(String.Format(rm.GetString("enter_asterisks"), MaxAsterisks), MaxAsterisks);
                RenderDiamond(asterisks);
            } while (IsContinuing());
        }

        /// <summary>
        /// Ask the user if he wants to continue.
        /// </summary>
        /// <returns>true if he chooses to contine, false otherwise</returns>
        private static bool IsContinuing() {
            ViewMessage(rm.GetString("quit"));
            if (Console.ReadKey(true).Key == ConsoleKey.Escape) {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Reads an odd byte from the console.
        /// The method handles all errors that may appear and will continue
        /// to ask the user to enter a new value until it's valid.
        /// </summary>
        /// <param name="prompt">A help text for the user, defaults to null</param>
        /// <param name="maxValue">The maximum allowed value, defaults to 255</param>
        /// <returns>An odd byte</returns>
        private static byte ReadOddByte(string prompt = null, byte maxValue = 255) {
            byte value;
            do {
                Console.Write(prompt);
                try {
                    value = Byte.Parse(Console.ReadLine());

                    if (value > maxValue ||
                        value % 2 == 0 /* value is even */) {
                        ViewMessage(String.Format(rm.GetString("error_message"), MaxAsterisks), isError: true);
                    } else {
                        break;
                    }
                } catch (FormatException) {
                    ViewMessage(String.Format(rm.GetString("error_message"), MaxAsterisks), isError: true);
                } catch (OverflowException) {
                    ViewMessage(String.Format(rm.GetString("error_message"), MaxAsterisks), isError: true);
                }
            } while (true);
            return value;
        }

        /// <summary>
        /// Renders the diamond.
        /// </summary>
        /// <param name="maxCount">The number of asterisks in the middle</param>
        private static void RenderDiamond(byte maxCount) {
            // Render the first pyramid
            for (byte i = 1; i <= maxCount; i += 2) {
                RenderRow(maxCount, i);
            }
            // Render the second pyramid upside-down
            for (int i = maxCount - 2; i >= 1; i -= 2) {
                RenderRow(maxCount, i);
            }
        }

        /// <summary>
        /// Renders a row of the diamond.
        /// </summary>
        /// <param name="maxCount">The number of asterisks in the middle</param>
        /// <param name="asteriskCount">The number of asterisks on this row</param>
        private static void RenderRow(int maxCount, int asteriskCount) {
            int spaces = (maxCount - asteriskCount) / 2;
            for (int i = 0; i < spaces; i++) {
                Console.Write(" ");
            }
            for (int i = 0; i < asteriskCount; i++) {
                Console.Write("*");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Writes an message to the user.
        /// </summary>
        /// <param name="message">The message to write</param>
        /// <param name="isError">Formats the message as an error, defaults to false</param>
        private static void ViewMessage(string message, bool isError = false) {
            Console.WriteLine();
            if (isError) {
                Console.BackgroundColor = ConsoleColor.Red;
            } else {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
            }
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
