using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV402.S2.L3C {
    public abstract class Shape3D : Shape, IComparable {
        public Shape2D _baseShape;
        private double _height;

        /// <summary>
        /// The height of the shape
        /// </summary>
        public double Height {
            get {
                return _height;
            }
            set {
                if (!(value > 0)) {
                    throw new ArgumentException("Height needs to be greater than zero");
                }
                _height = value;
            }
        }

        public abstract double MantelArea {
            get;
        }

        public abstract double TotalSurfaceArea {
            get;
        }

        public abstract double Volume {
            get;
        }

        protected Shape3D(ShapeType shapeType, Shape2D baseShape, double height) : base(shapeType) {
            _baseShape = baseShape;
            Height = height;
        }

        public int CompareTo(object obj) {
            if (obj == null) {
                return 1;
            }

            if (!(obj is Shape3D)) {
                throw new ArgumentException();
            }

            return (int) Math.Round(Volume - ((Shape3D) obj).Volume);
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
                    return String.Format("Längd           :{0,10:F1}\n"+
                                         "Bredd           :{1,10:F1}\n"+
                                         "Höjd            :{2,10:F1}\n"+
                                         "Mantelarea      :{3,10:F1}\n"+
                                         "Begränsningarea :{4,10:F1}\n"+
                                         "Volym           :{5,10:F1}\n",
                                         _baseShape.Length, _baseShape.Width, Height,
                                         MantelArea, TotalSurfaceArea, Volume);
                case "R":
                    return String.Format("{0,-10}{1,6:F1}{2,6:F1}{3,6:F1}{4,13:F1}{5,13:F1}{6,13:F1}",
                                         ShapeType.AsText(),
                                         _baseShape.Length, _baseShape.Width, Height,
                                         MantelArea, TotalSurfaceArea, Volume);
                default:
                    throw new FormatException("format needs to be G or R");
            }
        }
    }
}
