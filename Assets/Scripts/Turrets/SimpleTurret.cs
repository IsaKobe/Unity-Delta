using Player;
using Projectiles;
using Projectiles.Controllers;
using Projectiles.Controllers.Data;
using UnityEngine;

public class SimpleTurret : Turret
{
    [SerializeField] SimpleProjData data;
    protected override void OnShoot()
    {
        SimpleProjectile simpleProjectile = SProjController.GetProjectile(data, transform);
    }
    protected override void Awake()
    {
        data = Instantiate(data);
        Activate();
    }
}
