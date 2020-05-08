using System;
using UnityEngine.Events;

namespace PizzaGame.Orders
{
    /// <summary>
    /// An event whose first parameter is an order
    /// </summary>
    [Serializable]
    public class OrderEvent : UnityEvent<Order>
    {
    }
}