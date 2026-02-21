using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.Scripts
{
    public interface IDamagable
    {
        public float Health { get; set; }
        //public float health; 

        public void ReceiveDamage(float damage)
        {
            Health -= damage;
            if (Health <= 0)
                OnDeath();
        }

        public void OnDeath();
    }
}
