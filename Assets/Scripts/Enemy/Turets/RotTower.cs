using Assets.Scripts.Player;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RotTower : Tower
{
    [SerializeField] float rotSpeed;

    GameObject player;
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        Vector2 diff = player.transform.position - transform.position;

        float destRot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        destRot = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.z, destRot, rotSpeed);

        transform.rotation = Quaternion.Euler(0, 0, destRot);
    }
}
