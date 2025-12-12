using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Player : DamagableObject
    {
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision == null) return;
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Damage(20);
                collision.gameObject.GetComponent<Enemy>().Damage(10);
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision == null) return;
            if (collision.gameObject.CompareTag("EnemyProjectile"))
            {
                Damage(20);
            }
        }

        protected override void Die()
        {
            base.Die();
            Debug.LogWarning("you lost");
        }

    }
}