using System;
using UnityEngine;

public class Enemy : DamagableObject, IBeforeDeathSlave<Enemy>
{
    public int waypoint;
    public float Speed;

    public Action<Enemy> onDeath { get; set; }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            Damage(collision.gameObject.GetComponent<Projectile>().GetDamage());
        }
    }



    protected override void Die()
    {
        onDeath(this);
        base.Die();
        // ADD score
    }

}
