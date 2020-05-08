using System;
using UnityEngine;

namespace PizzaGame.Services
{
    /// <summary>
    /// Manages pauses
    /// </summary>
    [CreateAssetMenu(menuName = "Services/Pause Service")]
    public class PauseService : ScriptableObject
    {
        /// <summary>
        /// Invoked when the game is paused
        /// </summary>
        public event EventHandler Paused;

        /// <summary>
        /// Invoked when the game is resumed
        /// </summary>
        public event EventHandler Resumed;

        /// <summary>
        /// Pause the game
        /// </summary>
        public void Pause()
        {
            this.Paused?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Resume the game
        /// </summary>
        public void Resume()
        {
            this.Resumed?.Invoke(this, EventArgs.Empty);
        }
    }
}