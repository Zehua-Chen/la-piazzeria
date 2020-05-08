using UnityEngine;
using TMPro;
using PizzaGame.Orders;

namespace PizzaGame.UI
{
    /// <summary>
    /// Manages the order list view item
    /// </summary>
    public class OrderListViewItem : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI _title = null;

        [SerializeField]
        OrderEvent _selected = null;

        Order _order = null;

        public OrderEvent Selected => _selected;

        /// <summary>
        /// The order to use
        /// </summary>
        public Order Order
        {
            get { return _order; }
            set
            {
                _order = value;
                _title.text = _order.Name;
            }
        }

        public void OnClick()
        {
            _selected.Invoke(_order);
        }
    }
}