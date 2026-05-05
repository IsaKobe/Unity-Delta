using Assets.Scripts.Saves;
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

        [SerializeField] SaveControler controler;
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
            instance.StartEndGame(victory);
        }


        void StartEndGame(bool victory)
        {
            controler.Save(new LevelSave() { score = score });
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
