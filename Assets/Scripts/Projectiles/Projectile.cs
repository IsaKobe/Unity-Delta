using Projectiles.Controllers.Data;
using NUnit.Framework;
using System;
using UnityEngine;

namespace Projectiles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Projectile : MonoBehaviour, IOnEnd<Projectile>
    {
        [SerializeField] float damage;
        [SerializeField] protected float speed;
        
        protected Rigidbody2D rb;
        public Action<Projectile> onEnd { get; set; }
        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        public abstract void Move();

        public float GetDamage()
        {
            HandleDelete();
            return damage;
        }

        public void HandleDelete()
        {
            StopAllCoroutines();
            rb.linearVelocityX = 0;
            rb.linearVelocityY = 0;
            if (onEnd != null)
                onEnd?.Invoke(this);
            else
                Destroy(gameObject);
        }

        public virtual void SetStats(ProjData data)
        {
            damage = data.damage;
            speed = data.speed;
            if (data.isPlayerProj)
                gameObject.tag = "PlayerProjectile";
            else
                gameObject.tag = "EnemyProjectile";

            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = data.sprite;
            spriteRenderer.color = data.color;
        }
    }
}