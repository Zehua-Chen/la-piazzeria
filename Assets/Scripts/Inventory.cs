using System;
using UnityEngine;
using PizzaGame.Services;

namespace PizzaGame
{
    /// <summary>
    /// Provides inventory interaction for players
    /// </summary>
    public class Inventory : MonoBehaviour
    {
        [SerializeField]
        string _triggerButton = "Inventory";

        [SerializeField]
        InventoryService _inventoryService = null;

        bool _panelShown = false;

        private void OnEnable()
        {
            _inventoryService.InventoryPanelHidden += this.OnPanelHidden;
        }

        private void OnDisable()
        {
            _inventoryService.InventoryPanelHidden -= this.OnPanelHidden;
        }

        private void Update()
        {
            if (Input.GetButtonDown(_triggerButton))
            {
                if (!_panelShown)
                {
                    _inventoryService.ShowPanel();
                    _panelShown = true;
                }
            }
        }

        private void OnPanelHidden(object sender, EventArgs e)
        {
            _panelShown = false;
        }
    }
}