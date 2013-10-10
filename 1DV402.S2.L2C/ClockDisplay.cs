using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S2.L2C {
    class ClockDisplay {
        public const byte Hours = 24;
        public const byte Minutes = 60;

        private NumberDisplay _hourDisplay;
        private NumberDisplay _minuteDisplay;

        /// <summary>
        /// Holds the time
        /// </summary>
        public string Time {
            get {
                return ToString();
            }
            set {
                if (System.Text.RegularExpressions.Regex.IsMatch(value, "^(([0-1]?[0-9])|([2][0-3])):([0-5][0-9])$")) {
                    string[] split = value.Split(':');
                    _hourDisplay = new NumberDisplay(Hours, int.Parse(split[0]));
                    _minuteDisplay = new NumberDisplay(Minutes, int.Parse(split[1]));
                } else {
                    throw new ArgumentException(String.Format("Strängen '{0}' kan inte tolkas som en tid i formatet HH:mm.", value));
                }
            }
        }

        public ClockDisplay() : this(0, 0) {
        }

        public ClockDisplay(int hour, int minute) {
            _hourDisplay = new NumberDisplay(Hours, hour);
            _minuteDisplay = new NumberDisplay(Minutes, minute);
        }

        public ClockDisplay(string time) {
            Time = time;
        }

        public override bool Equals(object obj) {
            return obj.ToString() == ToString();
        }

        public override int GetHashCode() {
            return ToString().GetHashCode();
        }

        /// <summary>
        /// Increments the time by one minute
        /// </summary>
        public void Increment() {
            _minuteDisplay.Increment();
            if (_minuteDisplay.Number == 0) {
                _hourDisplay.Increment();
            }
        }

        public override string ToString() {
            return String.Format("{0}:{1}", _hourDisplay.ToString("0"), _minuteDisplay.ToString("00"));
        }

        public static bool operator ==(ClockDisplay a, ClockDisplay b) {
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

        public static bool operator !=(ClockDisplay a, ClockDisplay b) {
            return !(a == b);
        }
    }
}
