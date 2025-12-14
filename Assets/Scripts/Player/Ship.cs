using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Properties;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Player
{
    public class Ship : DamagableObject, IOnEnd<Ship>, IUpdatable
    {
        public void UIUpdate(string property = "")
        {
            propertyChanged?.Invoke(this, new(property));
        }

        [SerializeField] float maxHealth;

        [CreateProperty]
        public float Health { get => health; set => health = value; }

        public event EventHandler<BindablePropertyChangedEventArgs> propertyChanged;

        public Action<Ship> onEnd { get; set; }

        private void Start()
        {
            health = maxHealth;
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision == null) return;
            if (collision.gameObject.CompareTag("EnemyProjectile"))
            {
                Damage(20);
            }
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.GetComponent<DamagableObject>().ForceDie();
                Damage(20);
            }
        }

        public override void Damage(float damage)
        {
            base.Damage(damage);
            UIUpdate(nameof(Health));
        }

        protected override void Die(bool naturalDeath = true)
        {
            onEnd(this);
            base.Die();
            Debug.LogWarning("you lost");
        }
    }
}