using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV402.S2.L3C {
    public class Ellipse : Shape2D {
        public double Area {
            get {
                throw new System.NotImplementedException();
            }
        }

        public double Perimeter {
            get {
                throw new System.NotImplementedException();
            }
        }

        public Ellipse(double diameter) : base(ShapeType.Circle, diameter, diameter) {}

        public Ellipse(double hdiameter, double vdiameter) : base(ShapeType.Ellipse, hdiameter, vdiameter) {}
    }
}
