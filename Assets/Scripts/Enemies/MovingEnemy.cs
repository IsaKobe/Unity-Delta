using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class MovingEnemy : Enemy
    {
        public float speed;
        public int currentWaypoint = 1;
        public int wave;
        Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Move(List<Vector2> map)
        {
            Vector2 vector2 = map[currentWaypoint];
            rb.MovePosition(Vector2.MoveTowards(rb.position, vector2, speed));
            if (Vector2.Distance(rb.position, vector2) < 0.1f)
            {
                currentWaypoint++;
                if(currentWaypoint == map.Count)
                    DestroySelf();
            }
        }
    }
}
