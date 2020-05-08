using UnityEngine;
using UnityEngine.Events;

namespace PizzaGame.Services
{
    /// <summary>
    /// Manaages player's control of the invisble character
    /// </summary>
    [CreateAssetMenu(menuName = "Services/Player Control Service")]
    public class PlayerControlService : PerSceneStatefulService
    {
        [SerializeField]
        UnityEvent _userControlResumed = null;

        [SerializeField]
        UnityEvent _userControlStopped = null;

        /// <summary>
        /// Number of release requests currently active
        /// </summary>
        public int ReleaseCount { get; private set; } = 0;

        public UnityEvent ControlResumed => _userControlResumed;
        public UnityEvent ControlStopped => _userControlStopped;

        /// <summary>
        /// Request to regain user control
        /// </summary>
        public void RetainControl()
        {
            ReleaseCount--;

            if (ReleaseCount <= 0)
            {
                this.ControlResumed.Invoke();
                ReleaseCount = 0;
            }
        }

        /// <summary>
        /// Request to release user control
        /// </summary>
        public void ReleaseControl()
        {
            ReleaseCount++;
            this.ControlStopped.Invoke();
        }

        public override void Reset()
        {
            this.ReleaseCount = 0;
            this.ControlResumed.Invoke();
        }
    }
}
