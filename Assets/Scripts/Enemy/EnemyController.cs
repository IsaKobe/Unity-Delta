using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] EnemyWave path;

    [SerializeField] List<Enemy> dormantEnemies;
    [SerializeField] List<Enemy> enemies;

    void OnDrawGizmos()
    {
        if (path == null)
            return;
        for (int i = 0; i < path.points.Count; i++)
        {
            Gizmos.DrawIcon(
                path.points[i], 
                "Triangle.png", 
                false, 
                i == 0 ? Color.red 
                    : i == path.points.Count-1 
                        ? Color.blue
                        : Color.white);
            if(i < path.points.Count-1)
                Gizmos.DrawLine(path.points[i], path.points[i+1]);
        }
    }

    IEnumerator Start()
    {
        dormantEnemies = new();
        for (int i = 0; i < path.count; i++)
            dormantEnemies.Add(Instantiate(path.prefab, path.points[0], Quaternion.identity, transform));
        yield return new WaitForSeconds(path.delay);

        enemies = new();
        while (dormantEnemies.Count > 0)
        {
            Enemy enemy = dormantEnemies[0];
            enemies.Add(enemy);
            enemy.onDeath = (en) => enemies.Remove(en);
            dormantEnemies.RemoveAt(0);
            yield return new WaitForSeconds(path.delay);
        }
    }

    void LateUpdate()
    {
        foreach (Enemy enemy in enemies)
        {
            Move(enemy);
        }
    }

    void Move(Enemy enemy)
    {
        int i = enemy.waypoint;
        enemy.transform.position = Vector2.MoveTowards(
            enemy.transform.position,
            path.points[i],
            enemy.Speed * Time.deltaTime);
        if (enemy.transform.position == path.points[i])
        {
            enemy.waypoint++;
            if (enemy.waypoint == path.points.Count)
                enemy.ForceDie();
        }
    }
}
