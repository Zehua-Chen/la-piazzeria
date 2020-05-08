using System.Collections;
using UnityEngine;
using PizzaGame.Orders;

namespace PizzaGame.Environment
{
    /// <summary>
    /// Controls the order generator station
    /// </summary>
    public class OrderGeneratorStation : MonoBehaviour
    {
        [SerializeField]
        Transform _newOrderStart = null;

        [SerializeField]
        GameObject _orderTemplate = null;

        [SerializeField]
        float _minForce = 1.0f;

        [SerializeField]
        float _maxForce = 5.0f;

        [SerializeField]
        float _maxAngle = 45.0f;

        [SerializeField]
        float _minAngle = -45.0f;

        [SerializeField]
        AudioSource _ticketDispense = null;

        private float _orderGenerationInterval = 25f;

        private void Start()
        {
            StartCoroutine(this.SpawnOrder());
        }

        private IEnumerator SpawnOrder()
        {
            while (true)
            {
                GameObject order = Instantiate(_orderTemplate);

                OrderPaper orderItem = order.GetComponent<OrderPaper>();
                orderItem.Setup(Order.Random());

                Rigidbody orderRigidbody = order.GetComponent<Rigidbody>();
                orderRigidbody.MovePosition(_newOrderStart.position);

                Vector3 force = _newOrderStart.forward;
                force *= Random.Range(_minForce, _maxForce);

                Quaternion rotation = Quaternion.AngleAxis(
                    Random.Range(_minAngle, _maxAngle),
                    _newOrderStart.up);

                force = rotation * force;

                orderRigidbody.AddForce(force, ForceMode.Impulse);

                _ticketDispense.Play();

                yield return new WaitForSeconds(_orderGenerationInterval);
            }
        }
    }
}