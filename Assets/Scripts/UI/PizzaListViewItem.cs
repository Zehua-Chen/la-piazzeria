using UnityEngine;
using TMPro;
using PizzaGame.Pizzas;

namespace PizzaGame.UI
{
    /// <summary>
    /// Controls the list of pizza view
    /// </summary>
    public class PizzaListViewItem : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI _title = null;

        [SerializeField]
        PizzaEvent _selected = null;

        Pizza _pizza = null;

        public Pizza Pizza
        {
            get { return _pizza; }
            set
            {
                _pizza = value;
                _title.text = _pizza.ToString();
            }
        }

        public void OnClick()
        {
            _selected.Invoke(_pizza);
        }
    }
}