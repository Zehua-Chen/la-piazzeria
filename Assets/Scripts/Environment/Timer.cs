using UnityEngine;
using PizzaGame.Services;

namespace PizzaGame.Environment
{
    /// <summary>
    /// A count down timer
    /// </summary>
    public class Timer : MonoBehaviour
    {
        [SerializeField]
        CountDown _countDown = new CountDown();

        [SerializeField]
        CountDownService _countDownService = null;

        float _time = 0.0f;

        private void Update()
        {
            _time += Time.deltaTime;

            if (_time > 1.0f)
            {
                this.CountDown();
                _countDownService.Ticked.Invoke(_countDown);
                _time = 0.0f;
            }
        }

        private void CountDown()
        {
            _countDown.Seconds--;

            if (_countDown.Seconds <= 0)
            {
                if (_countDown.Minutes > 0)
                {
                    _countDown.Minutes--;
                    _countDown.Seconds = 59;
                }
                else
                {
                    _countDown.Seconds = 0;
                }
            }
        }
    }
}