using System;
using UnityEngine.Events;

namespace PizzaGame.Orders
{
    /// <summary>
    /// Event whose first parameter is of type ingredient
    /// </summary>
    [Serializable]
    public class IngredientEvent : UnityEvent<Ingredient>
    {
    }
}