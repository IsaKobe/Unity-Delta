using Assets.Scripts;
using Assets.Scripts.Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
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

    private void Update()
    {
        foreach(MovingEnemy enemy in enemies)
        {
            if (enemy.done)
                continue;

            WaveData wave = waves[enemy.wave];
            Vector2 newPos = Vector2.MoveTowards(
                enemy.transform.position,
                wave.path[enemy.waypoint],
                enemy.speed * Time.deltaTime);

            if (Vector2.Distance(newPos, wave.path[enemy.waypoint]) < 0.01f)
            {
                enemy.waypoint++;
                if (enemy.waypoint >= wave.path.Count)
                    enemy.done = true;
            }
            else
                enemy.transform.position = newPos;
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
