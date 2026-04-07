using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class ScoreManager : MonoBehaviour
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

        public static void AddScore(float _score)
        {
            return;
            instance.score += _score;
        }

        public static void EndGame(bool victory)
        {
            instance.StartEndGame(victory);
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
