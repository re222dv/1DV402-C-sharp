using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S2.L2C {
    class NumberDisplay {
        private int _maxNumber;
        private int _number;

        /// <summary>
        /// Holds the maximal allowed value, needs to be
        /// </summary>
        public int MaxNumber {
            get {
                return _maxNumber;
            }
            set {
                if (!(value > 0)) {
                    throw new ArgumentException("MaxNumber can't be below zero");
                }

                _maxNumber = value;
            }
        }

        /// <summary>
        /// Holds the current number
        /// </summary>
        public int Number {
            get {
                return _number;
            }
            set {
                if (0 > value || value >= MaxNumber) {
                    throw new ArgumentException("Number needs to be above zero and below or equal to Max Number");
                }

                _number = value;
            }
        }

        public NumberDisplay(int maxNumber) : this(maxNumber, 0) {}

        public NumberDisplay(int maxNumber, int number) {
            MaxNumber = maxNumber;
            Number = number;
        }

        public override bool Equals(object obj) {
            return ((NumberDisplay) obj).MaxNumber == MaxNumber &&
                   ((NumberDisplay) obj).Number == Number;
        }

        public override int GetHashCode() {
            return ToString().GetHashCode();
        }

        /// <summary>
        /// Increments the number by 1, if the value goes above MaxNumber it's reseted to zero. 
        /// </summary>
        public void Increment() {
            try {
                Number++;
            } catch (ArgumentException) {
                Number = 0;
            }
        }

        public override string ToString() {
            return ToString("0");
        }

        /// <summary>
        /// Outputs the number as a string
        /// </summary>
        /// <param name="format">Format to output, the allowed formats is 0 and 00</param>
        /// <returns></returns>
        public string ToString(string format) {
            if (format == "0" || format == "G") {
                return String.Format("{0}", Number);
            } else if (format == "00") {
                return String.Format("{0:D2}", Number);
            }
            throw new FormatException("Allowed formats is 0, G and 00");
        }

        public static bool operator ==(NumberDisplay a, NumberDisplay b) {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b)) {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object) a == null) || ((object) b == null)) {
                return false;
            }

            // Check the objects
            return a.Equals(b);
        }

        public static bool operator !=(NumberDisplay a, NumberDisplay b) {
            return !(a == b);
        }
    }
}
