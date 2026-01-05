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
        projData = Instantiate(projData);
    }

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
        yield return new PauseWaitUntil(initCooldown);
        StartCoroutine(ShootLoop());
    }


    protected override void OnShoot()
    {
        projData.initialVector = transform.up;

        VProjController.GetProjectile(projData, transform);
    }
}
