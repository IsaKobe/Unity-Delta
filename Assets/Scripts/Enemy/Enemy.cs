using Projectiles;
using System;
using UnityEngine;
using World;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : DamagableObject, IOnEnd<Enemy>, IScorable
{
    public int waypoint;
    public float Speed;
    
    [SerializeField] int score;
    int IScorable.Score { get => score; }

    public Rigidbody2D rb { get; private set; }
    public Action<Enemy> onEnd { get; set; }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

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
            (this as IScorable).AddScore();
        }
        onEnd(this);
        base.Die();
    }

}
