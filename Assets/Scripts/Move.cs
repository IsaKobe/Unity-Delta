using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Move : MonoBehaviour
{
    Rigidbody rb;
    bool start;
    [SerializeField] float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        start = true;
    }
    private void FixedUpdate()
    {
        if (start)
        {
            rb.linearVelocity = transform.up * speed;
            start = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 newVel = Vector3.Reflect(-collision.relativeVelocity, collision.contacts[0].normal);
        rb.linearVelocity = newVel;
        float rot_z = Mathf.Atan2(newVel.x, newVel.z) * Mathf.Rad2Deg;

        transform.up = newVel;
        Debug.Log(-collision.relativeVelocity);
    }
}
