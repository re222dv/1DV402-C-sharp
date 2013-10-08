using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV402.S2.L3C {
    public class Rectangle : Shape2D {
        public double Area {
            get {
                return Length * Width;
            }
        }

        public double Perimeter {
            get {
                return Length * 2 + Width * 2;
            }
        }

        public Rectangle(double length, double width) : base(ShapeType.Rectangle, length, width) {}
    }
}
