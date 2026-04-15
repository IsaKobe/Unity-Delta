using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class MovingEnemy : Enemy
    {
        public float speed;
        public int waypoint;
        public bool done;
        public int wave;
        /*private void Update()
        {
            while (true)
            {
                transform.position = Vector2.MoveTowards(transform.position, , speed);
            }
        }*/
    }
}
