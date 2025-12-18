using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

class VectorProjectile : Projectile
{
    Vector3 direction;
    public override void Move()
    {
        Vector3 newPos = transform.position + (direction * speed);
        rb.MovePosition(newPos);
    }

    public void SetVector(Vector2 vec)
    {
        direction = vec.normalized;
        enabled = true;
    }
}
