using System;
using System.Collections.Generic;
using UnityEngine;

namespace PizzaGame.Services
{
    /// <summary>
    /// Manages communication between interaction tip and interactable items
    /// </summary>
    [CreateAssetMenu(menuName = "Services/Interaction Service")]
    public class InteractionService : PerSceneStatefulService
    {
        struct MessageGroup
        {
            public string[] Messages;
        }

        Stack<MessageGroup> _messages = new Stack<MessageGroup>();

        public event EventHandler<string[]> MessagesChanged;
        public event EventHandler Hidden;

        public event EventHandler ItemsDeactivationRequested;
        public event EventHandler ItemsActivationRequested;

        /// <summary>
        /// Push a set of interaction tips
        /// </summary>
        /// <param name="messages">the tips to show</param>
        public void Show(params string[] messages)
        {
            _messages.Push(new MessageGroup()
            {
                Messages = messages
            });

            this.MessagesChanged?.Invoke(this, _messages.Peek().Messages);
        }

        /// <summary>
        /// Hide the last interaction tips
        /// </summary>
        public void Hide()
        {
            if (_messages.Count > 0)
            {
                _messages.Pop();

                if (_messages.Count == 0)
                {
                    this.Hidden?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    this.MessagesChanged?.Invoke(this, _messages.Peek().Messages);
                }
            }
        }

        /// <summary>
        /// Deactivate all interaction items
        /// </summary>
        public void DeactivateAllItems()
        {
            this.ItemsDeactivationRequested?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Activate all interaction items
        /// </summary>
        public void ActivateAllItems()
        {
            this.ItemsActivationRequested?.Invoke(this, EventArgs.Empty);
        }

        public override void Reset()
        {
            _messages.Clear();
        }
    }
}