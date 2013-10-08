using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV402.S2.L3C {
    public abstract class Shape3D : Shape, IComparable {
        public Shape2D _baseShape;
        private double _height;

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

        public abstract double MantelArea;

        public abstract double TotalSurfaceArea;

        public abstract double Volume;

        protected Shape3D(ShapeType shapeType, Shape2D baseShape, double height) : base(shapeType) {
            _baseShape = baseShape;
            Height = height;
        }

        public int CompareTo(object obj) {
            throw new System.NotImplementedException();
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
