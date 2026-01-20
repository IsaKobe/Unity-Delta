using UnityEngine;
using UnityEngine.InputSystem;

public class MyInput : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;

    protected InputAction move;
    protected InputAction fire;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Awake()
    {
        //transform.Translate(Vector3.up * 2);
        move = inputActions.FindAction("Move");
        fire = inputActions.FindAction("Fire");
    }
    private void OnEnable()
    {
        move.Enable();
        fire.Enable();
        move.performed += Move_performed;
        fire.started += Fire_started;
        fire.performed += Fire_performed;
        fire.canceled += Fire_cancled;
    }

    protected virtual void Fire_started(InputAction.CallbackContext obj)
    {
        Debug.Log("Fire action started");
    }
    protected virtual void Fire_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Fire action triggered");
    }
    protected virtual void Fire_cancled(InputAction.CallbackContext obj)
    {
        Debug.Log("Fire action canceled");
    }

    protected virtual void Move_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Move: " + move.ReadValue<Vector2>());
    }

    

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
        move.performed -= Move_performed;
        fire.performed -= Fire_performed;
    }

    // Update is called once per frame
    /*void Update()
    {
        Debug.Log("Fire:" + fire.ReadValue<float>());
        return;
        Debug.Log("Move: " + move.ReadValue<Vector2>());
        if(fire.triggered)
        {
            Debug.Log("Fire action triggered");
        }
    }*/
}
