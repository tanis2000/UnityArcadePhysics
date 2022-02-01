using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAction : MonoBehaviour
{
    public float Acceleration = 10;
    public float Friction = 0.82f;
    public float Damp = 2;
    public Vector3 Movement = Vector3.zero;
    
    private CapsuleCollider capsuleCollider;
    private Rigidbody body;
    private bool wasLeft;
    private bool wasRight;
    private bool isLeft;
    private bool isRight;

    private void OnEnable()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var dirX = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            dirX = -1f;
        } else if (Input.GetKey(KeyCode.D))
        {
            dirX = 1f;
        }

        var dirY = 0f;
        if (Input.GetKey(KeyCode.S))
        {
            dirY = -1f;
        } else if (Input.GetKey(KeyCode.W))
        {
            dirY = 1f;
        }

        Movement.x = dirX;
        Movement.y = 0;
        Movement.z = dirY;
    }

    private void FixedUpdate()
    {
        var d = Vector3.Dot(Movement, body.velocity);
        var multiplier = d < 0 ? Mathf.Abs(d): 1f;
        var drag = Friction * body.velocity * Time.fixedDeltaTime;
        if (drag.magnitude < 0.0005f)
        {
            drag = Vector3.zero;
        }
        body.velocity = drag;
        body.velocity += Acceleration * Movement * multiplier * Time.fixedDeltaTime;
    }
}
