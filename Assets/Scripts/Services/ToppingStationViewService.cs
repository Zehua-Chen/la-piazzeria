using System;
using UnityEngine;

namespace PizzaGame.Services
{
    /// <summary>
    /// Manages the UI of topping station
    /// </summary>
    [CreateAssetMenu(menuName = "Services/Topping Station Service")]
    public class ToppingStationViewService : ScriptableObject
    {
        /// <summary>
        /// Invoked when new pizza is created
        /// </summary>
        public event EventHandler NewPizzaCreated;

        /// <summary>
        /// Invoked wehn the pizza is moved to inventory
        /// </summary>
        public event EventHandler PizzaMovedToInventory;

        /// <summary>
        /// Invoked when the UI (panel) is hidden
        /// </summary>
        public event EventHandler PanelHidden;

        /// <summary>
        /// Invoked when the UI (panel) is shown
        /// </summary>
        public event EventHandler PanelShown;

        /// <summary>
        /// Show panel
        /// </summary>
        public void ShowPanel()
        {
            this.PanelShown?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Hide panel
        /// </summary>
        public void HidePanel()
        {
            this.PanelHidden?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Indicate that the pizza is requested to be moved to the inventory
        /// </summary>
        public void MovePizzaToInventory()
        {
            this.PizzaMovedToInventory?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Indicate that a pizza is created
        /// </summary>
        public void CreatePizza()
        {
            this.NewPizzaCreated?.Invoke(this, new EventArgs());
        }
    }
}
