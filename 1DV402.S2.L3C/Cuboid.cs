using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV402.S2.L3C {
    public class Cuboid : Shape3D {
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

        public Cuboid(double length, double width, double height) : base(ShapeType.Cuboid, new Rectangle(length, width), height) {}
    }
}
