using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipInput : MonoBehaviour
{
    [SerializeField] ObjectPool projectilePool;
    [SerializeField] InputActionAsset actionAsset;
    Rigidbody2D rb;

    InputAction move;
    InputAction fire;
    InputAction special;

    Vector2 moveBy;
    [SerializeField] float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        move = actionAsset.FindAction("Move");
        fire = actionAsset.FindAction("Fire");
        special = actionAsset.FindAction("Special");
    }

    private void OnEnable()
    {
        actionAsset.Enable();
    }

    private void OnDisable()
    {
        actionAsset.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        moveBy = move.ReadValue<Vector2>();
        if (fire.triggered)
        {
            GameObject proj = projectilePool.GetObject(transform.position);
            proj.GetComponent<Projectile>().Init();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveBy * speed);
    }
}
