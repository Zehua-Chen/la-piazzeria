using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PizzaGame.Extensions;

namespace PizzaGame.Services
{
    /// <summary>
    /// Base class for services with states
    /// </summary>
    public abstract class PerSceneStatefulService : ScriptableObject
    {
        protected virtual void OnEnable()
        {
            Instances.TryAdd(this.GetInstanceID(), this);
        }

        protected virtual void OnDisable()
        {
            Instances.TryRemove(this.GetInstanceID());
        }

        /// <summary>
        /// Called when the active scene changes
        /// </summary>
        public virtual void Reset()
        {
        }

        public static Dictionary<int, PerSceneStatefulService> Instances = new Dictionary<int, PerSceneStatefulService>();

        static PerSceneStatefulService()
        {
            SceneManager.activeSceneChanged += (a, b) =>
            {
                foreach (var pair in Instances)
                {
                    pair.Value.Reset();
                }
            };
        }
    }
}