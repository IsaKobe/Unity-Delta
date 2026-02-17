using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "waveData2", menuName = "Enemies/WaveData 2")]
    public class WaveData2 : ScriptableObject
    {
        public GameObject enemy;
        public float delay;
        public int enemyCount;
        public Vector2 spawnPoint;
    }

}
