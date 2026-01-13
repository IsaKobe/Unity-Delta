using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEditor;
using UnityEngine;
using World;

public class EnemyController : MonoBehaviour, IOnEnd<EnemyController>, IPausable
{
    public EnemyWave path;

    [SerializeField] List<Enemy> dormantEnemies;
    [SerializeField] List<Enemy> enemies;

    bool spawnedAll;

    public Action<EnemyController> onEnd { get; set; }

    void OnDrawGizmosSelected()
    {
        if (path == null)
            return;
        for (int i = 0; i < path.points.Count; i++)
        {
            Gizmos.DrawIcon(
                path.points[i], 
                "Triangle.png", 
                true, 
                i == 0 ? Color.red 
                    : i == path.points.Count-1 
                        ? Color.blue
                        : Color.white);
        }
    }

    void Awake()
    {
        enabled = false;
    }

    IEnumerator Start()
    {
        dormantEnemies = new();
        yield return new PauseWaitUntil(path.startDelay);
        for (int i = 0; i < path.count; i++)
            dormantEnemies.Add(Instantiate(path.prefab, path.points[0], Quaternion.Euler(180,0,0), transform));
        

        enemies = new();
        spawnedAll = false;
        while (dormantEnemies.Count > 0)
        {
            Enemy enemy = dormantEnemies[0];
            enemies.Add(enemy);
            enemy.onEnd = (en) => enemies.Remove(en);
            dormantEnemies.RemoveAt(0);
            yield return new PauseWaitUntil(path.delay);
        }
        spawnedAll = true;
    }

    void LateUpdate()
    {
        for (int i = enemies.Count - 1; i > -1; i--)
            Move(enemies[i]);
        if (spawnedAll && enemies.Count == 0) 
        {
            onEnd?.Invoke(this);
            Stop();
        }
    }

    public void Stop()
    {
        StopAllCoroutines();
        enabled = false;
    }


    void Move(Enemy enemy)
    {
        int i = enemy.waypoint;
        enemy.rb.MovePosition(
            Vector2.MoveTowards(
                enemy.transform.position,
                path.points[i],
                enemy.Speed));
        if (enemy.transform.position == path.points[i])
        {
            enemy.waypoint++;
            if (enemy.waypoint == path.points.Count)
                enemy.ForceDie();
        }
    }

    public void OnPause()
    {
        enabled = false;
    }

    public void OnResume()
    {
        enabled = true;
    }
}
