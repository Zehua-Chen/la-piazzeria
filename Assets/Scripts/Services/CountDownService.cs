using UnityEngine;

namespace PizzaGame.Services
{
    /// <summary>
    /// Manages count down notifications
    /// </summary>
    [CreateAssetMenu(menuName = "Services/Count Down Service")]
    public class CountDownService : ScriptableObject
    {
        [SerializeField]
        CountDownEvent _ticked = null;

        public CountDownEvent Ticked => _ticked;
    }
}