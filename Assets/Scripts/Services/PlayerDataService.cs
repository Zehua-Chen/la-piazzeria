#if UNITY_STANDALONE
using System.IO;
#endif

using UnityEngine;
using PizzaGame.Orders;

namespace PizzaGame.Services
{
    [CreateAssetMenu(menuName = "Services/Player Data Service")]
    public class PlayerDataService : ScriptableObject
    {
        struct PlayerData
        {
            public OrderValue HighestOrderValue;

            public static PlayerData Default => new PlayerData()
            {
                HighestOrderValue = new OrderValue()
            };
        }

        [SerializeField]
        string _fileName = "PlayerData.json";

        [SerializeField]
        bool _prettyPrint = true;

#if UNITY_STANDALONE
        public string FullPath => Path.Combine(Application.persistentDataPath, _fileName);
#endif

        PlayerData _playerData = new PlayerData();

        public OrderValue HighestOrderValue
        {
            get => _playerData.HighestOrderValue;
            set => _playerData.HighestOrderValue = value;
        }

        private void OnEnable()
        {
            _playerData = PlayerData.Default;

#if UNITY_STANDALONE
            if (File.Exists(this.FullPath))
            {
                string fileContent = File.ReadAllText(this.FullPath);
                _playerData = JsonUtility.FromJson<PlayerData>(fileContent);
            }
#endif
        }

        private void OnDisable()
        {
#if UNITY_STANDALONE
            string fileContent = JsonUtility.ToJson(_playerData, _prettyPrint);

            FileStream fileStream = null;

            if (!File.Exists(this.FullPath))
            {
                fileStream = File.Create(this.FullPath);
            }
            else
            {
                fileStream = File.OpenWrite(this.FullPath);
            }

            StreamWriter writer = new StreamWriter(fileStream);

            writer.Write(fileContent);

            writer.Dispose();
            fileStream.Dispose();
#endif
        }

        public void DeleteFile()
        {
#if UNITY_STANDALONE
            File.Delete(this.FullPath);
#endif
        }

        public void Reset()
        {
            _playerData = PlayerData.Default;
        }
    }
}