using UnityEngine;
using PizzaGame.Services;

namespace PizzaGame.UI
{
    /// <summary>
    /// Controls the cross sight
    /// </summary>
    public class CrossSight : MonoBehaviour
    {
        [SerializeField]
        PlayerControlService _playerControlService = null;

        private void OnEnable()
        {
            _playerControlService.ControlResumed.AddListener(this.OnControlResumed);
            _playerControlService.ControlStopped.AddListener(this.OnControlStopped);
        }

        private void OnDisable()
        {
            _playerControlService.ControlResumed.AddListener(this.OnControlResumed);
            _playerControlService.ControlStopped.AddListener(this.OnControlStopped);
        }

        private void OnControlResumed()
        {
            this.gameObject.SetActive(true);
        }

        private void OnControlStopped()
        {
            this.gameObject.SetActive(false);
        }
    }
}
