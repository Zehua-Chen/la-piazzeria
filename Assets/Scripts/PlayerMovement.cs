using UnityEngine;
using UnityEngine.Serialization;
using PizzaGame.Services;

namespace PizzaGame
{
    /// <summary>
    /// Controls movement of the player
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        float _speed = 10.0f;

        [SerializeField]
        [FormerlySerializedAs("_service")]
        PlayerControlService _playerControlService = null;

        private bool _moving = true;
        CharacterController _characterController = null;

        public AudioSource walkingSource;
        private bool isPlaying = false;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            // for pause menu
            if (!_moving) { return; }

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 forward = this.transform.forward;
            Vector3 left = Quaternion.AngleAxis(90.0f, Vector3.up) * forward;

            forward *= vertical;
            left *= horizontal;

            Vector3 offset = forward + left;

            // audio on / off on movement
            if (Mathf.Abs(offset.magnitude) < 0.5f)
            {
                walkingSource.Stop();
                isPlaying = false;
            }
            else
            {
                if (!isPlaying)
                {
                    walkingSource.Play();
                    isPlaying = true;
                }
            }

            _characterController.SimpleMove(offset * _speed);
        }

        private void OnEnable()
        {
            _playerControlService.ControlResumed.AddListener(this.OnPlayerControlResumed);
            _playerControlService.ControlStopped.AddListener(this.OnPlayerControlStopped);
        }

        private void OnDisable()
        {
            _playerControlService.ControlResumed.RemoveListener(this.OnPlayerControlResumed);
            _playerControlService.ControlStopped.RemoveListener(this.OnPlayerControlStopped);
        }

        private void OnPlayerControlResumed()
        {
            _moving = true;
        }

        private void OnPlayerControlStopped()
        {
            _moving = false;
        }
    }
}
