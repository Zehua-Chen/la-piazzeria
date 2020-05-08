using System.Collections.Generic;

namespace PizzaGame.Orders
{
    /// <summary>
    /// Cooking level of pizza
    /// </summary>
    public enum CookingLevel
    {
        Uncooked,
        Cooked,
        Burnt
    }

    /// <summary>
    /// Contracts of pizzas
    /// </summary>
    public interface IPizza
    {
        Dictionary<Ingredient, int> Toppings { get; }
        CookingLevel CookingLevel { get; }
    }
}