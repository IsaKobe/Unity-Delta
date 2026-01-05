using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace World
{
    public class TimeController : MonoBehaviour
    {
        public Action onPause;
        public Action onResume;

        public static float timeElapsed;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        static void Initialize()
        {
            timeElapsed = 0f;
        }

        public void StartTimer()
        {
            timeElapsed = 0f;
            StartCoroutine(Tick());
            Time.timeScale = 1;
        }

        public void ResumeTimer()
        {
            StartCoroutine(Tick());
            onResume?.Invoke();
        }

        public void StopTimer()
        {
            StopAllCoroutines();
            onPause?.Invoke();
        }

        IEnumerator Tick()
        {
            while (true)
            {
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
    }
}
