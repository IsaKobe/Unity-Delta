using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MapMovement : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D rigidBody2D;
    Vector2 pos;
    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        pos = transform.position;
    }

    void FixedUpdate()
    {
        pos.y -= speed;
        rigidBody2D.MovePosition(pos);
    }
}
