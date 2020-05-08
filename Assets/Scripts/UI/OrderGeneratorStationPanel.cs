using System;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using PizzaGame.Orders;
using PizzaGame.Services;

namespace PizzaGame.UI
{
    /// <summary>
    /// Manages UI of order generator station
    /// </summary>
    public class OrderGeneratorStationPanel : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField]
        GameObject _content = null;

        [SerializeField]
        TextMeshProUGUI _orderName = null;

        [SerializeField]
        TextMeshProUGUI _orderDetails = null;

        [Header("Services")]
        [SerializeField]
        OrderViewService _orderViewService = null;

        [SerializeField]
        PauseService _pauseService = null;

        [SerializeField]
        PlayerControlService _playerControlService = null;

        [SerializeField]
        OverlayPanelService _overlayPanelService = null;

        [Header("Events")]
        [SerializeField]
        UnityEvent _done = null;

        bool _used = false;

        public UnityEvent Done => _done;
        public Order Order;

        private void Start()
        {
            _content.SetActive(false);
        }

        private void OnEnable()
        {
            _orderViewService.OrderShown += OnOrderChanged;
            _orderViewService.Dismissed += OnDismiss;

            _pauseService.Paused += this.OnPaused;
            _pauseService.Resumed += this.OnResumed;
        }

        private void OnDisable()
        {
            _orderViewService.OrderShown -= OnOrderChanged;
            _orderViewService.Dismissed -= OnDismiss;

            _pauseService.Paused -= this.OnPaused;
            _pauseService.Resumed -= this.OnResumed;
        }

        private void OnOrderChanged(object sender, Order order)
        {
            _used = true;
            _orderName.text = order.Name;
            _orderDetails.text = order.ToString();

            _content.SetActive(true);
            _playerControlService.ReleaseControl();
            _overlayPanelService.Retain();
        }

        private void OnDismiss(object sender, EventArgs e)
        {
            _used = false;
            _content.SetActive(false);
            _overlayPanelService.Release();
            _playerControlService.RetainControl();
        }

        private void OnPaused(object sender, EventArgs e)
        {
            if (_used)
            {
                _content.SetActive(false);
            }
        }

        private void OnResumed(object sender, EventArgs e)
        {
            if (_used)
            {
                _content.SetActive(true);
            }
        }

        /// <summary>
        /// Event handler for Done button
        /// </summary>
        public void OnDoneClick()
        {
            _orderViewService.Dismiss();
        }
    }
}
