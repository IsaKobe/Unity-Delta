using System.Collections;
using UnityEngine;

public class Tower : Enemy
{
    //[SerializeField] Projectile prefab;
    [SerializeField] float cooldown;
    [SerializeField] ObjectPool projectilePool;

    protected virtual void Start()
    {
        StartCoroutine(Fire());
    }

    protected virtual IEnumerator Fire()
    {
        WaitForSeconds delay = new WaitForSeconds(cooldown);
        while (true)
        {
            yield return delay;
            projectilePool.GetObject(transform.position);
        }
    }
}
