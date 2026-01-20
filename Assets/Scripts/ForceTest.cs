using UnityEngine;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(Rigidbody))]
public class ForceTest : MyInput
{
    bool jump = false;
    bool canJump = false;
    Rigidbody rb;
    public ForceMode forceMode = ForceMode.Impulse;
    Vector3 movement;
    [SerializeField] float speed = 0.1f;

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (canJump && fire.triggered)
            jump = true;
        movement = new Vector3(move.ReadValue<Vector2>().x, 0, move.ReadValue<Vector2>().y);
    }

    private void FixedUpdate()
    {
        if(rb.linearVelocity.y == 0)
            canJump = true;
        if(jump)
        {
            rb.AddForce(Vector2.up * 10, forceMode);
            jump = false;
            canJump = false;
        }
        if(movement != Vector3.zero)
        {
            rb.MovePosition(rb.position + movement * speed);// new Vector3(movement.x, 0, movement.y) * 10, forceMode);
        }
    }
}
