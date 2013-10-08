using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV402.S2.L3C {
    public class Sphere : Shape3D {
        public double MantelArea {
            get {
                throw new System.NotImplementedException();
            }
        }

        public double TotalSurfaceArea {
            get {
                throw new System.NotImplementedException();
            }
        }

        public double Volume {
            get {
                throw new System.NotImplementedException();
            }
        }

        public Sphere(double radious) : base(ShapeType.Sphere, new Ellipse(radious*2), radious*2) {}
    }
}
