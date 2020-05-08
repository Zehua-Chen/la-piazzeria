using System;
using UnityEngine;
using UnityEngine.Events;
using PizzaGame.Services;

namespace PizzaGame.UI
{
    /// <summary>
    /// Controls the topping station panel
    /// </summary>
    public class ToppingStationPanel : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField]
        GameObject _content = null;

        [Header("Services")]
        [SerializeField]
        InventoryService _inventoryService = null;

        [SerializeField]
        OverlayPanelService _overlayPanelService = null;

        [Header("Events")]
        [SerializeField]
        UnityEvent _pizzaMovedToInventory = null;

        [SerializeField]
        UnityEvent _newPizzaCreated = null;

        [SerializeField]
        UnityEvent _done = null;

        bool _used = false;

        public UnityEvent PizzaMovedToInventory => _pizzaMovedToInventory;
        public UnityEvent NewPizzaCreated => _newPizzaCreated;
        public UnityEvent Done => _done;

        private void Start()
        {
            _content.SetActive(false);
        }

        private void OnEnable()
        {
            _inventoryService.InventoryPanelShown += this.InventoryPanelShown;
            _inventoryService.InventoryPanelHidden += this.InventoryPanelHidden;
        }

        private void OnDisable()
        {
            _inventoryService.InventoryPanelShown -= this.InventoryPanelShown;
            _inventoryService.InventoryPanelHidden -= this.InventoryPanelHidden;
        }

        private void InventoryPanelShown(object sender, EventArgs e)
        {
            if (_used)
            {
                _content.SetActive(false);
            }
        }

        private void InventoryPanelHidden(object sender, EventArgs e)
        {
            if (_used)
            {
                _content.SetActive(true);
            }
        }

        /// <summary>
        /// Show panel
        /// </summary>
        public void Show()
        {
            _used = true;
            _content.SetActive(true);
            _overlayPanelService.Retain();
        }

        /// <summary>
        /// Hide panel
        /// </summary>
        public void Hide()
        {
            _used = false;
            _content.SetActive(false);
            _overlayPanelService.Release();
        }

        /// <summary>
        /// Event handler for new pizza button
        /// </summary>
        public void OnNewPizza()
        {
            _newPizzaCreated.Invoke();
        }

        /// <summary>
        /// Event handler for done button
        /// </summary>
        public void OnDone()
        {
            _done.Invoke();
        }

        /// <summary>
        /// Event handler for moving to inventory
        /// </summary>
        public void OnMovedToInventory()
        {
            _pizzaMovedToInventory.Invoke();
        }
    }
}
