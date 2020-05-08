using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PizzaGame.Orders
{
    /// <summary>
    /// A type of ingredient
    /// </summary>
    [Serializable]
    public enum Ingredient
    {
        Mushroom,
        Olive,
        Pepperoni,
        Sausage
    }

    /// <summary>
    /// Helper functions for ingredients
    /// </summary>
    public static class Ingredients
    {
        /// <summary>
        /// Get a list of ingredients
        /// </summary>
        /// <returns>an enumerable list of ingredients</returns>
        public static IEnumerable<Ingredient> GetIngredients()
        {
            return Enum.GetValues(typeof(Ingredient)).Cast<Ingredient>();
        }

        /// <summary>
        /// Returns the hotkeys of ingredients
        /// </summary>
        /// <returns></returns>
        public static Dictionary<KeyCode, Ingredient> MapKeysToIngredients()
        {
            Dictionary<KeyCode, Ingredient> mapping = new Dictionary<KeyCode, Ingredient>();

            mapping[KeyCode.P] = Ingredient.Pepperoni;
            mapping[KeyCode.O] = Ingredient.Olive;
            mapping[KeyCode.M] = Ingredient.Mushroom;
            mapping[KeyCode.N] = Ingredient.Sausage;

            return mapping;
        }
    }
}
