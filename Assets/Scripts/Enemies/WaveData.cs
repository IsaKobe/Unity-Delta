using Assets.Scripts.Enemies;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable][CreateAssetMenu(fileName = "WaveData", menuName = "Enemies/WaveData", order = 1)]
    public class WaveData : ScriptableObject
    {
        public MovingEnemy enemyPrefab;
        public int enemyCount;
        public float delay;
        public float initialDelay;
        //public Vector2 spawnPoint;
        public List<Vector2> path;
    }

}
