#if UNITY_STANDALONE
using System.IO;
using UnityEditor;
using UnityEngine;
using PizzaGame.Services;

[CustomEditor(typeof(PlayerDataService))]
public class PlayerDataServiceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var service = (PlayerDataService)this.target;

        if (GUILayout.Button("Delete File"))
        {
            if (File.Exists(service.FullPath))
            {
                File.Delete(service.FullPath);
                Debug.LogFormat("{0} deleted", service.FullPath);
            }
        }
    }
}
#endif
