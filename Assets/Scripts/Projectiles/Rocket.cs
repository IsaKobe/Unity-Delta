using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

internal class Rocket : Projectile
{
    [SerializeField] float rotSpeed;
    [SerializeField] float projectileLife = 10;
    [SerializeField] Transform target;

    private void Awake()
    {
        target = WorldController.Ship.transform;
        StartCoroutine(TimeOut());
    }

    IEnumerator TimeOut()
    {
        yield return new WaitForSeconds(projectileLife);
        HandleDelete();
    }

    public override void Move()
    {
        Vector3 newPos = transform.position + (transform.up * speed);

        Vector3 diff = target.position - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90;
        //float currentRot = transform.rotation.eulerAngles.z - 90;
        //rot_z = Mathf.Clamp(rot_z, currentRot - rotSpeed, currentRot + rotSpeed);//(0, rot_z, rotSpeed);

        rb.MovePositionAndRotation(newPos, Quaternion.Euler(0,0, rot_z));
    }
}
