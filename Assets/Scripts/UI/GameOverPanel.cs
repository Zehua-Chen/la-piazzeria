using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;
using PizzaGame.Services;

namespace PizzaGame.UI
{
    /// <summary>
    /// Controls game over panel
    /// </summary>
    public class GameOverPanel : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField]
        GameObject _content = null;

        [SerializeField]
        TextMeshProUGUI _scoreLabel = null;

        [SerializeField]
        TextMeshProUGUI _maxScoreLabel = null;

        [Header("Navigation")]
        [SerializeField]
        string _startScene = "Start";

        [Header("Services")]
        [SerializeField]
        CountDownService _countDownService = null;

        [SerializeField]
        ScoreService _scoreService = null;

        [SerializeField]
        PlayerControlService _playerControlService = null;

        [SerializeField]
        PlayerDataService _playerDataService = null;

        private void Start()
        {
            _content.SetActive(false);
        }

        private void OnEnable()
        {
            _countDownService.Ticked.AddListener(this.OnTicked);
        }

        private void OnDisable()
        {
            _countDownService.Ticked.RemoveListener(this.OnTicked);
        }

        private void OnTicked(CountDown countDown)
        {
            if (countDown.Minutes == 0 && countDown.Seconds == 0)
            {
                _content.SetActive(true);
                _playerControlService.ReleaseControl();

                if (_scoreService.Score > _playerDataService.HighestOrderValue)
                {
                    _playerDataService.HighestOrderValue = _scoreService.Score;
                }

                _maxScoreLabel.text = $"Highest Score: {_playerDataService.HighestOrderValue}";
                _scoreLabel.text = $"Final Tips: {_scoreService.Score}";
            }
        }

        /// <summary>
        /// Called when the restart button has been clicked
        /// </summary>
        public void OnRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void OnExit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void OnStartMenu()
        {
            SceneManager.LoadScene(_startScene);
        }
    }
}