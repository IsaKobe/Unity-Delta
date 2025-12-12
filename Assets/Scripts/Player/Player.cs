using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Player : DamagableObject
    {
        [SerializeField] int maxHealth;

        [SerializeField] TMP_Text m_Text;
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
                Damage(20);
                collision.gameObject.GetComponent<Enemy>().ForceDie();
            }
        }

        public override void Damage(float damage)
        {
            base.Damage(damage);
            m_Text.text = $"{health}/{maxHealth}";
        }

        protected override void Die()
        {
            base.Die();
            Debug.LogWarning("you lost");
        }

    }
}