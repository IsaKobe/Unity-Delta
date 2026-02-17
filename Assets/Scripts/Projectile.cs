using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField]float speed = 1;
    [SerializeField]int damage = 1;
    Rigidbody2D rigidbody2;

    public Action<GameObject> returnToPool;

    public void Init()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        rigidbody2.gravityScale = 0;
        rigidbody2.AddForce(new Vector2(0, 1) * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            returnToPool(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            returnToPool(gameObject);
        }
    }
}
