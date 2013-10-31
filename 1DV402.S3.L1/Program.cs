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
                        recipes.AddRange(LoadRecepies());
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
                    case 6:
                        Recipe recipe = CreateRecipe();
                        if (recipe != null) {
                            recipes.Add(recipe);
                        }
                        break;
                    case 7:
                        FindRecipe(recipes);
                        break;
                    case 8:
                        ModifyRecipe(recipes);
                        break;
                }
            } while (running);
        }

        /// <summary>
        /// Pauses untill any key have been pressed
        /// </summary>
        private static void ContinueOnKeyPressed() {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("        Tryck tangent för att fortsätta        ");
            Console.ReadKey(true);

            Console.ResetColor();
        }

        /// <summary>
        /// Creates a new recipe
        /// </summary>
        /// <returns>The recipe or null if aborted</returns>
        private static Recipe CreateRecipe() {
            Console.Clear();
            RecipeView.RenderHeader("          Nytt recept          ");
            string name = ReadRecipeName();
            if (name == null) {
                return null;
            }

            Recipe recipe = new Recipe(name);

            List<Ingredient> ingredients = ReadIngredients();
            if (ingredients == null) {
                return null;
            }
            foreach (Ingredient ingredient in ingredients) {
                recipe.Add(ingredient);
            }

            List<string> directions = ReadDirections();
            if (directions == null) {
                return null;
            }
            foreach (string direction in directions) {
                recipe.Add(direction);
            }

            return recipe;
        }

        /// <summary>
        /// Reads the name for a recipe
        /// </summary>
        /// <returns>The name or null if aborted</returns>
        private static string ReadRecipeName() {
            do {
                Console.Write("Ange receptets namn: ");
                string name = Console.ReadLine();
                if (String.IsNullOrEmpty(name)) {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine(" FEL! Du måste ange receptets namn.");
                    Console.WriteLine();

                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.WriteLine("  Tryck tangent för att fortsätta - [Esc] avbryter   ");
                    Console.ResetColor();

                    if (Console.ReadKey(true).Key == ConsoleKey.Escape) {
                        return null;
                    }

                    continue;
                }

                return name;
            } while(true);
        }

        /// <summary>
        /// Reads the ingredients for a recipe
        /// </summary>
        /// <param name="ingredients">A default set of ingredients, defaults to null</param>
        /// <returns>The ingredients or null if aborted</returns>
        private static List<Ingredient> ReadIngredients(List<Ingredient> ingredients = null) {
            if (ingredients == null) {
                ingredients = new List<Ingredient>();
            }
            Console.WriteLine("Ange receptets ingredienser - tom rad för namnet avslutar: ");

            do {
                Ingredient ingredient = new Ingredient();
                Console.WriteLine("{0}.", ingredients.Count+1);
                
                Console.Write("Namn:");
                string name = Console.ReadLine();
                if (String.IsNullOrEmpty(name)) {

                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Är du säker på att du inte vill lägga till fler ingredienser [j/N]");
                    Console.ResetColor();

                    switch (Console.ReadLine().ToLower()) {
                        case "j":
                        case "ja":
                        case "y":
                        case "yes":
                            if (ingredients.Count < 1) {
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.ForegroundColor = ConsoleColor.White;

                                Console.WriteLine(" FEL! Du måste ange minst en ingrediens.");
                                Console.WriteLine();

                                Console.BackgroundColor = ConsoleColor.Blue;
                                Console.WriteLine("  Tryck tangent för att fortsätta ange ingredienser - [Esc] avbryter   ");
                                Console.ResetColor();

                                if (Console.ReadKey(true).Key == ConsoleKey.Escape) {
                                    return null;
                                }

                                continue;
                            }
                            return ingredients;
                        default:
                            continue;
                    }
                }
                ingredient.Name = name;

                Console.Write("Mängd:");
                ingredient.Amount = Console.ReadLine();

                Console.Write("Mått:");
                ingredient.Measure = Console.ReadLine();

                ingredients.Add(ingredient);
            } while (true);
        }


        /// <summary>
        /// Reads the directions for a recipe
        /// </summary>
        /// <returns>The directions or null if aborted</returns>
        private static List<string> ReadDirections(List<string> directions = null) {
            if (directions == null) {
                directions = new List<string>();
            }
            Console.WriteLine("Ange receptets instruktioner - tom rad avslutar: ");

            do {
                string direction;
                Console.WriteLine("<{0}>", directions.Count + 1);

                string name = Console.ReadLine();
                if (String.IsNullOrEmpty(name)) {

                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Är du säker på att du inte vill lägga till fler instruktioner [j/N]");
                    Console.ResetColor();

                    switch (Console.ReadLine().ToLower()) {
                        case "j":
                        case "ja":
                        case "y":
                        case "yes":
                            if (directions.Count < 1) {
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.ForegroundColor = ConsoleColor.White;

                                Console.WriteLine(" FEL! Du måste ange minst en instruktion.");
                                Console.WriteLine();

                                Console.BackgroundColor = ConsoleColor.Blue;
                                Console.WriteLine("  Tryck tangent för att fortsätta ange instruktioner - [Esc] avbryter   ");
                                Console.ResetColor();

                                if (Console.ReadKey(true).Key == ConsoleKey.Escape) {
                                    return null;
                                }

                                continue;
                            }
                            return directions;
                        default:
                            continue;
                    }
                }
                direction = name;

                directions.Add(direction);
            } while (true);
        }


        /// <summary>
        /// Lets the user search for recipes by ingredients
        /// </summary>
        private static void FindRecipe(List<Recipe> recipes) {
            if (recipes.Count <= 0) {
                Console.Clear();
                RecipeView.RenderHeader("Det finns inga recept att söka bland", Type.warning);
                ContinueOnKeyPressed();
                return;
            }

            do {
                Console.Clear();
                RecipeView.RenderHeader("          Sök recept          ");

                Console.WriteLine("Ange ingredienser att söka efter, sepparera med mellanslag: ");
                string line = Console.ReadLine().ToLower();
                if (String.IsNullOrEmpty(line)) {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine(" FEL! Du angav inga ingredienser.");
                    Console.WriteLine();

                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.WriteLine("  Tryck tangent för att fortsätta - [Esc] avbryter   ");
                    Console.ResetColor();

                    if (Console.ReadKey(true).Key == ConsoleKey.Escape) {
                        return;
                    }

                    continue;
                }

                List<Recipe> matches = new List<Recipe>();
                string[] ingredients = line.Split(' ');

                foreach (Recipe recipe in recipes) {
                    bool matchAll = false;
                    foreach (string ingredient in ingredients) {
                        bool match = false;
                        foreach (Ingredient i in recipe.Ingredients) {
                            if (i.Name.ToLower() == ingredient) {
                                match = true;
                                break;
                            }
                        }
                        // if match isn't true atleast one ingredient doesn't match so we break after matchAll have been set false
                        matchAll = match;
                        if (!match) {
                            break;
                        }
                    }
                    if (matchAll) {
                        matches.Add(recipe);
                    }
                }

                if (matches.Count < 1) {
                    Console.WriteLine("Inga recept hittades");
                } else {
                    ViewRecipe(matches);
                }

                ContinueOnKeyPressed();

            } while (true);
        }

        /// <summary>
        /// Lets the user deletes a recipe
        /// </summary>
        private static void DeleteRecipe(List<Recipe> recipes) {
            if (recipes.Count <= 0) {
                Console.Clear();
                RecipeView.RenderHeader("Det finns inga recept att ta bort", Type.warning);
                ContinueOnKeyPressed();
                return;
            }
            do {
                Recipe recipe = GetRecipe("Välj recept att ta bort", recipes);
                if (recipe == null) {
                    return;
                }
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Är du säker på att du vill ta bort '{0}'? [j/N]", recipe.Name);
                Console.ResetColor();
                switch (Console.ReadLine().ToLower()) {
                    case "j":
                    case "ja":
                    case "y":
                    case "yes":
                        recipes.Remove(recipe);
                        RecipeView.RenderHeader("Receptet har tagits bort", Type.good);
                        break;
                    default:
                        RecipeView.RenderHeader("Receptet kommer inte att tas bort", Type.good);
                        break;
                }
            } while (true);
        }

        /// <summary>
        /// Lets the user modify a recipe
        /// </summary>
        private static void ModifyRecipe(List<Recipe> recipes) {
            if (recipes.Count <= 0) {
                Console.Clear();
                RecipeView.RenderHeader("Det finns inga recept att ändra", Type.warning);
                ContinueOnKeyPressed();
                return;
            }
            do {
                Recipe recipe = GetRecipe("Välj recept att ändra", recipes);
                if (recipe == null) {
                    return;
                }

                bool modifying = true;
                do {
                    Console.Clear();
                    RecipeView.RenderHeader(recipe.Name);
                    Console.WriteLine(" 0. Avsluta.");
                    Console.WriteLine();
                    Console.WriteLine(" ------------------------------------");
                    Console.WriteLine();
                    Console.WriteLine(" 1. Visa receptet.");
                    Console.WriteLine();
                    Console.WriteLine(" - Ingredienser ----------------------");
                    Console.WriteLine();
                    Console.WriteLine(" 2. Lägg till ingredienser.");
                    Console.WriteLine(" 3. Ta bort ingredienser.");
                    Console.WriteLine(" 4. Byt ordning på ingredienser.");
                    Console.WriteLine();
                    Console.WriteLine(" - Instruktioner ---------------------");
                    Console.WriteLine();
                    Console.WriteLine(" 5. Lägg till instruktioner.");
                    Console.WriteLine(" 6. Ta bort instruktioner.");
                    Console.WriteLine(" 7. Byt ordning på instruktioner.");
                    Console.WriteLine();
                    Console.WriteLine("\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550");
                    Console.WriteLine();
                    Console.Write("Ange menyval [0-7]: ");

                    try {
                        int choise = int.Parse(Console.ReadLine());
                        switch (choise) {
                            case 0:
                                modifying = false;
                                break;
                            case 1:
                                RecipeView.Render(recipe);
                                break;
                            case 2:
                                recipe.Ingredients = ReadIngredients(new List<Ingredient>(recipe.Ingredients)).AsReadOnly();
                                break;
                            case 3:
                                recipe.Ingredients = RemoveIngredient(new List<Ingredient>(recipe.Ingredients)).AsReadOnly();
                                break;
                            case 4:
                                recipe.Ingredients = MoveIngredient(new List<Ingredient>(recipe.Ingredients)).AsReadOnly();
                                break;
                            case 5:
                                recipe.Directions = ReadDirections(new List<string>(recipe.Directions)).AsReadOnly();
                                break;
                            case 6:
                                recipe.Directions = RemoveDirection(new List<string>(recipe.Directions)).AsReadOnly();
                                break;
                            case 7:
                                recipe.Directions = MoveDirection(new List<string>(recipe.Directions)).AsReadOnly();
                                break;
                            default:
                                Console.BackgroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine();
                                Console.WriteLine("Felaktigt menyval");
                                Console.WriteLine();
                                Console.ResetColor();
                                break;
                        }
                    } catch {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine();
                        Console.WriteLine("Felaktigt menyval");
                        Console.WriteLine();
                        Console.ResetColor();
                    }

                    if (modifying) {
                        ContinueOnKeyPressed();
                    }
                } while (modifying);
            } while (true);
        }

        /// <summary>
        /// Lets the user remove an ingredient
        /// </summary>
        private static List<Ingredient> RemoveIngredient(List<Ingredient> ingredients) {
            if (ingredients.Count <= 0) {
                Console.Clear();
                RecipeView.RenderHeader("Det finns inga ingredienser att ta bort", Type.warning);
                ContinueOnKeyPressed();
                return ingredients;
            }
            do {
                Console.Clear();
                RecipeView.RenderHeader("Välj ingrediens att ta bort");
                Console.WriteLine(" 0. Avsluta.");
                Console.WriteLine();
                Console.WriteLine(" ------------------------------------");
                Console.WriteLine();
                for (int i = 0; i < ingredients.Count; i++) {
                    Ingredient ingredient = ingredients[i];
                    Console.WriteLine(" {0}. {1}.", i + 1, ingredient.Name);
                }
                Console.WriteLine();
                Console.WriteLine("\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550");
                Console.WriteLine();
                Console.Write("Ange menyval [0-{0}]: ", ingredients.Count);

                try {
                    int choise = int.Parse(Console.ReadLine());
                    if (choise == 0) {
                        break;
                    }
                    if (choise > 0 && choise <= ingredients.Count) {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("Är du säker på att du vill ta bort '{0}'? [j/N]", ingredients[choise- 1].Name);
                        Console.ResetColor();
                        switch (Console.ReadLine().ToLower()) {
                            case "j":
                            case "ja":
                            case "y":
                            case "yes":
                                ingredients.RemoveAt(choise - 1);
                                RecipeView.RenderHeader("Ingrediensen har tagits bort", Type.good);
                                break;
                            default:
                                RecipeView.RenderHeader("Ingrediensen kommer inte att tas bort", Type.good);
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
            return ingredients;
        }

        /// <summary>
        /// Lets the user remove a direction
        /// </summary>
        private static List<string> RemoveDirection(List<string> directions) {
            if (directions.Count <= 0) {
                Console.Clear();
                RecipeView.RenderHeader("Det finns inga instruktioner att ta bort", Type.warning);
                ContinueOnKeyPressed();
                return directions;
            }
            do {
                Console.Clear();
                RecipeView.RenderHeader("Välj instruktion att ta bort");
                Console.WriteLine(" 0. Avsluta.");
                Console.WriteLine();
                Console.WriteLine(" ------------------------------------");
                Console.WriteLine();
                for (int i = 0; i < directions.Count; i++) {
                    string direction = directions[i];
                    Console.WriteLine(" {0}. {1}.", i + 1, direction);
                }
                Console.WriteLine();
                Console.WriteLine("\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550");
                Console.WriteLine();
                Console.Write("Ange menyval [0-{0}]: ", directions.Count);

                try {
                    int choise = int.Parse(Console.ReadLine());
                    if (choise == 0) {
                        break;
                    }
                    if (choise > 0 && choise <= directions.Count) {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("Är du säker på att du vill ta bort '{0}'? [j/N]", directions[choise-1]);
                        Console.ResetColor();
                        switch (Console.ReadLine().ToLower()) {
                            case "j":
                            case "ja":
                            case "y":
                            case "yes":
                                directions.RemoveAt(choise - 1);
                                RecipeView.RenderHeader("Instruktionen har tagits bort", Type.good);
                                break;
                            default:
                                RecipeView.RenderHeader("Instruktionen kommer inte att tas bort", Type.good);
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
            return directions;
        }

        /// <summary>
        /// Lets the user move an ingredient
        /// </summary>
        private static List<Ingredient> MoveIngredient(List<Ingredient> ingredients) {
            if (ingredients.Count <= 0) {
                Console.Clear();
                RecipeView.RenderHeader("Det finns inga ingredienser att ta bort", Type.warning);
                ContinueOnKeyPressed();
                return ingredients;
            }
            do {
                Console.Clear();
                RecipeView.RenderHeader("Välj ingrediens att ta bort");
                Console.WriteLine(" 0. Avsluta.");
                Console.WriteLine();
                Console.WriteLine(" ------------------------------------");
                Console.WriteLine();
                for (int i = 0; i < ingredients.Count; i++) {
                    Ingredient ingredient = ingredients[i];
                    Console.WriteLine(" {0}. {1}.", i + 1, ingredient.Name);
                }
                Console.WriteLine();
                Console.WriteLine("\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550");
                Console.WriteLine();
                Console.Write("Ange menyval [0-{0}]: ", ingredients.Count);

                try {
                    int choise = int.Parse(Console.ReadLine());
                    if (choise == 0) {
                        break;
                    }
                    if (choise > 0 && choise <= ingredients.Count) {
                        bool moving = true;
                        do {
                            Console.WriteLine();
                            Console.WriteLine(" 0. Avsluta.");
                            if (choise != 1) {
                                Console.WriteLine(" 1. Flytta upp.");
                            }
                            if (choise != ingredients.Count) {
                                Console.WriteLine(" 2. Flytta ner.");
                                Console.Write("Ange menyval [0-2]: ");
                            } else {
                                Console.Write("Ange menyval [0-1]: ");
                            }
                            string subChoise = Console.ReadLine();
                            if (subChoise == "0") {
                                moving = false;
                                break;
                            } else if (subChoise == "1" && choise != 1) {
                                moving = false;
                                ingredients.Insert(choise - 2, ingredients[choise - 1]);
                                ingredients.RemoveAt(choise);
                                break;
                            } else if (subChoise == "2" && choise != ingredients.Count) {
                                moving = false;
                                ingredients.Insert(choise + 1, ingredients[choise - 1]);
                                ingredients.RemoveAt(choise - 1);
                                break;
                            }
                        } while (moving);
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
            return ingredients;
        }

        /// <summary>
        /// Lets the user move a direction
        /// </summary>
        private static List<string> MoveDirection(List<string> directions) {
            if (directions.Count <= 0) {
                Console.Clear();
                RecipeView.RenderHeader("Det finns inga instruktioner att ta bort", Type.warning);
                ContinueOnKeyPressed();
                return directions;
            }
            do {
                Console.Clear();
                RecipeView.RenderHeader("Välj instruktion att flytta");
                Console.WriteLine(" 0. Avsluta.");
                Console.WriteLine();
                Console.WriteLine(" ------------------------------------");
                Console.WriteLine();
                for (int i = 0; i < directions.Count; i++) {
                    string direction = directions[i];
                    Console.WriteLine(" {0}. {1}.", i + 1, direction);
                }
                Console.WriteLine();
                Console.WriteLine("\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550\u2550");
                Console.WriteLine();
                Console.Write("Ange menyval [0-{0}]: ", directions.Count);

                try {
                    int choise = int.Parse(Console.ReadLine());
                    if (choise == 0) {
                        break;
                    }
                    if (choise > 0 && choise <= directions.Count) {
                        bool moving = true;
                        do {
                            Console.WriteLine();
                            Console.WriteLine(" 0. Avsluta.");
                            if  (choise != 1) {
                                Console.WriteLine(" 1. Flytta upp.");
                            }
                            if  (choise != directions.Count) {
                                Console.WriteLine(" 2. Flytta ner.");
                                Console.Write("Ange menyval [0-2]: ");
                            } else {
                                Console.Write("Ange menyval [0-1]: ");
                            }
                            string subChoise = Console.ReadLine();
                            if (subChoise == "0") {
                                moving = false;
                                break;
                            } else if (subChoise == "1" && choise != 1) {
                                moving = false;
                                directions.Insert(choise - 2, directions[choise - 1]);
                                directions.RemoveAt(choise);
                                break;
                            } else if (subChoise == "2" && choise != directions.Count) {
                                moving = false;
                                directions.Insert(choise+1, directions[choise - 1]);
                                directions.RemoveAt(choise-1);
                                break;
                            }
                        } while (moving);
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
            return directions;
        }

        /// <summary>
        /// Handles the main menu
        /// </summary>
        /// <returns>The choosen option</returns>
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

        /// <summary>
        /// Asks the user to pick a recipe
        /// </summary>
        /// <param name="header">Text in header</param>
        private static Recipe GetRecipe(string header, List<Recipe> recipes) {
            do {
                Console.Clear();
                RecipeView.RenderHeader(header);

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
                        return null;
                    }
                    if (choise > 0 && choise <= recipes.Count) {
                        Console.Clear();
                        return recipes[choise - 1];
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
            } while (true);
        }

        /// <summary>
        /// Load recipes from a file choosen by the user
        /// </summary>
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

        /// <summary>
        /// Save the recipes to a file choosen by the user
        /// </summary>
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
        /// Views a single or all recipes.
        /// If only a single recipe should be viewed it asks the usere wich one
        /// </summary>
        /// <param name="viewAll">defaults to false</param>
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
                    Recipe recipe = GetRecipe("Välj recept att visa", recipes);
                    if (recipe == null) {
                        return;
                    }
                    RecipeView.Render(recipe);
                    ContinueOnKeyPressed();
                } while (true);
            }
        }
    }
}
