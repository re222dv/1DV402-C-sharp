using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1DV402.S3.L1 {
    public class RecipeRepository {
        private string _path;
    
        public string Path {
            get {
                return _path;
            }
            set {
                if (String.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException();
                }

                _path = value;
            }
        }

        public RecipeRepository(string path) {
            Path = path;
        }
    
        public List<Recipe> Load() {
            RecipeReadStatus status = RecipeReadStatus.Indefinite;
            string[] lines = System.IO.File.ReadAllLines(Path);
            Recipe recipe = null;
            List<Recipe> recepies = new List<Recipe>();

            foreach (string line in lines) {
                if (String.IsNullOrWhiteSpace(line)) {
                    continue;
                } else if (line == "[Recept]") {
                    status = RecipeReadStatus.New;
                    continue;
                } else if (line == "[Ingredienser]") {
                    status = RecipeReadStatus.Ingredient;
                    continue;
                } else if (line == "[Instruktioner]") {
                    status = RecipeReadStatus.Direction;
                    continue;
                }

                try {
                    switch (status) {
                        case RecipeReadStatus.New:
                            recipe = new Recipe(line);
                            recepies.Add(recipe);
                            break;
                        case RecipeReadStatus.Ingredient:
                            Ingredient i = new Ingredient();
                            string[] parts = line.Split(';');

                            if (parts.Length != 3) {
                                throw new Exception("bad file format");
                            }

                            i.Amount = parts[0];
                            i.Measure = parts[1];
                            i.Name = parts[2];

                            recipe.Add(i);
                            break;
                        case RecipeReadStatus.Direction:
                            recipe.Add(line);
                            break;
                        default:
                            throw new Exception("bad file format");
                    }
                } catch (NullReferenceException) {
                    throw new Exception("bad file format");
                }
            }

            recepies.Sort();

            return recepies;
        }

        public void Save(List<Recipe> recipes) {
            List<String> lines = new List<string>();
            foreach (Recipe recipe in recipes) {
                lines.Add("[Recept]");
                lines.Add(recipe.Name);
                lines.Add("[Ingredienser]");
                foreach (Ingredient ingredient in recipe.Ingredients) {
                    lines.Add(String.Format("{0};{1};{2}", ingredient.Amount, ingredient.Measure, ingredient.Name));
                }
                lines.Add("[Instruktioner]");
                foreach (String direction in recipe.Directions) {
                    lines.Add(direction);
                }
            }
            System.IO.File.WriteAllLines(Path, lines);
        }

        private enum RecipeReadStatus {
            Indefinite,
            New,
            Ingredient,
            Direction,
        }
    }
}
