using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Player
{
    public class SimpleProjectile : Projectile
    {
        //public float Speed { get => speed; set => speed = value; }
        public override void Move()
        {
            Vector3 newPos = new(transform.position.x, transform.position.y + speed, transform.position.z);
            rb.MovePosition(newPos);
        }
    }
}