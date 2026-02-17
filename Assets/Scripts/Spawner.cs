using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    [SerializeField] List<WaveData> waves;
    IEnumerator Start()
    {
        foreach (var waveData in waves)
        {
            yield return new WaitForSeconds(waveData.initialDelay);
            for (int i = 0; i < waveData.enemyCount; i++)
            {
                Instantiate(waveData.enemyPrefab, waveData.spawnPoint, Quaternion.identity);
                yield return new WaitForSeconds(waveData.delay);
            }
        }
    }
}
