using UnityEngine;

public abstract class DamagableObject : MonoBehaviour
{
    [SerializeField] float health = 100;

    protected abstract void OnTriggerEnter2D(Collider2D collision);

    public virtual void Damage(float damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
            Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public void ForceDie()
        => Die();
}
