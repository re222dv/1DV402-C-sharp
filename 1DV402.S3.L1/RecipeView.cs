using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV402.S3.L1 {
    public class RecipeView {
        public static void Render(Recipe recipe) {
            RenderHeader(recipe.Name);
            Console.WriteLine("Ingredienser");
            Console.WriteLine("============");

            foreach (Ingredient ingredient in recipe.Ingredients) {
                Console.WriteLine(ingredient.ToString());
            }

            Console.WriteLine();
            Console.WriteLine("Gör så här");
            Console.WriteLine("==========");

            for (int i = 1; i <= recipe.Directions.Count; i++) {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("({0})", i);
                Console.ResetColor();
                Console.WriteLine(recipe.Directions[i-1]);
                Console.WriteLine();
            }
        }

        public static void Render(IList<Recipe> recipes) {
            foreach (Recipe recipe in recipes) {
                Render(recipe);
            }
        }

        public static void RenderHeader(string header, Type type = Type.normal) {
            switch (type) {
                case Type.bad:
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case Type.good:
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case Type.warning:
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                default:
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            Console.Write(" \u2554\u2550");
            for (int i = 0; i < header.Length; i++) {
                Console.Write("\u2550");
            }
            Console.WriteLine("\u2550\u2557 ");

            Console.Write(" \u2551 ");
            Console.Write(header);
            Console.WriteLine(" \u2551 ");

            Console.Write(" \u255A\u2550");
            for (int i = 0; i < header.Length; i++) {
                Console.Write("\u2550");
            }
            Console.WriteLine("\u2550\u255D ");
            Console.WriteLine();

            Console.ResetColor();
        }
    }

    public enum Type {
        normal,
        good,
        bad,
        warning
    }
}
