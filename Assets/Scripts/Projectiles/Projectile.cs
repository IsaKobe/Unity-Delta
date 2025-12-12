using NUnit.Framework;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour, IBeforeDeathSlave<Projectile>
{
    [SerializeField] float damage;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected float speed;
    public Action<Projectile> onDeath { get; set; }

    private void FixedUpdate()
    {
        Move();
    }

    public abstract void Move();

    public float GetDamage()
    {
        HandleDelete();
        return damage;
    }

    public void HandleDelete()
    {
        if (onDeath != null)
            onDeath(this);
        else
            Destroy(gameObject);
    }
}
