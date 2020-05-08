using System.Collections.Generic;
using UnityEngine;
using PizzaGame.Orders;

namespace PizzaGame.Pizzas
{
    /// <summary>
    /// Represent an pizza object
    /// </summary>
    public class Pizza : MonoBehaviour, IPizza
    {
        [SerializeField]
        CookingLevel _cookingLevel = CookingLevel.Uncooked;

        private Renderer pizzaRenderer;

        /// <summary>
        /// Textures of different level of cooking
        /// </summary>
        public Texture[] cookingTextures;

        /// <summary>
        /// Textures of different levels of sauce
        /// </summary>
        public Texture[] sauceTextures;

        /// <summary>
        /// Textures of different level of cheese
        /// </summary>
        public Texture[] cheeseTextures;

        private int cheeseLevel;
        private int sauceLevel;

        private int MAX_CHEESE_LEVEL; 
        private int MAX_SAUCE_LEVEL;

        /// <summary>
        /// Level of cooking of the pizza
        /// </summary>
        public CookingLevel CookingLevel
        {
            get => _cookingLevel;
            set => _cookingLevel = value;
        }

        /// <summary>
        /// Name of the pizza
        /// </summary>
        public string Name
        {
            get { return this.gameObject.name; }
            set
            {
                this.gameObject.name = value;
            }
        }

        /// <summary>
        /// Toppings of the pizza
        ///
        /// Key is the ingredient, value is how many of the ingredients there
        /// are
        /// </summary>
        public Dictionary<Ingredient, int> Toppings { get; private set; } = new Dictionary<Ingredient, int>();

        #region Life Cycles

        private void Start()
        {
            foreach (Ingredient ingredient in Ingredients.GetIngredients())
            {
                this.Toppings[ingredient] = 0;
            }

            pizzaRenderer = GetComponentInChildren<Renderer>();
            cheeseLevel = 0;
            sauceLevel = 0;
            MAX_CHEESE_LEVEL = cheeseTextures.Length - 1;
            MAX_SAUCE_LEVEL = sauceTextures.Length - 1;
        }

        #endregion

        /// <summary>
        /// Add a topping
        /// </summary>
        /// <param name="ingredient">the topping</param>
        public void AddTopping(Ingredient ingredient)
        {
            this.Toppings[ingredient]++;
        }

        public override string ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// Add more sauce
        /// </summary>
        public void AddSauce()
        {
            if (sauceLevel == MAX_SAUCE_LEVEL)
            {
                return;
            }

            sauceLevel++;

            pizzaRenderer.material.mainTexture = sauceTextures[sauceLevel];
        }

        /// <summary>
        /// Add more cheese
        /// </summary>
        public void AddCheese()
        {
            if (sauceLevel < MAX_SAUCE_LEVEL || cheeseLevel == MAX_CHEESE_LEVEL)
            { 
                return; 
            }

            cheeseLevel++;

            pizzaRenderer.material.mainTexture = cheeseTextures[cheeseLevel];
        }

        /// <summary>
        /// Determine if the pizza can be put into ovens
        /// </summary>
        /// <returns>true if oven ready</returns>
        public bool IsOvenReady()
        {
            return cheeseLevel == MAX_CHEESE_LEVEL && sauceLevel == MAX_SAUCE_LEVEL;
        }

        /// <summary>
        /// Cook pizza. Each call would increase the cooking level
        /// </summary>
        public void CookPizza()
        {
            switch (this.CookingLevel)
            {
                case CookingLevel.Uncooked:
                    this.CookingLevel = CookingLevel.Cooked;
                    pizzaRenderer.material.mainTexture = cookingTextures[0];
                    break;
                case CookingLevel.Cooked:
                    this.CookingLevel = CookingLevel.Burnt;
                    pizzaRenderer.material.mainTexture = cookingTextures[1];
                    break;
                case CookingLevel.Burnt:
                default:
                    break;
            }
        }
    }
}
