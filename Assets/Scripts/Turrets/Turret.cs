using Projectiles;
using System;
using System.Collections;
using UnityEngine;
using World;

[RequireComponent(typeof(DownMover))]
public abstract class Turret : DamagableObject, IOnEnd<Turret>, IPausable, IScorable
{
    [SerializeField] int cooldown;
    [SerializeField] protected bool activated = false;
    [SerializeField] int score;
    int IScorable.Score { get => score; }

    protected Rigidbody2D rb;

    public Action<Turret> onEnd { get; set; }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            Damage(collision.gameObject.GetComponent<Projectile>().GetDamage());
        }
    }

    public virtual void Activate()
    {
        if(activated == false)
        {
            activated = true;
            StartCoroutine(ShootLoop());
        }
    }

    public virtual void Deactivate()
    {
        if (activated == true)
        {
            activated = false;
            StopAllCoroutines();
        }
    }

    protected IEnumerator ShootLoop()
    {
        while (true)
        {
            yield return new PauseWaitUntil(cooldown);
            OnShoot();
        }
    }

    protected abstract void OnShoot();



    protected override void Die(bool naturalDeath = true)
    {
        Deactivate();
        if (naturalDeath)
        {
            (this as IScorable).AddScore();
        }
        onEnd(this);
        ((IPausable)this).Detach(WorldController.TimeController);
        base.Die(naturalDeath);
    }

    public void OnPause()
    {
        enabled = false;
    }

    public void OnResume()
    {
        enabled = true;
    }
}
