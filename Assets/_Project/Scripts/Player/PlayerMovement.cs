using KBCore.Refs;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInputActions;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Self] Rigidbody2D rb;
    [SerializeField, Range(0 , 10)] float speed = 5;

    private void Update()
    {
        CheckForLightExposure();
    }
    void FixedUpdate()
    {
        if (GetMovementDir() != Vector2.zero) {
            HandleMovement();
        }
    }

    void HandleMovement() {
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * GetMovementDir());
    }

    Vector2 GetMovementDir() => InputManager.Instance.GetMovementVectorNormalized();
    void CheckForLightExposure() => LightManager.Instance.IsPlayerInLight(transform);
}
