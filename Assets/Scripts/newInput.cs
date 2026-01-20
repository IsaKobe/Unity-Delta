using UnityEngine;
using UnityEngine.InputSystem;

public class newInput : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] Rigidbody rb;
    InputAction move;
    InputAction fire;
    Vector3 newMove;
    bool jump = false;

    [SerializeField] float speed = 1;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        move = inputActions.FindAction("Move");
        fire = inputActions.FindAction("Fire");
    }

    private void OnEnable()
    {
        move.Enable();
        fire.Enable();
    }
    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        newMove = move.ReadValue<Vector2>();
        newMove.z = newMove.y;
        newMove.y = 0;
        if(fire.triggered)
            jump = true;
        /*
        //transform.localPosition += newMove * Time.deltaTime;
        transform.Translate(newMove * Time.deltaTime * speed);

        Debug.Log($"Move: {newMove * Time.deltaTime}");

        if(fire.triggered)
        {
            Debug.Log("Fire action triggered");
        }*/
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + newMove * speed);
        if(jump)
        {
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
            jump = false;
        }
    }
}
