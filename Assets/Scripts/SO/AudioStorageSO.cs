using AYellowpaper.SerializedCollections;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Storage", menuName = "Audio/Storage")]
public class AudioStorageSO : ScriptableObject
{
    [SerializeField] private SerializedDictionary<string, AudioClip> dictAudio;
    
#if UNITY_EDITOR
    //Utilities Method
    [ContextMenu("Find All Sound To Serialize")]
    public void AutoFindAll()
    {
        dictAudio.Clear();
        
        var guids = AssetDatabase.FindAssets("t:AudioClip", new[] { "Assets/Sound" });

        foreach (var guid in guids)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
            AudioClip data = AssetDatabase.LoadAssetAtPath<AudioClip>(assetPath);

            if (data != null)
            {
                dictAudio.Add(data.name, data);
            }
        }
    }
#endif
    public bool ContainAudio(string name) => dictAudio.ContainsKey(name);
    public AudioClip GetAudio(string name) => dictAudio[name];
}