using UnityEngine;
using PizzaGame.Orders;
using PizzaGame.Pizzas;

namespace PizzaGame.Environment
{
    /// <summary>
    /// Controls the ingredient of topping station
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class ToppingStationIngredient : MonoBehaviour
    {
        [SerializeField]
        int _pizzaLayer = 10;

        [SerializeField]
        Ingredient _ingredient = Ingredient.Pepperoni;

        Rigidbody _rigidbody = null;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == _pizzaLayer)
            {
                _rigidbody.isKinematic = true;

                Pizza pizza = collision.gameObject.GetComponentInParent<Pizza>();
                this.transform.parent = pizza.transform;
            }
        }
    }
}
