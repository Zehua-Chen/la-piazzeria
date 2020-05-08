using UnityEngine;
using PizzaGame.Orders;

namespace PizzaGame.Pizzas
{
    /// <summary>
    /// The source of ingredients
    /// </summary>
    public class ToppingStationIngredientSource : MonoBehaviour
    {
        [SerializeField]
        Ingredient _ingredient = Ingredient.Mushroom;

        [SerializeField]
        IngredientEvent _selected = null;

        private void OnMouseDown()
        {
            _selected.Invoke(_ingredient);
        }
    }
}