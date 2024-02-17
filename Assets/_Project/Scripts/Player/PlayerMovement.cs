using KBCore.Refs;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Self] Rigidbody2D rb;
    [SerializeField, Range(0 , 10)] float speed = 5;
    PlayerInputActions inputActions;

    void Awake() {
        inputActions = new();
    }

    void OnEnable() {
        inputActions.Player.Enable();
    }

    void OnDisable() {
        inputActions.Player.Disable();
        
    }
    private void Update()
    {
        CheckForLightExposure();
    }
    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement() {
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * GetMoveDirection());
    }

    Vector2 GetMoveDirection() => inputActions.Player.Move.ReadValue<Vector2>();

    void CheckForLightExposure() {
        bool isInLight = LightManager.Instance.IsPlayerInLight(transform);
        if (isInLight) {
            Debug.Log("Player is in light!");
        } else {
            Debug.Log("Player is hidden in shadows.");
        }
    }
}
