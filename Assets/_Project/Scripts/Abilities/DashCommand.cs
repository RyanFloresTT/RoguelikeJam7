using UnityEngine;

public class DashCommand : IAbilityCommand
{
    private Rigidbody2D rb;
    private float dashForce;
    private Vector2 fallbackDirection = Vector2.right;

    public DashCommand(Rigidbody2D rb, float dashForce)
    {
        this.rb = rb;
        this.dashForce = dashForce;
    }

    public void Execute()
    {
        Vector2 dashDirection = InputManager.Instance.GetMovementVectorNormalized();

        if (dashDirection != Vector2.zero) {
            dashDirection = rb.velocity.normalized;
        } else {
            dashDirection = fallbackDirection;
        }

        rb.velocity = Vector2.zero;
        rb.AddForce(rb.mass * dashDirection * dashForce, ForceMode2D.Impulse);

        Debug.Log($"Dashing in direction: {dashDirection} with force: {dashForce}");
    }
}
