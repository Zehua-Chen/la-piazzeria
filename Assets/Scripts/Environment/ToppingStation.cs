using UnityEngine;
using PizzaGame.Orders;
using PizzaGame.UI;
using PizzaGame.Environment;
using PizzaGame.Services;
using System.Collections.Generic;

namespace PizzaGame.Pizzas
{
    /// <summary>
    /// Controls the topping station
    /// </summary>
    public class ToppingStation : MonoBehaviour
    {
        [Header("Pizza Creation")]
        [SerializeField]
        GameObject _pizzaTemplate = null;

        [SerializeField]
        GameObject _pepperoniTemplate = null;

        [SerializeField]
        GameObject _oliveTemplate = null;

        [SerializeField]
        GameObject _sausageTemplate = null;

        [SerializeField]
        GameObject _mushroomTemplate = null;

        [SerializeField]
        LayerMask _pizzaLayerMask = new LayerMask();

        [SerializeField]
        Transform _newPizzaTransform = null;

        [Header("Interactions")]
        [SerializeField]
        string _triggerButton = "Interact";

        [SerializeField]
        string _cheeseButton = "Cheese";

        [SerializeField]
        string _sauceButton = "Sauce";

        [SerializeField]
        string _kneadButton = "Knead";

        [SerializeField]
        float _toppingCoolDown = 0.5f;

        [SerializeField]
        InteractableItem _interactableItem = null;

        [SerializeField]
        FocusZone _focusZone = null;

        [Header("UI")]
        [SerializeField]
        ToppingStationPanel _toppingStationPanel = null;

        [Header("Services")]
        [SerializeField]
        InventoryService _inventoryService = null;

        [SerializeField]
        private Dictionary<KeyCode, Ingredient> _ingredientKeyMapping = null;

        [SerializeField]
        private Dictionary<Ingredient, GameObject> _ingredientTemplateMapping = null;

        public GameObject[] doughLumps;

        int _maxDoughLump = 0;
        int _doughLevel = 0;
        bool _focused = false;

        float _coolDown = 0.0f;

        GameObject _selectedIngredientTemplate = null;

        Camera _camera = null;

        public Pizza Pizza { get; set; }

        #region Callbacks

        private void Awake()
        {
            _doughLevel = -1;
            _maxDoughLump = doughLumps.Length - 1;

            _interactableItem.LookedAt.AddListener(this.OnLookedAt);

            _ingredientKeyMapping = Ingredients.MapKeysToIngredients();

            MapIngredientsToTemplates();
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (_focused)
            {
                this.HandleCheeseSauceKnead();
                this.HandleTopping();

            }

            _coolDown += Time.deltaTime;
        }

        #endregion

        #region Callback Helpers

        private void HandleCheeseSauceKnead()
        {
            if (Input.GetButtonDown(_cheeseButton) && this.Pizza != null)
            {
                this.Pizza.AddCheese();
            }
            else if (Input.GetButtonDown(_sauceButton) && this.Pizza != null)
            {
                this.Pizza.AddSauce();
            }
            else if (Input.GetButtonDown(_kneadButton) && this.Pizza == null)
            {
                this.KneadDough();
            }
        }

        private void HandleTopping()
        {
            HandleSelectedIngredient();

            if (_selectedIngredientTemplate != null && this.Pizza != null)
            {
                GameObject ingredient = Instantiate(_selectedIngredientTemplate);

                float radius = UnityEngine.Random.Range(0, 0.75f);
                float theta = UnityEngine.Random.Range(0, 360f);

                Vector3 randomPos = new Vector3(radius * Mathf.Cos(theta), 1, radius * Mathf.Sin(theta));

                ingredient.transform.position = this.Pizza.transform.position + randomPos;
            }
        }

        private void MapIngredientsToTemplates()
        {
            _ingredientTemplateMapping = new Dictionary<Ingredient, GameObject>();
            _ingredientTemplateMapping[Ingredient.Mushroom] = _mushroomTemplate;
            _ingredientTemplateMapping[Ingredient.Olive] = _oliveTemplate;
            _ingredientTemplateMapping[Ingredient.Sausage] = _sausageTemplate;
            _ingredientTemplateMapping[Ingredient.Pepperoni] = _pepperoniTemplate;
        }

        private void HandleSelectedIngredient()
        {
            foreach (KeyCode key in _ingredientKeyMapping.Keys)
            {
                if (Input.GetKeyDown(key) && this.Pizza != null)
                {
                    Ingredient chosenIngredient = _ingredientKeyMapping[key];
                    this.Pizza.AddTopping(chosenIngredient);
                    _selectedIngredientTemplate = _ingredientTemplateMapping[chosenIngredient];
                    return;
                }
            }

            _selectedIngredientTemplate = null;
        }

        #endregion

        #region Event Handlers

        private void OnLookedAt()
        {
            if (Input.GetButton(_triggerButton))
            {
                _interactableItem.Deactivate();
                _toppingStationPanel.Show();
                _focusZone.GrabCamera(Camera.main);
                _focused = true;
            }
        }

        /// <summary>
        /// Event handler for <c>ToppingStationPanel.Done</c>
        /// </summary>
        public void OnDone()
        {
            _interactableItem.Activate();
            _focusZone.ReleaseCamera();
            _toppingStationPanel.Hide();
            _focused = false;
        }

        /// <summary>
        /// Event handler for <c>ToppingStationPanel.NewPizza</c>
        /// </summary>
        public void OnNewPizza()
        {
            if (this.Pizza == null && _doughLevel == -1)
            {
                _doughLevel = 0;
                doughLumps[_doughLevel].SetActive(true);
            }   
        }

        private void KneadDough()
        {
            if (this.Pizza == null && _doughLevel >= 0)
            {
                doughLumps[_doughLevel].SetActive(false);

                if (_doughLevel == _maxDoughLump)
                {
                    this.SpawnPizza();
                    _doughLevel = -1;
                }
                else
                {
                    _doughLevel++;
                    doughLumps[_doughLevel].SetActive(true);
                }
            }
        }

        private void SpawnPizza()
        {
            GameObject newPizza = Instantiate(_pizzaTemplate);
            newPizza.transform.position = _newPizzaTransform.position;

            this.Pizza = newPizza.GetComponent<Pizza>();
        }

        /// <summary>
        /// Event handler for <c>ToppingStationPanel.MoveToInventory</c>
        /// </summary>
        public void OnMoveToInventory()
        {
            if (_inventoryService.Pizza == null && this.Pizza.IsOvenReady())
            {
                _inventoryService.Pizza = this.Pizza;
                this.Pizza.gameObject.SetActive(false);
                this.Pizza = null;
            }
        }

        #endregion
    }
}
