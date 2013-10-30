using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV402.S3.L1 {
    /// <summary>
    /// A value type for an ingredient
    /// </summary>
    public struct Ingredient {
        public string Amount {
            get;
            set;
        }

        public string Measure {
            get;
            set;
        }

        public string Name {
            get;
            set;
        }

        public override string ToString() {
            return String.Join(" ", new string[] { Amount, Measure, Name }).Trim();
            ;
        }
    }
}
