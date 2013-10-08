using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV402.S2.L3C {
    public abstract class Shape {

        public ShapeType ShapeType {
            get;
            private set;
        }

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
}
