using Projectiles;
using System;
using UnityEngine;
using World;

public class Enemy : DamagableObject, IOnEnd<Enemy>
{
    public int waypoint;
    public float Speed;
    [SerializeField] int score;
    
    public Action<Enemy> onEnd { get; set; }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            Damage(collision.gameObject.GetComponent<Projectile>().GetDamage());
        }
    }



    protected override void Die(bool naturalDeath = true)
    {
        if (naturalDeath)
        {
            WorldController.AddScore(score);
        }
        onEnd(this);
        base.Die();
    }

}
