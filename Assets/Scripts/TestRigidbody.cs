using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;

public class TestRigidbody : MonoBehaviour
{
    Rigidbody body;
    [SerializeField] InputActionAsset asset;

    InputAction move;
    InputAction look;

    Vector2 rotation;
    Vector3 movePos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody>();
        move = asset.FindAction("Move");
        look = asset.FindAction("Rot");

        move.Enable();
        look.Enable();  
    }

    // Update is called once per frame
    void Update()
    {
        rotation = look.ReadValue<Vector2>();
        movePos = move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        movePos.z = movePos.y;
        movePos.y = 0;
        Vector3 v = Quaternion.AngleAxis(body.rotation.eulerAngles.y, Vector3.up) * movePos;
        Debug.Log($"{v}, {body.rotation.eulerAngles.y}");
        //float y = body.rotation.eulerAngles.y * Mathf.Rad2Deg;
        /*Vector3 v = new();

        float sin = Mathf.Sin(y * Mathf.Deg2Rad);
        float cos = Mathf.Cos(y * Mathf.Deg2Rad);

        float tx = movePos.x;
        float ty = movePos.y;
        v.x = (cos * tx) - (sin * ty);
        v.z = (sin * tx) + (cos * ty);*/

        Quaternion rot = Quaternion.Euler(
            0,
            body.rotation.eulerAngles.y + rotation.x,
            0);

        body.Move(body.position + v, rot);
    }
}
