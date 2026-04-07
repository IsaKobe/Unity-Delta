using Assets.Scripts;
using Assets.Scripts.Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1);
    }*/


    public List<WaveData> waves;

    bool spawnerDone;
    int enemyCounter = 0;
    List<MovingEnemy> enemies;
    IEnumerator Start()
    {
        spawnerDone = false;
        enemies = new List<MovingEnemy>();
        for (int i = 0; i < waves.Count; i++)
        {
            WaveData waveData = waves[i];
            yield return new WaitForSeconds(waveData.initialDelay);
            for (int j = 0; j < waveData.enemyCount; j++)
            {
                enemyCounter++;
                MovingEnemy enemy = Instantiate(
                    waveData.enemyPrefab,
                    waveData.path[0], 
                    Quaternion.identity,
                    transform);
                enemy.OnDeath += OnEnemyDeath;
                enemy.wave = i;
                enemies.Add(enemy);
                yield return new WaitForSeconds(waveData.delay);
            }
        }
        spawnerDone = true;
    }

    private void FixedUpdate()
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            MovingEnemy enemy = enemies[i];
            enemy.Move(waves[enemy.wave].path);
        }
    }

    void OnEnemyDeath(Enemy enemy, bool awardScore)
    {
        if(awardScore)
            ScoreManager.AddScore(enemy.Score);

        enemies.Remove(enemy as MovingEnemy);
        enemyCounter--;

        if (spawnerDone && enemyCounter == 0)
        {
            ScoreManager.EndGame(true);
        }
    }
}
