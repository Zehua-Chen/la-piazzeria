using UnityEngine;
using UnityEngine.SceneManagement;

namespace PizzaGame.UI
{
    /// <summary>
    /// Provide start menu
    /// </summary>
    public class StartMenu : MonoBehaviour
    {
        /// <summary>
        /// Event handler for navigation buttons
        /// </summary>
        /// <param name="scenename"></param>
        public void changemenuscene(string scenename)
        {
            SceneManager.LoadScene(scenename);
        }
    }
}
