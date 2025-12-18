using Player;
using System.Collections;
using UnityEngine;

public class AdvancedTurret : Turret
{
    [SerializeField] int initCooldown = 2;
    [SerializeField] VectorProjectile projectile;
    [SerializeField] float rotSpeed = 1;

    Vector3 direction;
    private void Update()
    {
        if (!activated)
            return;
        direction = WorldController.Ship.transform.position - transform.position;
        transform.up = Vector3.Lerp(transform.up, direction, rotSpeed * Time.deltaTime);
    }

    public override void Activate()
    {
        if (activated) return;
        activated = true;
        StartCoroutine(BeforeActivation());
    }

    IEnumerator BeforeActivation()
    {
        yield return new WaitForSeconds(initCooldown);
        StartCoroutine(ShootLoop());
    }


    protected override void OnShoot()
    {
        VectorProjectile proj = 
            Instantiate(
                projectile, 
                transform.position, 
                Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z), 
                transform.parent.parent.GetChild(2));
        proj.SetVector(transform.up);
    }
}
