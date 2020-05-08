using System;
using UnityEngine;
using TMPro;
using PizzaGame.Services;

namespace PizzaGame.UI
{
    /// <summary>
    /// A interaction tip to hint the users what control should they use
    ///
    /// This item must be placed at the top of the canvas so that when other
    /// items are active, the interaction tip is hidden
    /// </summary>
    public class InteractionTip : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField]
        TextMeshProUGUI _prompt = null;

        [SerializeField]
        GameObject _content = null;

        [Header("Services")]
        [SerializeField]
        InteractionService _service = null;

        private void Start()
        {
            _content.SetActive(false);
        }

        private void OnEnable()
        {
            _service.MessagesChanged += this.OnMessagesChanged;
            _service.Hidden += this.OnHidden;
        }

        private void OnDisable()
        {
            _service.MessagesChanged -= this.OnMessagesChanged;
            _service.Hidden -= this.OnHidden;
        }

        private void OnMessagesChanged(object sender, string[] messages)
        {
            _content.SetActive(true);
            _prompt.text = string.Join(", ", messages);
        }

        private void OnHidden(object sender, EventArgs e)
        {
            _content.SetActive(false);
        }
    }
}