using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV402.S2.L3C {
    public static class Extensions {
        /// <summary>
        /// Returns a text sumary of ShapeType
        /// </summary>
        public static string AsText(this ShapeType shapeType) {
            switch (shapeType) {
                case ShapeType.Circle:
                    return "Cirkel";
                case ShapeType.Cuboid:
                    return "Rätblock";
                case ShapeType.Cylinder:
                    return "Cylinder";
                case ShapeType.Ellipse:
                    return "Ellips";
                case ShapeType.Rectangle:
                    return "Rektangel";
                case ShapeType.Sphere:
                    return "Sfär";
                default:
                    return "Okänt";
            }

        }

        /// <summary>
        /// Centers a string in a string
        /// </summary>
        /// <param name="other">The string to center inside</param>
        /// <returns>The full string</returns>
        public static string CenterAlignString(this string s, string other) {
            int startLength = other.Length / 2 - s.Length / 2;
            int endLength = other.Length / 2 + s.Length / 2;
            if (s.Length % 2 == 1) {
                endLength++;
            }
            return String.Format("{0}{1}{2}", other.Substring(0, startLength), s, other.Substring(endLength));
        }

        /// <summary>
        /// Returns a random number between min and max
        /// </summary>
        public static double NextDouble(this Random r, double min, double max) {
            return r.NextDouble() * (max - min) + min;
        }
    }
}
