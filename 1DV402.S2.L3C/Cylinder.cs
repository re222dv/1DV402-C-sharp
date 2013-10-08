using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV402.S2.L3C {
    public class Cylinder : Shape3D {
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

        public Cylinder(double hradius, double vradius, double height) : base(ShapeType.Cylinder, new Ellipse(hradius*2, vradius*2), height) {}
    }
}
