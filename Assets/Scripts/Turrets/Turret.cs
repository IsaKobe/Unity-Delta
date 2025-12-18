using System;
using System.Collections;
using UnityEngine;

public abstract class Turret : DamagableObject
{
    [SerializeField] int cooldown;
    [SerializeField] protected bool activated = false;

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
}
