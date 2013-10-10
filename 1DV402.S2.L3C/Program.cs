using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S2.L3C {
    class Program {
        private const string FilledLine = "=====================================";
        private const string EmptyLine = "=                                   =";

        private static Random random = new Random();

        static void Main(string[] args) {
            bool running = true;
            do {
                ViewMenu();
                ConsoleKey key = Console.ReadKey().Key;
                Console.WriteLine();

                switch (key) {
                    case ConsoleKey.NumPad0:
                    case ConsoleKey.D0:
                        running = false;
                        break;
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        ViewShapeDetail(CreateShape(ShapeType.Rectangle));
                        break;
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        ViewShapeDetail(CreateShape(ShapeType.Circle));
                        break;
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.D3:
                        ViewShapeDetail(CreateShape(ShapeType.Ellipse));
                        break;
                    case ConsoleKey.NumPad4:
                    case ConsoleKey.D4:
                        ViewShapeDetail(CreateShape(ShapeType.Cuboid));
                        break;
                    case ConsoleKey.NumPad5:
                    case ConsoleKey.D5:
                        ViewShapeDetail(CreateShape(ShapeType.Cylinder));
                        break;
                    case ConsoleKey.NumPad6:
                    case ConsoleKey.D6:
                        ViewShapeDetail(CreateShape(ShapeType.Sphere));
                        break;
                    case ConsoleKey.NumPad7:
                    case ConsoleKey.D7:
                        ViewShapes(Randomize2DShapes());
                        break;
                    case ConsoleKey.NumPad8:
                    case ConsoleKey.D8:
                        ViewShapes(Randomize3DShapes());
                        break;
                    default:
                        ViewMenuErrorMessage();
                        break;
                }

                if (running) {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine("   Tryck tangent för att fortsätta   ");
                    Console.ReadKey(true);

                    Console.ResetColor();
                }

            } while (running);
        }

        /// <summary>
        /// Creates a shape of the specified Shape Type
        /// </summary>
        public static Shape CreateShape(ShapeType shapeType) {
            Console.Clear();
            ViewHeader(shapeType.AsText());

            double[] dimensions = ReadDimensions(shapeType);

            Console.WriteLine();

            switch (shapeType) {
                case ShapeType.Circle:
                    return new Ellipse(dimensions[0]);
                case ShapeType.Sphere:
                    return new Sphere(dimensions[0] / 2);
                case ShapeType.Ellipse:
                    return new Ellipse(dimensions[0], dimensions[1]);
                case ShapeType.Rectangle:
                    return new Rectangle(dimensions[0], dimensions[1]);
                case ShapeType.Cuboid:
                    return new Cuboid(dimensions[0], dimensions[1], dimensions[2]);
                case ShapeType.Cylinder:
                    return new Cylinder(dimensions[0] / 2, dimensions[1] / 2, dimensions[2]);
            }
            return null;
        }

        /// <summary>
        /// Returns 5-20 random 2d shapes that have sides of 5-100
        /// </summary>
        public static Shape2D[] Randomize2DShapes() {
            int numberOfShapes = random.Next(5, 21);
            Shape2D[] shapes = new Shape2D[numberOfShapes];

            for (int i = 0; i < numberOfShapes; i++) {
                Shape2D shape;
                switch (random.Next(3)) {
                    case 0:
                        shape = new Ellipse(random.NextDouble(5, 100), random.NextDouble(5, 100));
                        break;
                    case 1:
                        shape = new Ellipse(random.NextDouble(5, 100));
                        break;
                    case 2:
                        shape = new Rectangle(random.NextDouble(5, 100), random.NextDouble(5, 100));
                        break;
                    default:
                        shape = null;
                        break;
                }
                shapes[i] = shape;
            }

            return shapes;
        }

        /// <summary>
        /// Returns 5-20 random 3d shapes that have sides of 5-100
        /// </summary>
        public static Shape3D[] Randomize3DShapes() {
            int numberOfShapes = random.Next(5, 21);
            Shape3D[] shapes = new Shape3D[numberOfShapes];

            for (int i = 0; i < numberOfShapes; i++) {
                Shape3D shape;
                switch (random.Next(3, 6)) {
                    case 3:
                        shape = new Cuboid(random.NextDouble(5, 100), random.NextDouble(5, 100), random.NextDouble(5, 100));
                        break;
                    case 4:
                        shape = new Cylinder(random.NextDouble(5, 50), random.NextDouble(5, 50), random.NextDouble(5, 100));
                        break;
                    case 5:
                        shape = new Sphere(random.NextDouble(5, 50));
                        break;
                    default:
                        shape = null;
                        break;
                }
                shapes[i] = shape;
            }

            return shapes;
        }

        /// <summary>
        /// Asks the user about all dimensions requiered to create the choosen shape.
        /// </summary>
        /// <param name="shapeType">The Shape Type to create</param>
        /// <returns>An array with all dimensions</returns>
        public static double[] ReadDimensions(ShapeType shapeType) {
            switch (shapeType) {
                case ShapeType.Circle:
                case ShapeType.Sphere:
                    return ReadDoublesGreaterThanZero("Ange figurens diameter: ");
                case ShapeType.Ellipse:
                case ShapeType.Rectangle:
                    return ReadDoublesGreaterThanZero("Ange figurens längd och bredd: ", 2);
                case ShapeType.Cuboid:
                case ShapeType.Cylinder:
                    return ReadDoublesGreaterThanZero("Ange figurens längd, bredd och höjd: ", 3);
            }
            return null;
        }

        /// <summary>
        /// Asks the user to input a couple of numbers
        /// </summary>
        /// <param name="prompt">The propmpt to show the user</param>
        /// <param name="numberOfValues">The numbers of values requiered, defaults to 1</param>
        /// <returns>An array with all numbers</returns>
        public static double[] ReadDoublesGreaterThanZero(string prompt, int numberOfValues = 1) {
            double[] doubles = new double[numberOfValues];
            while (true) {
                Console.Write(prompt);
                string line = Console.ReadLine();
                string[] split = line.Split(' ');
                try {
                    if (split.Length != numberOfValues) {
                        ViewErrorMessage("Ange {0} värden sepparerade med mellanslag", numberOfValues);
                        throw new Exception();
                    }
                    for (int i = 0; i < numberOfValues; i++) {
                        try {
                            double value = Double.Parse(split[i]);
                            if (value < 0) {
                                throw new Exception();
                            }
                            doubles[i] = value;
                        } catch (Exception) {
                            ViewErrorMessage("{0} kan inte tolkas som en giltig dimension", split[i]);
                            throw;
                        }
                    }
                    break;
                } catch (Exception) {
                    ViewErrorMessage(" FEL! Ett fel inträffade då figurens dimensioner tolkades. ");
                }
            }
            return doubles;
        }

        /// <summary>
        /// Shows the main menu to the user
        /// </summary>
        public static void ViewMenu() {
            Console.Clear();
            ViewHeader("Geometriska figurer", true);

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();
            Console.WriteLine(" 0. Avsluta.");
            Console.WriteLine(" 1. Rektangel.");
            Console.WriteLine(" 2. Cirkel.");
            Console.WriteLine(" 3. Ellips.");
            Console.WriteLine(" 4. Rätblock.");
            Console.WriteLine(" 5. Cylinder.");
            Console.WriteLine(" 6. Sfär.");
            Console.WriteLine(" 7. Slumpa 2D-figurer.");
            Console.WriteLine(" 8. Slumpa 3D-figurer.");
            Console.WriteLine();
            Console.WriteLine(FilledLine);
            Console.WriteLine();
            Console.Write(" Ange menyval [0-8]: ");

            Console.ResetColor();
        }

        /// <summary>
        /// Shows a header
        /// </summary>
        /// <param name="text">The text in the header</param>
        /// <param name="air">If the text should have air around it, defaults to false</param>
        public static void ViewHeader(string text, bool air = false) {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(FilledLine);
            if (air) {
                Console.WriteLine(EmptyLine);
            }
            Console.WriteLine(text.CenterAlignString(EmptyLine));
            if (air) {
                Console.WriteLine(EmptyLine);
            }
            Console.WriteLine(FilledLine);
            Console.WriteLine();

            Console.ResetColor();
        }

        /// <summary>
        /// Shows a error message to the user
        /// </summary>
        /// <param name="error">The error to show</param>
        /// <param name="obj">objects for String.Format</param>
        public static void ViewErrorMessage(string error, params object[] obj) {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine();
            Console.WriteLine(error, obj);
            Console.WriteLine();

            Console.ResetColor();
        }

        /// <summary>
        /// Shows a error message to the user that an ivalid menu entry were choosen
        /// </summary>
        public static void ViewMenuErrorMessage() {
            ViewErrorMessage(" Fel! Ange ett nummer mellan 0 och 8.");
        }

        /// <summary>
        /// Shows the details of a shape
        /// </summary>
        /// <param name="shape"></param>
        public static void ViewShapeDetail(Shape shape) {
            ViewHeader("Detaljer");
            Console.WriteLine(shape.ToString());
        }

        /// <summary>
        /// Shows a couple of shapes
        /// </summary>
        public static void ViewShapes(Shape[] shapes) {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;

            Array.Sort(shapes);

            if (shapes[0].IsShape3D) {
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("Figur      Längd Bredd  Höjd   Mantelarea Begräns.area        Volym");
                Console.WriteLine("-------------------------------------------------------------------");
            } else {
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Figur      Längd Bredd Omkrets    Area");
                Console.WriteLine("--------------------------------------");
            }

            Console.ResetColor();

            foreach (Shape shape in shapes) {
                Console.WriteLine(shape.ToString("R"));
            }

            Console.WriteLine();
        }
    }
}
