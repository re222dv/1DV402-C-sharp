using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace _1DV402.S3.L1 {
    /// <summary>
    /// Represents a recipe
    /// </summary>
    public class Recipe : IComparable, IComparable<Recipe> {
        private string _name;
        private List<Ingredient> _ingredients;
        private List<string> _directions;

        /// <summary>
        /// Returns the ingredients
        /// </summary>
        public ReadOnlyCollection<Ingredient> Ingredients {
            get {
                return new ReadOnlyCollection<Ingredient>(_ingredients);
            }
            set {
                _ingredients = new List<Ingredient>(value);
            }
        }

        /// <summary>
        /// Returns the directions
        /// </summary>
        public ReadOnlyCollection<string> Directions {
            get {
                return new ReadOnlyCollection<string>(_directions);
            }
            set {
                _directions = new List<string>(value);
            }
        }

        /// <summary>
        /// Holds the name of the recipe
        /// </summary>
        public string Name {
            get {
                return _name;
            }
            set {
                if (String.IsNullOrEmpty(value)) {
                    throw new ArgumentException();
                }
                _name = value;
            }
        }

        public Recipe(string name) {
            Name = name;
            _ingredients = new List<Ingredient>();
            _directions = new List<string>();
        }

        public Recipe(string name, IList<Ingredient> ingredients, IList<string> direction) {
            Name = name;
            _ingredients = new List<Ingredient>(ingredients);
            _directions = new List<string>(direction);
        }

        /// <summary>
        /// Add an ingredient
        /// </summary>
        public void Add(Ingredient ingredient) {
            _ingredients.Add(ingredient);
        }

        /// <summary>
        /// Add a direction
        /// </summary>
        public void Add(string direction) {
            _directions.Add(direction);
        }

        public int CompareTo(object obj) {
            if (obj == null) {
                return 1;
            }

            if (!(obj is Recipe)) {
                throw new ArgumentException();
            }

            return Name.CompareTo(((Recipe) obj).Name);
        }

        public int CompareTo(Recipe other) {
            return CompareTo((object) other);
        }
    }
}
