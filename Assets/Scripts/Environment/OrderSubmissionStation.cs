using UnityEngine;
using UnityEngine.Serialization;
using PizzaGame.Pizzas;
using PizzaGame.Orders;
using PizzaGame.Services;
using PizzaGame.UI;

namespace PizzaGame.Environment
{
    /// <summary>
    /// Controls the order submission station
    /// </summary>
    public class OrderSubmissionStation : MonoBehaviour
    {
        [Header("Interaction")]
        [SerializeField]
        string _triggerButton = "Interact";

        [SerializeField]
        InteractableItem _interactableItem = null;

        [SerializeField]
        OrderSubmissionStationPanel _orderSubmissionStationPanel = null;

        [Header("Services")]
        [FormerlySerializedAs("_service")]
        [SerializeField]
        ScoreService _scoreService = null;

        [SerializeField]
        InventoryService _inventoryService = null;

        Pizza _pizza = null;
        Order _order = null;

        /// <summary>
        /// Materials of the table
        /// </summary>
        public Material[] tableMaterials;

        /// <summary>
        /// Audios played when order is submitted
        /// </summary>
        public AudioSource[] orderEvaluationAudioSources;

        private const int MATERIAL_CORRECT_IDX = 0, MATERIAL_INCORRECT_IDX = 1, MATERIAL_DEFAULT_IDX = 2;
        private const int AUDIO_CORRECT_IDX = 0, AUDIO_INCORRECT_IDX = 1;

        private Renderer _tableRenderer;

        /// <summary>
        /// Reference to the submission table
        /// </summary>
        public GameObject submissionTable;

        private const float DISPLAY_TIMER = 2f;
        private float _currentTimer = 0f;
        private bool _displayingPizza = false;

        #region Life Cycles

        private void Awake()
        {
            _interactableItem.LookedAt.AddListener(this.OnLookedAt);
            _tableRenderer = submissionTable.GetComponent<Renderer>();
        }

        private void OnEnable()
        {
            _orderSubmissionStationPanel.OrderSelected.AddListener(this.OnOrderSelected);
        }

        private void OnDisable()
        {
            _orderSubmissionStationPanel.OrderSelected.RemoveListener(this.OnOrderSelected);
        }

        private void Update()
        {
            if (_displayingPizza)
            {
                _currentTimer += Time.deltaTime;

                if (_currentTimer >= DISPLAY_TIMER)
                {
                    RemovePizza();
                }
            }
        }

        #endregion

        #region Helpers

        private void SubmitPizza()
        {
            OrderValue value = _order.Evaluate(_pizza);

            _scoreService.Score += value;

            if (value == _order.Value)
            {
                _tableRenderer.material = tableMaterials[MATERIAL_CORRECT_IDX];
                orderEvaluationAudioSources[AUDIO_CORRECT_IDX].Play();
            }
            else
            {
                _tableRenderer.material = tableMaterials[MATERIAL_INCORRECT_IDX];
                orderEvaluationAudioSources[AUDIO_INCORRECT_IDX].Play();
            }

            _currentTimer = 0f;
            _displayingPizza = true;
            _pizza.gameObject.SetActive(true);

            Vector3 pizzaLocalPosition = new Vector3(-0.35f, 1.3f, 0f);

            _pizza.transform.position = gameObject.transform.position + pizzaLocalPosition;
        }

        private void RemovePizza()
        {
            _tableRenderer.material = tableMaterials[MATERIAL_DEFAULT_IDX];
            _pizza.gameObject.SetActive(false);
            Destroy(_pizza);
            _pizza = null;
            _order = null;
            _displayingPizza = false;
            _currentTimer = 0f;
        }

        private void OnLookedAt()
        {
            if (Input.GetButtonDown(_triggerButton) && !_displayingPizza)
            {
                if (_inventoryService.Orders.Count == 0 || _inventoryService.Pizza == null)
                {
                    return;
                }

                _pizza = _inventoryService.Pizza;
                _inventoryService.Pizza = null;
                _interactableItem.Deactivate();

                _orderSubmissionStationPanel.Show();
            }
        }

        #endregion

        #region Event Handlers

        private void OnOrderSelected(Order order)
        {
            _order = order;
        }

        /// <summary>
        /// Callback for <c>OrderSubmissionStationPanel.Done</c>
        /// </summary>
        public void OnDone()
        {
            _orderSubmissionStationPanel.Hide();
            _inventoryService.Orders.Remove(_order);

            _interactableItem.Activate();

            SubmitPizza();

            _order = null;
        }

        #endregion
    }
}