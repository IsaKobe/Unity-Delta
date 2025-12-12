using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Player
{
    public class PlayerProjectile : Projectile
    {
        public override void Move()
        {
            Vector3 newPos = new(transform.position.x, transform.position.y + speed, transform.position.z);
            rb.MovePosition(newPos);
        }
    }
}