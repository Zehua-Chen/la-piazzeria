using UnityEngine;
using UnityEngine.Events;
using PizzaGame.Services;

namespace PizzaGame.Environment
{
    /// <summary>
    /// A state-machine-based zone that would drag the camera over once it has
    /// entered its collider.
    ///
    /// States:
    ///
    /// <list type="bullet">
    ///   <item>Idle</item>
    ///   <item>Grab</item>
    ///   <item>Release</item>
    /// </list>
    ///
    /// Transitions:
    ///
    /// <list>
    ///   <item>Idle -> Grab</item>
    ///   <item>Grab -> Release</item>
    ///   <item>Release -> Idle</item>
    /// </list>
    ///
    ///
    /// On Capture, MouseLook attached to the camera would be disabled. 
    /// </summary>
    public class FocusZone : MonoBehaviour
    {
        enum State
        {
            Idle,
            Grab,
            Release
        }

        [SerializeField]
        Transform _focusPoint = null;

        [SerializeField]
        float _cameraSmoothTime = 0.5f;

        [SerializeField]
        float _stopDisance = 0.1f;

        /// <summary>
        /// Invoked when player is captured
        /// </summary>
        [Header("Events")]
        [SerializeField]
        [Tooltip("Invoked when player is captured")]
        UnityEvent _cameraCaptured = null;

        [SerializeField]
        UnityEvent _cameraReleased = null;

        [SerializeField]
        PlayerControlService _playerControlService = null;

        public UnityEvent CameraCaptured => _cameraCaptured;
        public UnityEvent CameraReleased => _cameraReleased;

        State _state = State.Idle;

        Transform _camera;

        Vector3 _cameraVelocity;
        Vector3 _cameraStartPosition;
        Quaternion _cameraStartRotation;

        private void Update()
        {
            switch (_state)
            {
                case State.Grab:
                    this.Grab();
                    break;
                case State.Release:
                    this.Release();
                    break;
            }
        }

        private void Grab()
        {
            _camera.position = Vector3.SmoothDamp(
                _camera.position,
                _focusPoint.position,
                ref _cameraVelocity,
                _cameraSmoothTime);

            float remainingDistance = Vector3.Distance(_camera.position, _focusPoint.position);
            float totalDistance = Vector3.Distance(_cameraStartPosition, _focusPoint.position);

            float t = (totalDistance - remainingDistance) / totalDistance;

            _camera.rotation = Quaternion.Slerp(_cameraStartRotation, _focusPoint.rotation, t);
        }

        private void Release()
        {
            _camera.position = Vector3.SmoothDamp(
                _camera.position,
                _cameraStartPosition,
                ref _cameraVelocity,
                _cameraSmoothTime);

            float remainingDistance = Vector3.Distance(_camera.position, _cameraStartPosition);
            float totalDistance = Vector3.Distance(_cameraStartPosition, _focusPoint.position);

            float t = (totalDistance - remainingDistance) / totalDistance;

            _camera.rotation = Quaternion.Slerp(_focusPoint.rotation, _cameraStartRotation, t);

            if (remainingDistance < _stopDisance)
            {
                this.EnterIdle();
            }
        }

        private void EnterGrab(
            Transform camera,
            Vector3 cameraStartPosition,
            Quaternion cameraStartRotation)
        {
            _state = State.Grab;

            _camera = camera;

            _cameraStartPosition = cameraStartPosition;
            _cameraStartRotation = cameraStartRotation;

            _playerControlService.ReleaseControl();

            _cameraCaptured.Invoke();
        }

        private void EnterRelease()
        {
            _state = State.Release;
            _cameraReleased.Invoke();
        }

        private void EnterIdle()
        {
            _state = State.Idle;

            _camera.position = _cameraStartPosition;
            _camera.rotation = _cameraStartRotation;

            _camera = null;

            _playerControlService.RetainControl();
        }

        /// <summary>
        /// Release camera from focus and renable MouseLook
        /// </summary>
        public void ReleaseCamera()
        {
            if (_state == State.Grab)
            {
                this.EnterRelease();
            }
        }

        /// <summary>
        /// Crab a camera
        /// </summary>
        /// <param name="camera">the camera to grab</param>
        public void GrabCamera(Camera camera)
        {
            Transform cameraTransform = camera.transform;

            this.EnterGrab(
                cameraTransform,
                cameraTransform.position,
                cameraTransform.rotation);
        }
    }
}
