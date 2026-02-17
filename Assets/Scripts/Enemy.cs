using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]int Health = 3;

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
