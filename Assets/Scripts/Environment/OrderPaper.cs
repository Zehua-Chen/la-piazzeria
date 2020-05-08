using UnityEngine;
using PizzaGame.Orders;
using PizzaGame.Services;

namespace PizzaGame.Environment
{
    /// <summary>
    /// Scripts that controls the order paper
    /// </summary>
    public class OrderPaper : MonoBehaviour
    {
        [SerializeField]
        InteractableItem _interactableItem = null;

        [SerializeField]
        string _viewButton = "Interact";

        [SerializeField]
        string _pickUpButton = "PickUp";

        [SerializeField]
        OrderViewService _orderViewService = null;

        [SerializeField]
        InventoryService _inventoryService = null;

        [SerializeField]
        Order _order;

        private void Awake()
        {
            _orderViewService.Dismissed += OnViewDismissed;
        }

        /// <summary>
        /// Setup using an order
        /// </summary>
        /// <param name="order">the order to setup with</param>
        public void Setup(Order order)
        {
            _order = order;
        }

        /// <summary>
        /// Event handler for <c>InteractableItem.LookedAt</c>
        /// </summary>
        public void OnLookedItem()
        {
            if (Input.GetButton(_viewButton))
            {
                _interactableItem.Deactivate();
                _orderViewService.Order = _order;
            }
            else if (Input.GetButton(_pickUpButton))
            {
                _inventoryService.Orders.Add(_order);
                Destroy(this.gameObject);
            }
        }

        private void OnViewDismissed(object sender, System.EventArgs e)
        {
            _interactableItem.Activate();
        }
    }
}