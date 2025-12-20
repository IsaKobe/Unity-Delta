using Projectiles;
using System;
using System.Collections;
using UnityEngine;

public abstract class Turret : DamagableObject, IOnEnd<Turret>, IPausable
{
    [SerializeField] int cooldown;
    [SerializeField] protected bool activated = false;

    public Action<Turret> onEnd { get; set; }

    protected abstract void Awake();

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
            yield return new WaitForSeconds(cooldown);
            OnShoot();
        }
    }

    protected abstract void OnShoot();



    protected override void Die(bool naturalDeath = true)
    {
        Deactivate();
        onEnd(this);
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
