using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV402.S2.L3C {
    public abstract class Shape2D : Shape, IComparable {
        private double _length;
        private double _width;

        /// <summary>
        /// The length of the shape
        /// </summary>
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

        /// <summary>
        /// The width of the shape
        /// </summary>
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


        public abstract double Area {
            get;
        }

        public abstract double Perimeter {
            get;
        }

        protected Shape2D(ShapeType shapeType, double length, double width) : base(shapeType) {
            Length = length;
            Width = width;
        }

        public int CompareTo(object obj) {
            if (obj == null) {
                return 1;
            }

            if (!(obj is Shape2D)) {
                throw new ArgumentException();
            }

            return (int) Math.Round(Area - ((Shape2D) obj).Area);
        }

        public override string ToString() {
            return ToString("G");
        }

        /// <summary>
        /// Returns a string representation of the shape
        /// </summary>
        /// <param name="format">The format of the representation, G = multi row, R = single row</param>
        public override string ToString(string format) {
            switch (format) {
                case "G":
                case "":
                case null:
                    return String.Format("Längd  :{0,10:F1}\n"+
                                         "Bredd  :{1,10:F1}\n"+
                                         "Omkrets:{2,10:F1}\n"+
                                         "Area   :{3,10:F1}\n",
                                         Length, Width, Perimeter, Area);
                case "R":
                    return String.Format("{0,-10}{1,6:F1}{2,6:F1}{3,8:F1}{4,8:F1}",
                                         ShapeType.AsText(), Length, Width, Perimeter, Area);
                default:
                    throw new FormatException("format needs to be G or R");
            }            
        }
    }
}
