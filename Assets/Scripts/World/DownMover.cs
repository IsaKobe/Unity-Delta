using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using World;

[RequireComponent(typeof(Rigidbody2D))]
public class DownMover : MonoBehaviour
{
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Vector2 vector = transform.position;
        vector.y -= MapMovement.Speed;
        rb.MovePosition(vector);
    }
}
