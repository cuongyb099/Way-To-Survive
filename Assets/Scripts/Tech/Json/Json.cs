using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEditor;

namespace Tech.Json
{
    public static class Json 
    {
        public static void SaveJson<T>(this T data, string path)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(path, json);
#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
        }
    
        public static async void SaveJsonAsync<T>(this T data, string path, Action saveDone = null)
        {
            await Task.Run(() =>
            {
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(path, json);
            });

            saveDone?.Invoke();
#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
        }
        
        
        
        public static void LoadJson<T>(string path, out T value)
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                T data = JsonConvert.DeserializeObject<T>(json);
                value = data;
                return;
            }

            value = default;
        }
    }
}