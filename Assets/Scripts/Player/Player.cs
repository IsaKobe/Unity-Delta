using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour, IDamagable, IUpdatable
    {
        public event EventHandler<BindablePropertyChangedEventArgs> propertyChanged;

        [SerializeField] float maxHealth;
        public float MaxHealth => maxHealth;
        [SerializeField] float health;

        
        [CreateProperty]
        public float Health 
        { 
            get => health; 
            set 
            { 
                health = value; 
                //UIUpdate(nameof(Health)); 
            } 
        }

        private void Start()
        {
            Health = maxHealth;
        }

        public void OnDeath()
        {
            Debug.Log("Game Lost!");
        }

        public void UIUpdate(string property = "")
        {
            propertyChanged?.Invoke(this, new(property));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                ((IDamagable)this).ReceiveDamage(20);
                collision.gameObject.GetComponent<IDamagable>().ReceiveDamage(20);
            }
        }
    }
}
