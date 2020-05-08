using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using PizzaGame.Services;

namespace PizzaGame.UI
{
    /// <summary>
    /// Controls records panel
    /// </summary>
    public class RecordsPanel : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI _scoreLabel = null;

        [SerializeField]
        PlayerDataService _playerDataService = null;

        private void Start()
        {
            _scoreLabel.text = $"Highest Score: {_playerDataService.HighestOrderValue}";
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void OnClean()
        {
            _playerDataService.Reset();
            _scoreLabel.text = $"Highest Score: {_playerDataService.HighestOrderValue}";
        }
    }
}