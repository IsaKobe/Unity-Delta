using UnityEngine;
using UnityEngine.InputSystem;

public class newInput : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;

    InputAction move;
    InputAction fire;

    [SerializeField] float speed = 1;
    void Awake()
    {
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
        Vector3 newMove = move.ReadValue<Vector2>();
        //transform.localPosition += newMove * Time.deltaTime;
        transform.Translate(newMove * Time.deltaTime * speed);

        Debug.Log($"Move: {newMove * Time.deltaTime}");

        if(fire.triggered)
        {
            Debug.Log("Fire action triggered");
        }
    }
}
