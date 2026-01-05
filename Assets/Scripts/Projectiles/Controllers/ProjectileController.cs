using Projectiles.Controllers.Data;
using Player;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using World;
using Unity.Burst;

namespace Projectiles.Controllers
{
    public abstract class ProjectileController<T> : MonoBehaviour, IPausable where T : Projectile
    {
        Transform poolTransform;

        [SerializeField] List<T> pool;
#if UNITY_EDITOR
        [SerializeField] List<T> activeProjectiles;
#endif

#pragma warning disable UDR0001 // Domain Reload Analyzer
        static ProjectileController<T> instance;
#pragma warning restore UDR0001 // Domain Reload Analyzer


        private void Awake()
        {
            instance = this;

            GameObject poolObj = new("Pool");
            poolObj.transform.parent = GameObject.FindGameObjectWithTag("ProjectilePoolTrans").transform;
            poolTransform = poolObj.transform;

            pool = new();
            activeProjectiles = new();

            ((IPausable)this).Attach(WorldController.TimeController);
        }

        public static T GetProjectile(ProjData data, Transform transform, bool enable = true)
        {
            T proj = instance.GetProj();
            proj.transform.position = transform.position;
            proj.transform.rotation = transform.rotation;

            proj.transform.localScale = data.size;
            proj.GetComponent<CapsuleCollider2D>().size = data.colliderSize;

            proj.SetStats(data);
            proj.gameObject.SetActive(enable);
            return proj;
        }

        T GetProj()
        {
            T proj;
            if (pool.Count == 0)
                proj = CreateProjectile();
            else
            {
                proj = pool[pool.Count - 1];
                pool.RemoveAt(pool.Count - 1);
            }

#if UNITY_EDITOR
            activeProjectiles.Add(proj);
#endif
            proj.transform.parent = transform;
            return proj;
        }

        T CreateProjectile()
        {
            GameObject gObject = new("Projectile", typeof(SpriteRenderer));
            gObject.SetActive(false);

            Rigidbody2D rb = gObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;

            CapsuleCollider2D capsuleCollider = gObject.AddComponent<CapsuleCollider2D>();
            capsuleCollider.isTrigger = true;
            

            T projectile = gObject.AddComponent<T>();
            projectile.onEnd = ReturnToPool;
            return projectile;
        }

        void ReturnToPool(Projectile proj)
        {
            proj.transform.parent = poolTransform;
            proj.gameObject.SetActive(false);
            pool.Add(proj as T);
#if UNITY_EDITOR
            activeProjectiles.Remove(proj as T);
#endif
        }

        public void OnPause()
        {
            foreach (var item in activeProjectiles)
            {
                item.GetComponent<Rigidbody2D>().simulated = false;
            }
        }

        public void OnResume()
        {
            foreach (var item in activeProjectiles)
            {
                item.GetComponent<Rigidbody2D>().simulated = true;
            }
        }
    }
}
