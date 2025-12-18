using UnityEngine;

public class RocketTurret : Turret
{
    [SerializeField] Rocket rocket;

    [SerializeField] Transform projectileTrans;

    protected override void OnShoot()
    {
        Rocket simpleProjectile = Instantiate(rocket, transform.position, Quaternion.identity, projectileTrans);
    }
}
