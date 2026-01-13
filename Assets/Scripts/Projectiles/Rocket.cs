using Projectiles.Controllers.Data;
using Player;
using System;
using System.Collections;
using UnityEngine;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using World;

namespace Projectiles
{
    public class Rocket : Projectile
    {
        [SerializeField] float rotSpeed;
        [SerializeField] float projectileLife = 10;

        [SerializeField] Transform target;
        bool rotate = true;
        IEnumerator TimeOut()
        {
            yield return new PauseWaitUntil(projectileLife);
            rotate = false;
            //HandleDelete();
        }

        public override void Move()
        {
            Vector3 newPos = transform.position + (transform.up * speed);

            if (rotate)
            {
                if (!target)
                    FindTarget();
                if (target)
                {
                    float newRot = transform.rotation.eulerAngles.z;

                    Vector3 diff = target.position - transform.position;
                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90;
                    newRot = Mathf.MoveTowardsAngle(newRot, rot_z, rotSpeed);
                    
                    rb.MovePositionAndRotation(newPos, newRot);
                }
            }
            rb.MovePosition(newPos);
        }


        public override void SetStats(ProjData data)
        {
            base.SetStats(data);
            projectileLife = (data as RocketProjData).timeToLive;
            rotSpeed = (data as RocketProjData).rotSpeed;

            if(!data.isPlayerProj)
                target = WorldController.Ship.transform;
            else
            {
                FindTarget();
            }
        }

        void FindTarget()
        {
            List<Transform> targets = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Select(q => q.transform).ToList();
            targets.AddRange(FindObjectsByType<Turret>(FindObjectsSortMode.None).Select(q => q.transform));

            float min = float.MaxValue;
            int minI = -1;
            for (int i = 0; i < targets.Count; i++)
            {
                float dis = Vector2.Distance(targets[i].position, transform.position);
                if (dis < min)
                {
                    min = dis;
                    minI = i;
                }
            }
            if(minI > -1)
                target = targets[minI];
        }

        private void OnEnable()
        {
            StartCoroutine(TimeOut());
        }

    }
}