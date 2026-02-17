using System;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "WaveData", menuName = "Enemies/WaveData", order = 1)]
    class WaveData : ScriptableObject
    {
        public Enemy enemyPrefab;
        public int enemyCount;
        public float delay;
        public float initialDelay;
        public Vector3 spawnPoint;
    }

}
