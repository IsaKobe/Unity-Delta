using Projectiles;
using Projectiles.Controllers;
using Projectiles.Controllers.Data;
using System.Collections;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using World;

public class AdvancedTurret : Turret
{
    [SerializeField] int initCooldown = 2;
    [SerializeField] float rotSpeed = 1;

    [SerializeField] VecProjData projData;

    Vector3 direction;
    protected override void Awake()
    {
        base.Awake();
        projData = Instantiate(projData);
    }

    private void FixedUpdate()
    {
        if (!activated)
            return;
        Vector2 v = WorldController.Ship.transform.position - transform.position;
        float newRot = (Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg) - 90;
        newRot = Mathf.MoveTowardsAngle(rb.rotation, newRot, rotSpeed);
        rb.SetRotation(newRot);
    }

    public override void Activate()
    {
        if (activated) return;
        activated = true;
        StartCoroutine(BeforeActivation());
    }

    IEnumerator BeforeActivation()
    {
        yield return new PauseWaitUntil(initCooldown);
        StartCoroutine(ShootLoop());
    }


    protected override void OnShoot()
    {
        projData.initialVector = transform.up;

        VProjController.GetProjectile(projData, rb);
    }
}
