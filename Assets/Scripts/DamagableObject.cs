using UnityEngine;

public abstract class DamagableObject : MonoBehaviour
{
    [SerializeField] protected float health = 100;

    protected abstract void OnTriggerEnter2D(Collider2D collision);

    public virtual void Damage(float damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
            Die();
    }

    protected virtual void Die(bool naturalDeath = true)
    {
        Destroy(gameObject);
    }

    public void ForceDie()
        => Die(false);
}
