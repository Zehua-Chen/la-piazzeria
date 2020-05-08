using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using PizzaGame.Orders;
using PizzaGame.Services;
using PizzaGame.Pizzas;

namespace PizzaGame.UI
{
    /// <summary>
    /// Manages the inventory panel
    /// </summary>
    public class InventoryPanel : MonoBehaviour
    {
        [Header("Templates")]
        [SerializeField]
        GameObject _orderListViewItemTemplate = null;

        [SerializeField]
        GameObject _pizzaListViewItemTemplate = null;

        [Header("UI References")]
        [SerializeField]
        Text _orderTextBox = null;

        [SerializeField]
        [FormerlySerializedAs("_content")]
        Transform _viewItemsParent = null;

        [SerializeField]
        GameObject _content = null;

        [Header("Services")]
        [SerializeField]
        InventoryService _inventoryService = null;

        [SerializeField]
        PlayerControlService _playerControlService = null;

        [SerializeField]
        OverlayPanelService _overlayPanelService = null;

        List<GameObject> _viewItems = new List<GameObject>();

        private void Start()
        {
            _content.SetActive(false);
        }

        private void OnEnable()
        {
            _inventoryService.InventoryPanelHidden += this.OnHidden;
            _inventoryService.InventoryPanelShown += this.OnShown;
        }

        private void OnDisable()
        {
            _inventoryService.InventoryPanelHidden -= this.OnHidden;
            _inventoryService.InventoryPanelShown -= this.OnShown;
        }

        private void Reset()
        {
            foreach (GameObject item in _viewItems)
            {
                Destroy(item);
            }

            _viewItems.Clear();
        }

        private void Show(IEnumerable<Order> orders, Pizza pizza)
        {
            this.Reset();
            this.gameObject.SetActive(true);

            _playerControlService.ReleaseControl();
            _overlayPanelService.Retain();

            foreach (Order order in orders)
            {
                GameObject orderItemObject = Instantiate(_orderListViewItemTemplate, _viewItemsParent);
                OrderListViewItem orderListViewItem = orderItemObject.GetComponent<OrderListViewItem>();

                orderListViewItem.Order = order;
                orderListViewItem.Selected.AddListener(this.DisplayOrder);

                _viewItems.Add(orderItemObject);
            }

            if (pizza != null)
            {
                GameObject pizzaItem = Instantiate(_pizzaListViewItemTemplate, _viewItemsParent);
                pizzaItem.GetComponent<PizzaListViewItem>().Pizza = pizza;

                _viewItems.Add(pizzaItem);
            }
            

            _orderTextBox.text = "Click on an order to read it!";
        }

        private void OnShown(object sender, EventArgs e)
        {
            _content.SetActive(true);
            this.Show(_inventoryService.Orders, _inventoryService.Pizza);
        }

        private void OnHidden(object sender, EventArgs e)
        {
            _orderTextBox.text = "";
            _content.SetActive(false);

            _playerControlService.RetainControl();
            _overlayPanelService.Release();
        }

        public void OnDone()
        {
            _inventoryService.HidePanel();
        }

        public void DisplayOrder(Order order)
        {
            _orderTextBox.text = order.ToString();
        }
    }
}