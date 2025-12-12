using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "wave", menuName ="Wave data")]
public class EnemyWave : ScriptableObject
{
    public List<Vector3> points;
    public Enemy prefab;
    public int count = 10;
    public float startDelay = 0;
    public float delay = 1;
}