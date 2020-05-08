using UnityEngine;
using TMPro;
using PizzaGame.Services;

namespace PizzaGame.UI
{
    /// <summary>
    /// Displays the current count down
    /// </summary>
    public class CountDownPanel : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField]
        TextMeshProUGUI _timerLabel = null;

        [SerializeField]
        GameObject _content = null;

        [Header("Services")]
        [SerializeField]
        CountDownService _countDownService = null;

        [SerializeField]
        OverlayPanelService _overlayPanelService = null;

        private void OnEnable()
        {
            _countDownService.Ticked.AddListener(this.OnTick);
            _overlayPanelService.OverlaysOccured.AddListener(this.OnOverlaysOccured);
            _overlayPanelService.OverlaysGone.AddListener(this.OnOverlaysGone);
        }

        private void OnDisable()
        {
            _countDownService.Ticked.RemoveListener(this.OnTick);
            _overlayPanelService.OverlaysOccured.RemoveListener(this.OnOverlaysOccured);
            _overlayPanelService.OverlaysGone.RemoveListener(this.OnOverlaysGone);
        }

        private void OnOverlaysOccured()
        {
            _content.SetActive(false);
        }

        private void OnOverlaysGone()
        {
            _content.SetActive(true);
        }

        private void OnTick(CountDown countDown)
        {
            _timerLabel.text = string.Format("{0:00}:{1:00}", countDown.Minutes, countDown.Seconds);
        }
    }
}