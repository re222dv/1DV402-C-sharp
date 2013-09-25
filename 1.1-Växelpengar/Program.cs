using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace _1._1_Växelpengar {

    class Program {
        // ReasourceManager that loads the strings from th resource file.
        private static ResourceManager rm = Strings.ResourceManager;
        // Denominations larger than this will be treated as bills.
        private const uint LargestCoin = 10;

        static void Main(string[] args) {
            // Array with the available Swedish denominatons.
            uint[] denominations = { 500, 100, 50, 20, 10, 5, 1 };

            double subtotal, roundingOffAmount;
            uint cash, total, change;
            uint[] notes;

            do {
                // Read the total sum
                subtotal = ReadPositiveDouble(Strings.enter_total);

                // Calculate the reminder when rounding to whole crowns.
                total = (uint)Math.Round(subtotal);
                roundingOffAmount = total - subtotal;

                // Read the received cash
                cash = ReadUInt(rm.GetString("enter_received"), total);

                // Calculate the change the customer will receive
                change = cash - total;

                notes = SplitIntoDenominations(change, denominations);

                ViewReceipt(subtotal, roundingOffAmount, total, cash, change, notes, denominations);

                // Ask for new calculation or exit
                ViewMessage(rm.GetString("new_calc"));
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        /// <summary>
        /// Reads a double from the console and makes sure that when rounded
        /// the value is not less than one (1).
        /// The method handles all errors that may appear and will continue
        /// to ask the user to enter a new value until it's valid.
        /// </summary>
        /// <param name="promt">The texts that should be displayed as a help</param>
        /// <returns>A positive double</returns>
        private static double ReadPositiveDouble(string promt) {
            string raw;
            double value;
            do {
                Console.Write("{0,-20}: ", promt);
                raw = Console.ReadLine();
                try {
                    value = Double.Parse(raw);

                    if ((int)Math.Round(value) < 1) {
                        ViewMessage(String.Format(rm.GetString("not_valid"), raw), isError: true);
                    } else {
                        break;
                    }
                } catch (FormatException) {
                    ViewMessage(String.Format(rm.GetString("not_valid"), raw), isError: true);
                } catch (OverflowException) {
                    ViewMessage(String.Format(rm.GetString("not_valid"), raw), isError: true);
                }
            } while (true);
            return value;
        }

        /// <summary>
        /// Reads a positive integral from the console.
        /// The method handles all errors that may appear and will continue
        /// to ask the user to enter a new value until it's valid.
        /// </summary>
        /// <param name="promt">The texts that should be displayed as a help</param>
        /// <param name="minValue">The minimum allowed value</param>
        /// <returns>A positive integral</returns>
        private static uint ReadUInt(string promt, uint minValue) {
            string line;
            uint raw;
            do {
                Console.Write("{0,-20}: ", promt);
                line = Console.ReadLine();
                try {
                    raw = UInt32.Parse(line);

                    if (raw < minValue) {
                        ViewMessage(String.Format(rm.GetString("too_small"), line), isError: true);
                    } else {
                        break;
                    }
                } catch (FormatException) {
                    ViewMessage(String.Format(rm.GetString("not_valid"), line), isError: true);
                } catch (OverflowException) {
                    ViewMessage(String.Format(rm.GetString("not_valid"), line), isError: true);
                }
            } while (true);
            return raw;
        }

        /// <summary>
        /// Splits the change in denominations.
        /// </summary>
        /// <param name="change">The change</param>
        /// <param name="denominations">The availble denominations</param>
        /// <returns>An array of how many coins and bills of each denomination</returns>
        private static uint[] SplitIntoDenominations(uint change, uint[] denominations) {
            List<uint> notes = new List<uint>();
            foreach (uint denomination in denominations) {
                notes.Add(change / denomination);
                change %= denomination;
            }
            return notes.ToArray();
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

        /// <summary>
        /// Writes a nicley formatted receipt to the user.
        /// </summary>
        /// <param name="subtotal">The total cost</param>
        /// <param name="roundingOffAmount">The amount rounded off</param>
        /// <param name="total">The total vost rounded to whole crowns</param>
        /// <param name="cash">The cash received</param>
        /// <param name="change">The change to the customer</param>
        /// <param name="notes">The amount of each coin or bill</param>
        /// <param name="denominations">The availble coin and bills</param>
        private static void ViewReceipt(double subtotal, double roundingOffAmount, uint total,
                               uint cash, uint change, uint[] notes, uint[] denominations) {
            Console.WriteLine();
            Console.WriteLine(rm.GetString("receipt"));
            Console.WriteLine(rm.GetString("separator"));
            Console.WriteLine("{1,-16}:{0,12:c}", subtotal, rm.GetString("total"));
            Console.WriteLine("{1,-16}:{0,12:c}", roundingOffAmount, rm.GetString("rounding_off_amount"));
            Console.WriteLine("{1,-16}:{0,12:c0}", total, rm.GetString("to_pay"));
            Console.WriteLine("{1,-16}:{0,12:c0}", cash, rm.GetString("cash"));
            Console.WriteLine("{1,-16}:{0,12:c0}", change, rm.GetString("change"));
            Console.WriteLine(rm.GetString("separator"));
            Console.WriteLine();

            for (int i = 0; i < denominations.Length; i++) {
                if (notes[i] > 0) {
                    if (notes[i] > LargestCoin) {
                        Console.WriteLine("{0,3}-{2,-12}: {1}", denominations[i], notes[i], rm.GetString("bills"));
                    } else {
                        Console.WriteLine("{0,3}-{2,-12}: {1}", denominations[i], notes[i], rm.GetString("coins"));
                    }
                }
            }
        }
    }
}
