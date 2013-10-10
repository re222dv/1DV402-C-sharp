using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV402.S2.L3C {
    public class Cylinder : Shape3D {
        /// <summary>
        /// Calculates the MantelArea of the Shape
        /// </summary>
        public override double MantelArea {
            get {
                return _baseShape.Perimeter * Height;
            }
        }

        /// <summary>
        /// Calculates the TotalSurfaceArea of the Shape
        /// </summary>
        public override double TotalSurfaceArea {
            get {
                return MantelArea + 2 * _baseShape.Area;
            }
        }

        /// <summary>
        /// Calculates the Volume of the Shape
        /// </summary>
        public override double Volume {
            get {
                return _baseShape.Area * Height;
            }
        }

        public Cylinder(double hradius, double vradius, double height) : base(ShapeType.Cylinder, new Ellipse(hradius*2, vradius*2), height) {}
    }
}
