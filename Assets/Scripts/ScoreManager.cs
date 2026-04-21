using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    public class ScoreManager : MonoBehaviour, INotifyBindablePropertyChanged
    {
        static ScoreManager instance;

        private void Awake()
        {
            if(instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        public static void ClearInstance()
        {
            instance = null;
        }

        float score;

        public event EventHandler<BindablePropertyChangedEventArgs> propertyChanged;

        [CreateProperty]
        public float Score 
        { 
            get => score; 
            set 
            { 
                score = value;
                propertyChanged?.Invoke(this, new(nameof(Score)));
            } 
        }
        public static void AddScore(float _score)
        {
            instance.Score += _score;
        }

        public static void EndGame(bool victory)
        {
            instance.SaveData();

            instance.StartEndGame(victory);
        }

        struct SaveD
        {
            public float score;
            public override string ToString()
            {
                return $"score: {score}";
            }
        }

        [ContextMenu("SaveData")]
        void SaveData()
        {
            var jsonSerializer = JsonSerializer.Create();
            string path = Path.Join(Application.persistentDataPath, "save.json");

            SaveD data = new SaveD() { score = score };
            StreamWriter writer = null;
            JsonTextWriter jsonWriter = null;
            try
            {
                writer = new StreamWriter(path);
                jsonWriter = new(writer);
                jsonSerializer.Serialize(jsonWriter, data);
                jsonWriter.Close();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                if (writer != null)
                    writer.Dispose();
                if (jsonWriter != null && jsonWriter.WriteState != WriteState.Closed)
                    jsonWriter.Close();
            }
            Debug.Log(path);
            Debug.Log(data);
        }

        [ContextMenu("LoadData")]
        void LoadData()
        {
            var s = JsonSerializer.Create();
            string path = Path.Join(Application.persistentDataPath, "save.json");
            StreamReader reader = null;
            JsonTextReader jsonReader = null;
            try
            {
                reader = new StreamReader(path);
                jsonReader = new(reader);

                Debug.Log(s.Deserialize<SaveD>(jsonReader));
                jsonReader.Close();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                if (reader != null)
                    reader.Dispose();
            }
            reader.Close();
        }

        void StartEndGame(bool victory)
        {
            StartCoroutine(LoadEnd(victory));
        }

        IEnumerator LoadEnd(bool victory) 
        {
            yield break;
            yield return SceneManager.LoadSceneAsync("End", LoadSceneMode.Additive);
            GameObject obj = GameObject.FindGameObjectWithTag("EndScreen");


            yield return SceneManager.UnloadSceneAsync(gameObject.scene);
        }
    }
}
