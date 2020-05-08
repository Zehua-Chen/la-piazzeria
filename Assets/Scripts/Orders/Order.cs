using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PizzaGame.Orders
{
    /// <summary>
    /// Orders
    /// </summary>
    public class Order
    {
        private const int MIN_INGREIDENTS = 0;
        private const int MAX_INGREDIENTS = 10;

        private const int BASE_DOLLAR_VALUE = 3;
        private const int BONUS_CENTS_PER_INGREDIENT = 15;

        private const float MISSING_INGREDIENT_PENALTY = 0.15f;
        private const float BURNT_PIZZA_PENALTY = 0.25f;
        private const float UNDERCOOKED_PIZZA_PENALTY = 0.25f;

        /// <summary>
        /// Toppings
        /// </summary>
        public Dictionary<Ingredient, int> Toppings { get; private set; } = new Dictionary<Ingredient, int>();

        /// <summary>
        /// Name of the order
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Tips of the order
        /// </summary>
        public OrderValue Value { get; set; }

        /// <summary>
        /// Number of ingredients
        /// </summary>
        public int NumIngredients { get; private set; }

        /// <summary>
        /// Construct an empty order
        /// </summary>
        public Order()
        {
            CreateToppings();

            this.Value = GenerateOrderValueFromToppings();
        }

        /// <summary>
        /// Construct an emtpy order with a specified value
        /// </summary>
        /// <param name="value">the value of the order</param>
        public Order(OrderValue value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Construct an order of specified toppings
        /// </summary>
        /// <param name="toppings">toppings of the order</param>
        public Order(Dictionary<Ingredient, int> toppings)
        {
            this.Toppings = toppings;
            this.Value = GenerateOrderValueFromToppings();
        }

        /// <summary>
        /// Get the order value from toppings
        /// </summary>
        /// <returns>The value associate with values</returns>
        private OrderValue GenerateOrderValueFromToppings()
        {
            int dollars = BASE_DOLLAR_VALUE;

            OrderValue baseValue = new OrderValue(dollars, 0);

            int ingredientBonus = BONUS_CENTS_PER_INGREDIENT * this.NumIngredients;
            int randomBonus = 5 * UnityEngine.Random.Range(0, 20);

            OrderValue bonusValue = new OrderValue(ingredientBonus + randomBonus);

            return baseValue + bonusValue;
        }

        /// <summary>
        /// Create random toppings
        /// </summary>
        private void CreateToppings()
        {
            int numIngredients = UnityEngine.Random.Range(MIN_INGREIDENTS, MAX_INGREDIENTS + 1);

            this.NumIngredients = numIngredients;

            List<Ingredient> allIngredients = Ingredients.GetIngredients().ToList();

            foreach (Ingredient ingredient in allIngredients)
            {
                this.Toppings[ingredient] = 0;
            }

            System.Random rand = new System.Random();

            for (int idx = 0; idx < numIngredients; idx++)
            {
                int randomIngredientIndex = rand.Next(allIngredients.Count);

                Ingredient randomIngredient = allIngredients[randomIngredientIndex];

                this.Toppings[randomIngredient]++;
            }
        }

        /// <summary>
        /// Output as string
        /// </summary>
        /// <returns>string representation</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (NumIngredients == 0)
            {
                stringBuilder.Append("Plain Pizza");
            }
            else
            {
                stringBuilder.Append("Toppings:\n");

                foreach (Ingredient ingredient in Toppings.Keys)
                {
                    if (Toppings[ingredient] > 0)
                    {
                        stringBuilder.Append(String.Format("{0}: x{1}\n", ingredient.ToString("F"), Toppings[ingredient]));
                    }
                }
            }

            stringBuilder.Append(String.Format("\nSub Total:{0}", Value.ToString()));

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Evaluate a pizza against the order
        /// </summary>
        /// <typeparam name="TPizza">types of pizza, must implement <c>IPizza</c></typeparam>
        /// <param name="pizza">the pizza object</param>
        /// <returns>values from the order</returns>
        public OrderValue Evaluate<TPizza>(TPizza pizza) where TPizza: IPizza
        {
            float originalValue = this.Value.Cents + this.Value.Dollars * 100;

            float orderPercentageValue = 1f;

            int numMistakes = 0;

            switch (pizza.CookingLevel)
            {
                case CookingLevel.Uncooked:
                    orderPercentageValue -= UNDERCOOKED_PIZZA_PENALTY;
                    numMistakes += 3;
                    break;
                case CookingLevel.Burnt:
                    orderPercentageValue -= BURNT_PIZZA_PENALTY;
                    numMistakes += 3;
                    break;
                default:
                    break;
            }

            foreach (Ingredient ingredient in Ingredients.GetIngredients().ToList())
            {
                int mistakes = Mathf.Abs(pizza.Toppings[ingredient] - this.Toppings[ingredient]);

                orderPercentageValue -= MISSING_INGREDIENT_PENALTY * mistakes;
                numMistakes += mistakes;
            }

            if (numMistakes > 5)
            {
                orderPercentageValue = 0f;
            }

            orderPercentageValue = Mathf.Clamp(orderPercentageValue, 0f, 1f);

            float newValue = originalValue * orderPercentageValue;

            int newDollars = (int)(newValue / 100);
            int newCents = (int)(newValue % 100);

            OrderValue newOrderValue = new OrderValue(newDollars, newCents);

            return newOrderValue;
        }

        static int _nextOrderID = 0;

        /// <summary>
        /// Generate random order
        /// </summary>
        /// <param name="name">name for the random order</param>
        /// <returns>a random order</returns>
        public static Order Random(string name = "")
        {
            if (name == "")
            {
                name = $"Order {_nextOrderID}";
            }

            _nextOrderID++;

            var order = new Order()
            {
                Name = name
            };

            return order;
        }
    }
}