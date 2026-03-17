using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour, IDamagable
    {
        [SerializeField] UIDocument doc;
        ProgressBar healthBar;

        [SerializeField] float maxHealth;
        public float MaxHealth => maxHealth;
        [SerializeField] float health;


        public float Health 
        { 
            get => health; 
            set 
            { 
                health = value;
                healthBar.value = health;
            }
        }

        private void Start()
        {
            healthBar = doc.rootVisualElement.Q<ProgressBar>("Health");
            Health = maxHealth;
            healthBar.highValue = maxHealth;
            healthBar.value = health;
        }

        public void OnDeath()
        {
            Debug.Log("Game Lost!");
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
