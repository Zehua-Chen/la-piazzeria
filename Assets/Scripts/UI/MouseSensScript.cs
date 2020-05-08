using UnityEngine;

namespace PizzaGame.UI
{
    /// <summary>
    /// Controls mouse sensitivity slider
    /// </summary>
    public class MouseSensScript : MonoBehaviour
    {
        /// <summary>
        /// Event handler for the slider
        /// </summary>
        /// <param name="sens">sensitivity</param>
        public void SetSens(float sens)
        {
            // default value: 100f -> mapped to center of slider at 0.5, hence (2 * sens)
            // range: 0f - 200f
            PizzaGame.MouseLook.mouseSensitivity = (2 * sens) * 100f;
        }
    }

}
