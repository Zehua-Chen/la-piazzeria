using System;
using UnityEngine.Events;

namespace PizzaGame.Services
{
    /// <summary>
    /// Represnets a time stamp in count down
    /// </summary>
    [Serializable]
    public struct CountDown
    {
        public int Seconds;
        public int Minutes;
    }

    /// <summary>
    /// Events whose first parameter is count down
    /// </summary>
    [Serializable]
    public class CountDownEvent : UnityEvent<CountDown>
    {
    }
}