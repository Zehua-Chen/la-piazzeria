using PizzaGame.Pizzas;
using UnityEngine;
using PizzaGame.Services;

namespace PizzaGame.Environment
{
    /// <summary>
    /// Controls the oven station
    /// </summary>
    public class OvenStation : MonoBehaviour
    {
        [Header("Interactions")]
        [SerializeField]
        string _triggerButton = "Interact";

        [SerializeField]
        InteractableItem _interactableItem = null;

        [SerializeField]
        InventoryService _inventoryService = null;

        private const float COOK_TIMER = 25f;
        private float _ovenTimer;

        private Light[] _ovenLights;
        
        private bool _isOn;
        public Pizza pizza;

        [SerializeField]
        AudioSource _dingSoundEffect = null;

        [SerializeField]
        AudioSource _tickSoundEffect = null;

        private void Awake()
        {
            _interactableItem.LookedAt.AddListener(this.OnLookedAt);
        }

        // Start is called before the first frame update
        void Start()
        {
            _ovenLights = this.gameObject.GetComponentsInChildren<Light>();
            this.pizza = null;
            SwitchOvenState(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (_isOn)
            {
                if (this.pizza == null)
                {
                    Debug.LogError("Error: oven should never be on if it doesn't have a pizza");
                    return;
                }

                _ovenTimer += Time.deltaTime;

                if (_ovenTimer >= COOK_TIMER)
                {
                    // stop ticking sound at this point - most natural
                    _tickSoundEffect.Stop();

                    _dingSoundEffect.Play();
                    this.pizza.CookPizza();
                    _ovenTimer = 0f;
                }
            }
        }

        /// <summary>
        /// Update the state of the oven
        /// </summary>
        /// <param name="turnOn">oven's new state</param>
        public void SwitchOvenState(bool turnOn)
        {
            _isOn = turnOn;

            foreach (Light light in this._ovenLights)
            {
                light.enabled = turnOn;
            }

            _ovenTimer = 0f;
        }

        void PutPizzaInOven(Pizza _pizza)
        {
            if (this.pizza != null)
            {
                return;
            }

            if (!_pizza.IsOvenReady())
            {
                return;
            }

            this.pizza = _pizza;
            this.pizza.gameObject.SetActive(true);

            SwitchOvenState(true);

            // start ticking noise here - most natural
            _tickSoundEffect.Play();

            Vector3 pizzaLocalPosition = new Vector3(-0.25f, -0.15f, 0f);
            pizza.transform.position = this.gameObject.transform.position + pizzaLocalPosition;
        }

        void RemovePizzaFromOven()
        {
            if (this.pizza == null)
            {
                Debug.LogError("Cannot remove nonexistant pizza");
                return;
            }

            this.pizza.gameObject.SetActive(false);
            this.pizza = null;
            _tickSoundEffect.Stop();
            SwitchOvenState(false);
        }

        private void OnLookedAt()
        {
            if (Input.GetButtonDown(_triggerButton))
            {
                // Do something related to pizza and player
                Pizza playerPizza = _inventoryService.Pizza;
                _inventoryService.Pizza = null;
                Pizza ovenPizza = this.pizza;

                if (ovenPizza == null && playerPizza != null)
                {
                    PutPizzaInOven(playerPizza);
                }
                else if (ovenPizza != null && playerPizza == null)
                {
                    RemovePizzaFromOven();
                    _inventoryService.Pizza = ovenPizza;
                }
            }
        }
    }
}