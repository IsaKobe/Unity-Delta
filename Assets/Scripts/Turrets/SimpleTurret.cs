using Player;
using UnityEngine;

public class SimpleTurret : Turret
{
    [SerializeField] SimpleProjectile projectile;
    protected override void OnShoot()
    {
        SimpleProjectile simpleProjectile = Instantiate(projectile, transform.position, Quaternion.identity, transform.parent.parent.GetChild(2));
        //simpleProjectile.Speed *= -1;
    }
    private void Awake()
    {
        Activate();
    }
}
