using Projectiles.Controllers.Data;
using Projectiles.Controllers;
using UnityEngine;

public class RocketTurret : Turret
{
    [SerializeField] RocketProjData rocketData;

    [SerializeField] Transform projectileTrans;

    protected override void OnShoot()
    {
        RProjController.GetProjectile(rocketData, transform);
    }

    protected override void Awake()
    {
        //rocketData = Instantiate(rocketData);
    }
}
