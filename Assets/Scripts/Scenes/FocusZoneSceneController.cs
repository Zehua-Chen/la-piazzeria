using UnityEngine;
using UnityEngine.UI;
using PizzaGame.Environment;

namespace PizzaGame.Scenes
{
    /// <summary>
    /// Controls the scene that test focus zone
    /// </summary>
    public class FocusZoneSceneController : MonoBehaviour
    {
        [SerializeField]
        FocusZone _focusZone = null;

        [SerializeField]
        GameObject _capturedUI = null;

        [SerializeField]
        Button _releaseButton = null;

        private void Awake()
        {
            _focusZone.CameraCaptured.AddListener(this.OnCameraCaptured);
            _releaseButton.onClick.AddListener(this.OnRelease);

            _capturedUI.SetActive(false);
        }

        private void OnCameraCaptured()
        {
            _capturedUI.SetActive(true);
        }

        public void OnRelease()
        {
            _capturedUI.SetActive(false);
            _focusZone.ReleaseCamera();
        }
    }

}
