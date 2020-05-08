using System;
using UnityEngine;
using PizzaGame.Orders;

namespace PizzaGame.Services
{
    /// <summary>
    /// Services that manages the viewing of orders
    /// </summary>
    [CreateAssetMenu(menuName = "Services/Order View Service")]
    public class OrderViewService : PerSceneStatefulService
    {
        Order _order = null;

        /// <summary>
        /// The order to view. Setting this would invoke <c>OrderShown</c>
        /// </summary>
        public Order Order
        {
            get { return _order; }
            set
            {
                _order = value;

                if (_order != null)
                {
                    this.OrderShown?.Invoke(this, _order);
                }
            }
        }

        /// <summary>
        /// Invoked when an order is required to show
        /// </summary>
        public event EventHandler<Order> OrderShown;

        /// <summary>
        /// Invoked when the view panel is dismissed
        /// </summary>
        public event EventHandler Dismissed;

        /// <summary>
        /// Dismiss the view panel
        /// </summary>
        public void Dismiss()
        {
            this.Dismissed?.Invoke(this, new EventArgs());
        }

        public override void Reset()
        {
            base.Reset();

            _order = null;
        }
    }
}