using UnityEngine;
using UnityEngine.Events;

namespace PizzaGame.Services
{
    /// <summary>
    /// Coordinates over lay panels
    /// </summary>
    [CreateAssetMenu(menuName = "Services/Overlay Panel Service")]
    public class OverlayPanelService : PerSceneStatefulService
    {
        int _overlayCount = 0;

        [SerializeField]
        UnityEvent _overlaysOccured = null;

        [SerializeField]
        UnityEvent _overlaysGone = null;

        /// <summary>
        /// Invoked wnen an overlay has occured
        /// </summary>
        public UnityEvent OverlaysOccured => _overlaysOccured;

        /// <summary>
        /// Invoked when all overlays are gone
        /// </summary>
        public UnityEvent OverlaysGone => _overlaysGone;

        /// <summary>
        /// Indicate that an overlay has occured
        /// </summary>
        public void Retain()
        {
            _overlayCount++;
            _overlaysOccured.Invoke();
        }

        /// <summary>
        /// Indicate that an overlay has been dismissed
        /// </summary>
        public void Release()
        {
            _overlayCount--;

            if (_overlayCount <= 0)
            {
                _overlaysGone.Invoke();
                _overlayCount = 0;
            }
        }

        public override void Reset()
        {
            _overlayCount = 0;
            _overlaysGone.Invoke();
        }
    }
}