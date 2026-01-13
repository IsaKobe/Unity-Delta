using Projectiles.Controllers.Data;
using Projectiles.Controllers;
using UnityEngine;

public class RocketTurret : Turret
{
    [SerializeField] RocketProjData rocketData;

    [SerializeField] Transform projectileTrans;

    protected override void OnShoot()
    {
        RProjController.GetProjectile(rocketData, rb);
    }

    protected override void Awake()
    {
        base.Awake();
        rocketData = Instantiate(rocketData);
    }
}
