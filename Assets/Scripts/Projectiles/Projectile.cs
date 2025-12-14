using NUnit.Framework;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour, IOnEnd<Projectile>
{
    [SerializeField] float damage;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected float speed;
    public Action<Projectile> onEnd { get; set; }

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
        if (onEnd != null)
            onEnd?.Invoke(this);
        else
            Destroy(gameObject);
    }
}
