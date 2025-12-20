using Projectiles.Controllers.Data;
using UnityEngine;

namespace Projectiles
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SimpleProjectile : Projectile
    {
        public override void Move()
        {
            Vector3 newPos = new(transform.position.x, transform.position.y + speed, transform.position.z);
            rb.MovePosition(newPos);
        }

        public override void SetStats(ProjData data)
        {
            base.SetStats(data);
        }
    }
}