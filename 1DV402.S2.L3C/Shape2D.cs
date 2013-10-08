using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV402.S2.L3C {
    public abstract class Shape2D : Shape, IComparable {
        private double _length;
        private double _width;

        public double Area {
            get {
                throw new System.NotImplementedException();
            }
        }

        public double Length {
            get {
                return _length;
            }
            set {
                if (!(value > 0)) {
                    throw new ArgumentException("Length needs to be greater than zero");
                }
                _length = value;
            }
        }

        public abstract double Perimiter;

        public double Width {
            get {
                return _width;
            }
            set {
                if (!(value > 0)) {
                    throw new ArgumentException("Width needs to be greater than zero");
                }
                _width = value;
            }
        }

        protected Shape2D(ShapeType shapeType, double length, double width) : base(shapeType) {
            Length = length;
            Width = width;
        }

        public int CompareTo(object obj) {
            throw new NotImplementedException();
        }

        public string ToString() {
            return ToString("G");
        }

        public string ToString(string format) {
            switch (format) {
                case "G":
                case "":
                case null:
                    throw new System.NotImplementedException();
                case "R":
                    throw new System.NotImplementedException();
                default:
                    throw new FormatException("format needs to be G or R");
            }            
        }
    }
}
