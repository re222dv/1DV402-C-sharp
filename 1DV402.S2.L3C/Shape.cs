using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV402.S2.L3C {
    public abstract class Shape {

        /// <summary>
        /// The type of the shape
        /// </summary>
        public ShapeType ShapeType {
            get;
            private set;
        }

        /// <summary>
        /// If the shape is a 3D shape
        /// </summary>
        public bool IsShape3D {
            get {
                switch (ShapeType) {
                    case ShapeType.Cuboid:
                    case ShapeType.Cylinder:
                    case ShapeType.Sphere:
                        return true;
                }

                return false;
            }
        }

        protected Shape(ShapeType shapeType) {
            ShapeType = shapeType;
        }

        public abstract string ToString(string format);
    }

    public enum ShapeType {
        Rectangle,
        Circle,
        Ellipse,
        Cuboid,
        Cylinder,
        Sphere,
    }
}
