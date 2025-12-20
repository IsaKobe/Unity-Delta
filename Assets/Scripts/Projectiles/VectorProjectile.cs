using Projectiles.Controllers.Data;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Projectiles
{
    public class VectorProjectile : Projectile
    {
        Vector3 direction;
        public override void Move()
        {
            Vector3 newPos = transform.position + (direction * speed);
            rb.MovePosition(newPos);
        }

        public override void SetStats(ProjData data)
        {
            base.SetStats(data);
            direction = (data as VecProjData).initialVector;
        }
    }
}