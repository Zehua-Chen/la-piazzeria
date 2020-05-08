using UnityEngine;
using UnityEngine.Serialization;
using PizzaGame.Services;

namespace PizzaGame
{
    /// <summary>
    /// Provides mouse control
    /// </summary>
    public class MouseLook : MonoBehaviour
    {
        [SerializeField]
        [FormerlySerializedAs("_service")]
        PlayerControlService _playerControlService = null;

        private bool _looking = true;

        /// <summary>
        /// static for easy access from other scripts
        /// default value: 100f -> mapped to center of slider at 0.5
        /// range: 0f - 200f
        /// </summary>
        public static float mouseSensitivity = 100f;

        /// <summary>
        /// Reference to player body
        /// </summary>
        public Transform playerBody;

        private float xRotation = 0f;

        private void Start()
        {
            IsCursorShown = false;
            _looking = true;
        }

        // Update is called once per frame
        private void Update()
        {
            if (!_looking)
            {
                return;
            }

            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            playerBody.Rotate(Vector3.up * mouseX);
        }

        private void OnEnable()
        {
            _playerControlService.ControlResumed.AddListener(this.OnUserControlResumed);
            _playerControlService.ControlStopped.AddListener(this.OnUserControlStopped);
        }

        private void OnDisable()
        {
            _playerControlService.ControlResumed.RemoveListener(this.OnUserControlResumed);
            _playerControlService.ControlStopped.RemoveListener(this.OnUserControlStopped);
        }

        private void OnUserControlResumed()
        {
            IsCursorShown = false;
            _looking = true;
        }

        private void OnUserControlStopped()
        {
            IsCursorShown = true;
            _looking = false;
        }

        static bool _isCursorShown = true;

        /// <summary>
        /// If cursor is shown. Setting true would cause the cursor to hide
        /// </summary>
        public static bool IsCursorShown
        {
            get { return _isCursorShown; }
            set
            {
                _isCursorShown = value;

                if (!_isCursorShown)
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                else
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }
    }
}
