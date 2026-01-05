using Player.PlayerInputs;
using Projectiles;
using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{
    public class Ship : DamagableObject, IOnEnd<Ship>, IUpdatable
    {
        public void UIUpdate(string property = "")
        {
            propertyChanged?.Invoke(this, new(property));
        }

        public float maxHealth;

        [CreateProperty]
        public float Health
        { 
            get => health;
            set 
            { 
                health = value;
                UIUpdate(nameof(Health));
            } 
        }

        public event EventHandler<BindablePropertyChangedEventArgs> propertyChanged;

        public PlayerInput Input;


        public Action<Ship> onEnd { get; set; }

        GameObject lastCollision;

        private void Start()
        {
            health = maxHealth;
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision == null && collision.gameObject == lastCollision) return;
            if (collision.gameObject.CompareTag("EnemyProjectile"))
            {
                Damage(collision.GetComponent<Projectile>().GetDamage());
            }
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = collision.GetComponent<Enemy>();
                enemy.ForceDie();
                Damage(20);
            }
            lastCollision = collision.gameObject;
        }

        private void FixedUpdate()
        {
            lastCollision = null;
        }

        public override void Damage(float damage)
        {
            base.Damage(damage);
            UIUpdate(nameof(Health));
        }

        protected override void Die(bool naturalDeath = true)
        {
            if (enabled)
            {
                enabled = false;
                onEnd(this);
                base.Die();
                Debug.LogWarning("you lost");
            }
        }
    }
}