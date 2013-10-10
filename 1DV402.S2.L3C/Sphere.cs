using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV402.S2.L3C {
    public class Sphere : Shape3D {
        /// <summary>
        /// Calculates the MantelArea of the Shape
        /// </summary>
        public override double MantelArea {
            get {
                return _baseShape.Area * 4;
            }
        }

        /// <summary>
        /// Calculates the TotalSurfaceArea of the Shape
        /// </summary>
        public override double TotalSurfaceArea {
            get {
                return MantelArea;
            }
        }

        /// <summary>
        /// Calculates the Volume of the Shape
        /// </summary>
        public override double Volume {
            get {
                return 4 / 3 * _baseShape.Area * (Height / 2);
            }
        }

        public Sphere(double radious) : base(ShapeType.Sphere, new Ellipse(radious*2), radious*2) {}
    }
}
