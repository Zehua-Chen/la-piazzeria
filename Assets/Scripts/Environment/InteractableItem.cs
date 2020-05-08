using System;
using UnityEngine;
using UnityEngine.Events;
using PizzaGame.Services;

namespace PizzaGame.Environment
{
    /// <summary>
    /// An item that when looked at, displays an interaction tip
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class InteractableItem : MonoBehaviour
    {
        [SerializeField]
        string[] _messages = null;

        [SerializeField]
        float _interactionDistance = 5.0f;

        [SerializeField]
        bool _acitvated = true;

        [SerializeField]
        InteractionService _service = null;

        [SerializeField]
        UnityEvent _lookedAt = null;

        [SerializeField]
        int _deactivationCount = 0;

        bool _showingTip = false;
        public UnityEvent LookedAt => _lookedAt;

        private void OnEnable()
        {
            _service.ItemsActivationRequested += this.OnActivationRequested;
            _service.ItemsDeactivationRequested += this.OnDeactivationRequested;
        }

        private void OnDisable()
        {
            _service.ItemsActivationRequested -= this.OnActivationRequested;
            _service.ItemsDeactivationRequested -= this.OnDeactivationRequested;
        }

        private void OnDeactivationRequested(object sender, EventArgs e)
        {
            this.Deactivate();
        }

        private void OnActivationRequested(object sender, EventArgs e)
        {
            this.Activate();
        }

        private void OnMouseOver()
        {
            if (_acitvated)
            {
                this.Test();
            }
        }

        private void OnMouseExit()
        {
            this.Hide();
        }

        private void OnDestroy()
        {
            this.Hide();
        }

        private void Hide()
        {
            _service.Hide();
            _showingTip = false;
        }

        private void Test()
        {
            Player player = PizzaGame.Player.Shared.Value;
            float distance = Vector3.Distance(player.transform.position, this.transform.position);

            if (distance < _interactionDistance)
            {
                if (!_showingTip)
                {
                    _service.Show(_messages);
                    _showingTip = true;
                }

                _lookedAt.Invoke();
            }
            else
            {
                if (_showingTip)
                {
                    this.Hide();
                }
            }
        }

        /// <summary>
        /// Deactivate the item such that when it is looked at, no
        /// tool tip is displayed
        /// </summary>
        public void Deactivate()
        {
            _deactivationCount++;
            _acitvated = false;
            this.Hide();
        }

        /// <summary>
        /// Reactivate the item such that when it is looked at, tool tip
        /// is displayed
        /// </summary>
        public void Activate()
        {
            _deactivationCount--;

            if (_deactivationCount <= 0)
            {
                _acitvated = true;
                _deactivationCount = 0;
            }
        }
    }
}