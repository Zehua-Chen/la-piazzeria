using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using PizzaGame.Orders;
using PizzaGame.Services;

namespace PizzaGame.UI
{
    /// <summary>
    /// Manages the UI of order submission station
    /// </summary>
    public class OrderSubmissionStationPanel : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField]
        GameObject _content = null;

        [SerializeField]
        Transform _orderListViewParent = null;

        [SerializeField]
        GameObject _orderLIstViewItemTemplate = null;

        [SerializeField]
        TextMeshProUGUI _detailsText = null;

        [Header("Services")]
        [SerializeField]
        InventoryService _inventoryService = null;

        [SerializeField]
        PlayerControlService _playerControlService = null;

        [SerializeField]
        OverlayPanelService _overlayPanelService = null;

        [Header("Events")]
        [SerializeField]
        OrderEvent _orderSelected = null;

        List<GameObject> _orderViewItems = new List<GameObject>();

        public OrderEvent OrderSelected => _orderSelected;

        private void Start()
        {
            _content.SetActive(false);
        }

        /// <summary>
        /// Show order submission panel
        /// </summary>
        public void Show()
        {
            foreach (Order order in _inventoryService.Orders)
            {
                GameObject orderListViewObject = Instantiate(_orderLIstViewItemTemplate, _orderListViewParent);
                OrderListViewItem orderListViewItem = orderListViewObject.GetComponent<OrderListViewItem>();

                orderListViewItem.Order = order;
                orderListViewItem.Selected.AddListener((Order selected) =>
                {
                    _orderSelected.Invoke(selected);
                    _detailsText.text = selected.ToString();
                });

                _orderViewItems.Add(orderListViewObject);
            }

            _content.SetActive(true);
            _playerControlService.ReleaseControl();
            _overlayPanelService.Retain();

            if (_inventoryService.Orders.Count > 0)
            {
                _orderSelected.Invoke(_inventoryService.Orders.First());
            }
        }

        /// <summary>
        /// Hide the order submission station panel
        /// </summary>
        public void Hide()
        {
            for (int i = 0; i < _orderViewItems.Count; i++)
            {
                Destroy(_orderViewItems[i]);
            }

            _orderViewItems.Clear();

            _content.SetActive(false);
            _playerControlService.RetainControl();
            _overlayPanelService.Release();
        }
    }
}