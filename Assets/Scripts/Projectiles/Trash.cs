using UnityEngine;

public class Trash : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;

        if (collision.gameObject.CompareTag("PlayerProjectile") || 
            collision.gameObject.CompareTag("EnemyProjectile"))
        {
            collision.GetComponent<Projectile>().HandleDelete();
        }
    }
}
