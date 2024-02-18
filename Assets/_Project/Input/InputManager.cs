using System;
using UnityEngine;
public class InputManager : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private static InputManager instance = null;
    public static InputManager Instance
    {
        get { return instance; }
    }

    public static Action OnPlayerDash = delegate { };

    private void Awake()
    {
        CheckInstance();
        playerInputActions = new PlayerInputActions();
    }
    private void Start()
    {
        playerInputActions.Player.Dash.performed += Handle_Dash;
    }
    private void CheckInstance()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    } 

    private void Handle_Dash(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OnPlayerDash?.Invoke();
    }

    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        return playerInputActions.Player.Move.ReadValue<Vector2>().normalized;
    }
}