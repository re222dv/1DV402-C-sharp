using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV402.S2.L3C {
    public class Ellipse : Shape2D {
        /// <summary>
        /// Calculates the Area of the Shape
        /// </summary>
        public override double Area {
            get {
                return Math.PI * (Length / 2) * (Width / 2);
            }
        }

        /// <summary>
        /// Calculates the Perimeter of the Shape
        /// </summary>
        public override double Perimeter {
            get {
                double a = Length / 2;
                double b = Width / 2;
                return Math.PI * Math.Sqrt(2 * a * a + 2 * b * b);
            }
        }

        public Ellipse(double diameter) : base(ShapeType.Circle, diameter, diameter) {}

        public Ellipse(double hdiameter, double vdiameter) : base(ShapeType.Ellipse, hdiameter, vdiameter) {}
    }
}
