using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PizzaGame
{
    /// <summary>
    /// Central control hub for player
    /// </summary>
    public class Player : MonoBehaviour
    {
        [SerializeField]
        MouseLook _mouseLook = null;

        [SerializeField]
        Camera _camera = null;

        [SerializeField]
        PlayerMovement _playerMovement = null;

        [SerializeField]
        Inventory _inventory = null;

        /// <summary>
        /// Reference to player movement
        /// </summary>
        public PlayerMovement PlayerMovement => _playerMovement;

        /// <summary>
        /// Reference to mouse look
        /// </summary>
        public MouseLook MouseLook => _mouseLook;

        /// <summary>
        /// Reference to camera
        /// </summary>
        public Camera Camera => _camera;

        /// <summary>
        /// Reference to inventory
        /// </summary>
        public Inventory Inventory => _inventory;

        public static Lazy<Player> Shared = null;

        static Player()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Shared = new Lazy<Player>(() =>
            {
                return GameObject.FindObjectOfType<Player>();
            });
        }
    }
}