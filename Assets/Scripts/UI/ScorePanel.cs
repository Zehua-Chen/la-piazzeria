using UnityEngine;
using UnityEngine.Serialization;
using TMPro;
using PizzaGame.Services;
using PizzaGame.Orders;

namespace PizzaGame.UI
{
    /// <summary>
    /// Controls the score panel
    /// </summary>
    public class ScorePanel : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField]
        TextMeshProUGUI _scoreLabel = null;

        [SerializeField]
        GameObject _content = null;

        [Header("Services")]
        [SerializeField]
        [FormerlySerializedAs("_service")]
        ScoreService _scoreService = null;

        [SerializeField]
        OverlayPanelService _overlayPanelService = null;

        private void OnEnable()
        {
            _scoreService.ScoreChanged += this.OnScoreChanged;
            _overlayPanelService.OverlaysOccured.AddListener(this.OnOverlaysOccred);
            _overlayPanelService.OverlaysGone.AddListener(this.OnOverlaysGone);
        }

        private void OnDisable()
        {
            _scoreService.ScoreChanged -= this.OnScoreChanged;
            _overlayPanelService.OverlaysOccured.RemoveListener(this.OnOverlaysOccred);
            _overlayPanelService.OverlaysGone.RemoveListener(this.OnOverlaysGone);
        }

        private void OnOverlaysOccred()
        {
            _content.SetActive(false);
        }

        private void OnOverlaysGone()
        {
            _content.SetActive(true);
        }

        private void OnScoreChanged(object sender, OrderValue newScore)
        {
            _scoreLabel.text = string.Format("Tips: {0:C}", newScore);
        }
    }
}