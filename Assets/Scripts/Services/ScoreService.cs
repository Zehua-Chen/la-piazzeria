using PizzaGame.Orders;
using System;
using UnityEngine;

namespace PizzaGame.Services
{
    /// <summary>
    /// Manages the score of the user
    /// </summary>
    [CreateAssetMenu(menuName = "Services/Score Service")]
    public class ScoreService : PerSceneStatefulService
    {
        // int _score = 0;
        OrderValue _score = new OrderValue(0, 0);

        /// <summary>
        /// Score of the user, setting this would invoke
        /// <c>ScoreChanged</c>
        /// </summary>
        public OrderValue Score
        {
            get { return _score; }
            set
            {
                _score = value;
                this.ScoreChanged?.Invoke(this, _score);
            }
        }

        /// <summary>
        /// Invoked when score changes
        /// </summary>
        public event EventHandler<OrderValue> ScoreChanged;

        public override void Reset()
        {
            _score = new OrderValue(0, 0);
        }
    }
}