using UnityEngine;
using UnityEngine.UI;
using PizzaGame.Orders;
using PizzaGame.Pizzas;

namespace PizzaGame
{
    /// <summary>
    /// Deprecated class
    /// </summary>
    public class OrderButton : MonoBehaviour
    {
        // Start is called before the first frame update
        public Text OrderTextBox;
        public Text TipsJar;
        public Button SubmitOrderButton;

        private OrderValue currentTips;

        public ToppingStation toppingStation;
        private Order order;

        void Start()
        {
            OrderTextBox.text = "Order:";
            TipsJar.text = "Tips:";
            SubmitOrderButton.onClick.AddListener(SubmitPizza);
            currentTips = new OrderValue(0, 0);
            order = new Order();

            OrderTextBox.text = string.Format("Order: {0}", order.ToString());
        }

        // Update is called once per frame
        void Update()
        {

        }

        void SubmitPizza()
        {
            OrderValue submittedValue = order.Evaluate(toppingStation.Pizza);

            currentTips += submittedValue;

            TipsJar.text = string.Format("Tips: {0}", currentTips.ToString());

            order = new Order();

            OrderTextBox.text = string.Format("Order: {0}", order.ToString());
        }
    }

}
