using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using PizzaGame.Services;

namespace PizzaGame.UI
{
    /// <summary>
    /// Controls the pause menu
    /// </summary>
    public class PauseMenu : MonoBehaviour
    {
        /// <summary>
        /// If the game is currently paused
        /// </summary>
        public bool IsPaused = false;

        [SerializeField]
        PauseService pauseService = null;

        /// <summary>
        /// UI of pause menu
        /// </summary>
        public GameObject pauseMenuUI;

        /// <summary>
        /// References to player control service
        /// </summary>
        public PlayerControlService playerControllerService;

        private void Start()
        {
            pauseMenuUI.SetActive(false);
        }

        private void OnEnable()
        {
            pauseService.Paused += this.OnPause;
            pauseService.Resumed += this.OnResume;
        }

        private void OnDisable()
        {
            pauseService.Paused -= this.OnPause;
            pauseService.Resumed -= this.OnResume;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (IsPaused)
                {
                    pauseService.Resume();
                }
                else
                {
                    pauseService.Pause();
                }
            }
        }

        private void OnPause(object sender, EventArgs e)
        {
            Time.timeScale = 0f;
            IsPaused = true;

            pauseMenuUI.SetActive(true);
            playerControllerService.ReleaseControl();
        }

        public void OnResume(object sender, EventArgs e)
        {
            Time.timeScale = 1f;
            IsPaused = false;

            pauseMenuUI.SetActive(false);
            playerControllerService.RetainControl();

        }

        /// <summary>
        /// Event handler for resume button
        /// </summary>
        public void OnResumeClick()
        {
            pauseService.Resume();
        }

        /// <summary>
        /// Event handler to load scenes
        /// </summary>
        /// <param name="scene">name of the scene</param>
        public void LoadScene(string scene)
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(scene);
        }

        public void OnExitClick()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

}
