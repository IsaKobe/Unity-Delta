using Assets.Scripts;
using System.Collections;
using UnityEngine;

public class Spawner2 : MonoBehaviour
{
    WaveData2 wave;
    void Start()
    {
        //gameObject.SetActive(false);
        wave = Resources.Load<WaveData2>("waveData2");

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        
        for (int i = 0; i < wave.enemyCount; i++)
        {
            yield return new WaitForSeconds(wave.delay);
            Instantiate(wave.enemy, wave.spawnPoint, Quaternion.identity);
        }
    }
}
