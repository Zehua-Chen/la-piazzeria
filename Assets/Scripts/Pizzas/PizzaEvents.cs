using System;
using UnityEngine.Events;
using PizzaGame.Orders;

namespace PizzaGame.Pizzas
{
    /// Events whose first parameter is sauce
    [Serializable]
    public class SauceEvent : UnityEvent<string>
    {
    }

    /// <summary>
    /// Event whose first parmater is topping
    /// </summary>
    [Serializable]
    public class ToppingEvent : UnityEvent<Ingredient>
    {
    }

    /// <summary>
    /// Event whose first parameter is pizza
    /// </summary>
    [Serializable]
    public class PizzaEvent : UnityEvent<Pizza>
    {
    }

    /// <summary>
    /// Event whose first parameter is of type <c>IPizza</c>
    /// </summary>
    [Serializable]
    public class PizzaContentEvent : UnityEvent<IPizza>
    {
    }
}
