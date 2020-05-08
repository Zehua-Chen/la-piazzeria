using System;
using System.Collections.Generic;
using UnityEngine;
using PizzaGame.Orders;
using PizzaGame.Pizzas;

namespace PizzaGame.Services
{
    /// <summary>
    /// Manages the inventory of the player
    /// </summary>
    [CreateAssetMenu(menuName = "Services/Inventory Service")]
    public class InventoryService : PerSceneStatefulService
    {
        [SerializeField]
        InteractionService _interactionService = null;

        /// <summary>
        /// A set of orders
        /// </summary>
        public HashSet<Order> Orders { get; private set; } = new HashSet<Order>();

        /// <summary>
        /// A pizza
        /// </summary>
        public Pizza Pizza { get; set; }

        /// <summary>
        /// Invoked when inventory is required to show
        /// </summary>
        public event EventHandler InventoryPanelShown;

        /// <summary>
        /// Invoked when inventory is required to hide
        /// </summary>
        public event EventHandler InventoryPanelHidden;

        /// <summary>
        /// Show inventory panel
        /// </summary>
        public void ShowPanel()
        {
            _interactionService.DeactivateAllItems();
            this.InventoryPanelShown?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Hide inventory panel
        /// </summary>
        public void HidePanel()
        {
            _interactionService.ActivateAllItems();
            this.InventoryPanelHidden?.Invoke(this, EventArgs.Empty);
        }

        public override void Reset()
        {
            this.Orders.Clear();
            this.Pizza = null;
        }
    }
}