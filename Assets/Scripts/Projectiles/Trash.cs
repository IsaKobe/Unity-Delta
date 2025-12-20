using Projectiles;
using System.Collections;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] bool enableObjects;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;

        
        if (collision.gameObject.CompareTag("Turret"))
        {
            if (enableObjects)
            {
                collision.GetComponent<Turret>().Activate();
            }
            else
                collision.GetComponent<Turret>().Deactivate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.gameObject.CompareTag("PlayerProjectile") ||
            collision.gameObject.CompareTag("EnemyProjectile"))
        {
            if (collision.TryGetComponent<Rocket>(out _))
                return;
            collision.GetComponent<Projectile>().HandleDelete();
        }
        else if (collision.gameObject.CompareTag("Turret"))
        {
            if (!enableObjects)
                collision.gameObject.GetComponent<Turret>().ForceDie();
        }

    }

}
