using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public abstract class DamagableObject : MonoBehaviour
    {
        public float Health;

        public virtual void TakeDamage(float damage)
        {
            Health -= damage;
            if (Health <= 0)
                OnDeath();
        }

        protected abstract void OnDeath();
    }
}
