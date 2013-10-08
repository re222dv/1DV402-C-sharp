using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S2.L2C {
    class AlarmClock {
        private ClockDisplay _time;
        private ClockDisplay[] _alarmTimes;

        /// <summary>
        /// Holds the current time
        /// </summary>
        public string Time {
            get {
                return _time.ToString();
            }
            set {
                _time = new ClockDisplay(value);
            }
        }

        /// <summary>
        /// Holds the alarm times
        /// </summary>
        public string[] AlarmTimes {
            get {
                string[] result = new string[_alarmTimes.Length];
                for (int i = 0; i < _alarmTimes.Length; i++) {
                    result[i] = _alarmTimes[i].ToString();
                }
                return result;
            }
            set {
                _alarmTimes = new ClockDisplay[value.Length];

                for (int i = 0; i < value.Length; i++) {
                    _alarmTimes[i] = new ClockDisplay(value[i]);
                }
            }
        }

        public AlarmClock() : this(0, 0) {}

        public AlarmClock(int hour, int minute) : this(hour, minute, 0, 0) {}

        public AlarmClock(int hour, int minute, int alarmHour, int alarmMinute) {
            _time = new ClockDisplay(hour, minute);
            _alarmTimes = new ClockDisplay[1];
            _alarmTimes[0] = new ClockDisplay(alarmHour, alarmMinute);
        }

        public AlarmClock(string time, params string[] alarmTimes) {
            Time = time;
            AlarmTimes = alarmTimes;
        }

        public override bool Equals(object obj) {
            return obj.ToString() == ToString();
        }

        public override int GetHashCode() {
            return ToString().GetHashCode();
        }

        /// <summary>
        /// Go one minute
        /// </summary>
        /// <returns> true if an alarm goes of</returns>
        public bool TickTock() {
            _time.Increment();

            foreach (ClockDisplay alarm in _alarmTimes) {
                if (alarm == _time) {
                    return true;
                }
            }
            return false;
        }

        public override string ToString() {
            return String.Format("{0,5} ({1})", Time, String.Join(", ", AlarmTimes));
        }

        public static bool operator ==(AlarmClock a, AlarmClock b) {
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

        public static bool operator !=(AlarmClock a, AlarmClock b) {
            return !(a == b);
        }
    }
}
