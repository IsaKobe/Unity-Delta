using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Unity.Properties;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour, IDamagable, INotifyBindablePropertyChanged
    {
        [SerializeField] float maxHealth;
        public float MaxHealth => maxHealth;
        [SerializeField] float health;

        public event EventHandler<BindablePropertyChangedEventArgs> propertyChanged;

        [CreateProperty]
        public float Health 
        { 
            get => health; 
            set 
            { 
                health = value;
                propertyChanged?.Invoke(this, new (nameof(Health)));
            }
        }


        public void OnDeath()
        {
            Debug.Log("Game Lost!");
            ScoreManager.EndGame(false);
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
