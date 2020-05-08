using System.Collections.Generic;
using NUnit.Framework;

namespace PizzaGame.Orders.Test
{
    public class OrderEvaulationTest
    {
        class TestPizza: IPizza
        {
            public Dictionary<Ingredient, int> Toppings { get; set; }
            public CookingLevel CookingLevel { get; set; }
        }

        [Test]
        public void TestSimple()
        {
            /*
            var order = new Order(new OrderValue(10, 10));
            order.Toppings.Add(Ingredient.Olive);

            TestPizza pizza = new TestPizza()
            {
                Toppings = new HashSet<Ingredient>()
                {
                    Ingredient.Olive
                },
                CookingLevel = PizzaGame.Orders.CookingLevel.Cooked
            };

            OrderValue evaluatedValue = order.Evaluate(pizza);

            Assert.AreEqual(10, evaluatedValue.Dollars);
            Assert.AreEqual(10, evaluatedValue.Cents);
            */
        }
    }
}