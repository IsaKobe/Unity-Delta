using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{

    [CreateAssetMenu(fileName = "waveData2", menuName = "Enemies/Foo")]
    public class Foo : ScriptableObject
    {
        public GameObject enemy;
        public float delay;
        public int test;
    }
}
