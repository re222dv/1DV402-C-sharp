using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S3.L1 {
    class Program {
        private static void Main(string[] args) {
            bool running = true;
            List<Recipe> recipes = new List<Recipe>();
            do {
                switch (GetMenuChoise()) {
                    case 0:
                        running = false;
                        break;
                    case 1:
                        recipes = LoadRecepies();
                        break;
                    case 2:
                        SaveRecipes(recipes);
                        break;
                    case 3:
                        DeleteRecipe(recipes);
                        break;
                    case 4:
                        ViewRecipe(recipes);
                        break;
                    case 5:
                        ViewRecipe(recipes, true);
                        break;
                }
            } while (running);
        }

        private static void ContinueOnKeyPressed() {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("        Tryck tangent för att fortsätta        ");
            Console.ReadKey(true);

            Console.ResetColor();
        }

        private static Recipe CreateRecipe() {
            throw new System.NotImplementedException();
        }

        private static void DeleteRecipe(List<Recipe> recipes) {
            if (recipes.Count <= 0) {
                Console.Clear();
                RecipeView.RenderHeader("Det finns inga recept att ta bort", Type.warning);
                ContinueOnKeyPressed();
                return;
            }
            do {
                Console.Clear();
                RecipeView.RenderHeader("Välj recept att ta bort");

                Console.WriteLine(" 0. Avsluta.");
                Console.WriteLine();
                Console.WriteLine(" ------------------------------------");
                Console.WriteLine();

                for (int i = 1; i <= recipes.Count; i++) {
                    Console.WriteLine(" {0}. {1}.", i, recipes[i-1].Name);
                }
                Console.WriteLine();
                Console.WriteLine("\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550");
                Console.WriteLine();
                Console.Write("Ange menyval [0-{0}]: ", recipes.Count);

                try {
                    int choise = int.Parse(Console.ReadLine());
                    if (choise == 0) {
                        break;
                    }
                    if (choise > 0 && choise <= recipes.Count) {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("Är du säker på att du vill ta bort '{0}'? [j/N]", recipes[choise - 1].Name);
                        Console.ResetColor();

                        switch (Console.ReadLine().ToLower()) {
                            case "j":
                            case "ja":
                            case "y":
                            case "yes":
                                recipes.RemoveAt(choise - 1);
                                RecipeView.RenderHeader("Receptet har tagits bort", Type.good);
                                break;
                            default:
                                RecipeView.RenderHeader("Receptet kommer inte att tas bort", Type.good);
                                break;
                        }
                    } else {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine();
                        Console.WriteLine("Felaktigt menyval");
                        Console.WriteLine();
                        Console.ResetColor();
                    }
                } catch {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine();
                    Console.WriteLine("Felaktigt menyval");
                    Console.WriteLine();
                    Console.ResetColor();
                }

                ContinueOnKeyPressed();
            } while (true);
        }

        private static int GetMenuChoise() {
            do {
                Console.Clear();

                RecipeView.RenderHeader("       Receptsamling med fil       ");

                Console.WriteLine(" - Arkiv --------------------------------");
                Console.WriteLine();
                Console.WriteLine(" 0. Avsluta.");
                Console.WriteLine(" 1. Öppna textfil med recept.");
                Console.WriteLine(" 2. Spara recept på textfil.");
                Console.WriteLine();
                Console.WriteLine(" - Redigera -----------------------------");
                Console.WriteLine();
                Console.WriteLine(" 3. Ta bort recept.");
                Console.WriteLine();
                Console.WriteLine(" - Visa ---------------------------------");
                Console.WriteLine();
                Console.WriteLine(" 4. Visa recept.");
                Console.WriteLine(" 5. Visa alla recept.");
                Console.WriteLine();
                Console.WriteLine(" - Extrauppgifter -----------------------");
                Console.WriteLine();
                Console.WriteLine(" 6. Lägg till nytt recept.");
                Console.WriteLine(" 7. Hitta recept med ingredienser.");
                Console.WriteLine(" 8. Ändra recept.");
                Console.WriteLine();
                Console.WriteLine("\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550");
                Console.WriteLine();
                Console.Write("Ange menyval [0-8]: ");

                try {
                    int choise = int.Parse(Console.ReadLine());
                    if (choise >= 0 && choise <= 8) {
                        return choise;
                    }
                } catch {}

                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine();
                Console.WriteLine("Felaktigt menyval");
                Console.WriteLine();
                Console.ResetColor();

                ContinueOnKeyPressed();
            } while (true);
        }

        private static Recipe GetRecipe(string header, List<Recipe> recipes) {
            throw new System.NotImplementedException();
        }

        private static List<Recipe> LoadRecepies() {
            List<Recipe> recepies;
            Console.Clear();
            Console.Write("Välj filnamn [recipes.txt]: ");
            string name = Console.ReadLine();
            if (name == "") {
                name = "recipes.txt";
            }
            try {
                RecipeRepository rp = new RecipeRepository(name);
                recepies = rp.Load();
                RecipeView.RenderHeader("Recepten har lästs in", Type.good);
            } catch {
                RecipeView.RenderHeader("FEL! Ett fel inträffade då recepten lästes in.", Type.bad);
                recepies = new List<Recipe>();
            }
            ContinueOnKeyPressed();
            return recepies;
        }

        private static List<string> ReadDirections() {
            throw new System.NotImplementedException();
        }

        private static List<Ingredient> ReadIngredients() {
            throw new System.NotImplementedException();
        }

        private static string ReadRecipeName() {
            throw new System.NotImplementedException();
        }

        private static void SaveRecipes(List<Recipe> recipes) {
            Console.Clear();
            if (recipes.Count > 0) {
                bool repeat = false;
                string name;
                do {
                    Console.Write("Välj filnamn [recipes.txt]: ");
                    name = Console.ReadLine();
                    if (name == "") {
                        name = "recipes.txt";
                    }
                    if (System.IO.File.Exists(name)) {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("Är du säker på att du vill skriva över '{0}'? [j/N]", name);
                        Console.ResetColor();

                        switch (Console.ReadLine().ToLower()) {
                            case "j":
                            case "ja":
                            case "y":
                            case "yes":
                                repeat = false;
                                break;
                            default:
                                repeat = true;
                                break;
                        }
                    } else {
                        repeat = false;
                    }
                } while(repeat);
                try {
                    RecipeRepository rp = new RecipeRepository(name);
                    rp.Save(recipes);
                    RecipeView.RenderHeader("Recepten har sparats", Type.good);
                } catch {
                    RecipeView.RenderHeader("FEL! Ett fel inträffade då recepten skulle sparas.", Type.bad);
                }
            } else {
                RecipeView.RenderHeader("Det finns inga recept att spara", Type.warning);
            }
            ContinueOnKeyPressed();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recipes"></param>
        /// <param name="viewAll"></param>
        private static void ViewRecipe(List<Recipe> recipes, bool viewAll = false) {
            if (recipes.Count <= 0) {
                Console.Clear();
                RecipeView.RenderHeader("Det finns inga recept att visa", Type.warning);
                ContinueOnKeyPressed();
                return;
            }
            if (viewAll) {
                Console.Clear();
                recipes.Sort();
                RecipeView.Render(recipes);
                ContinueOnKeyPressed();
            } else {
                do {
                    Console.Clear();
                    RecipeView.RenderHeader("Välj recept att visa");

                    Console.WriteLine(" 0. Avsluta.");
                    Console.WriteLine();
                    Console.WriteLine(" ------------------------------------");
                    Console.WriteLine();

                    for (int i = 1; i <= recipes.Count; i++) {
                        Console.WriteLine(" {0}. {1}.", i, recipes[i - 1].Name);
                    }
                    Console.WriteLine();
                    Console.WriteLine("\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550");
                    Console.WriteLine();
                    Console.Write("Ange menyval [0-{0}]: ", recipes.Count);

                    try {
                        int choise = int.Parse(Console.ReadLine());
                        if (choise == 0) {
                            break;
                        }
                        if (choise > 0 && choise <= recipes.Count) {
                            Console.Clear();
                            RecipeView.Render(recipes[choise - 1]);
                        } else {
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine();
                            Console.WriteLine("Felaktigt menyval");
                            Console.WriteLine();
                            Console.ResetColor();
                        }
                    } catch {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine();
                        Console.WriteLine("Felaktigt menyval");
                        Console.WriteLine();
                        Console.ResetColor();
                    }

                    ContinueOnKeyPressed();
                } while (true);
            }
        }
    }
}
