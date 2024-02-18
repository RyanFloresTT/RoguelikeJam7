using UnityEngine;
using System.Collections;
using Utilities;
using KBCore.Refs;
using UnityEngine.InputSystem;
using System;

public class AbilityInvoker : MonoBehaviour
{
    [SerializeField, Self] Rigidbody2D playerRigidbody;
    [SerializeField] float dashForce = 10f;
    [SerializeField, Child] SpriteRenderer playerSpriteRenderer;

    CountdownTimer dashCooldownTimer;
    Color originalColor;
    DashCommand dashCommand;


    void Start()
    {
        SetUpTimer();
        SetUpCommand();
        ListenToInput();
    }

    private void ListenToInput()
    {
        InputManager.OnPlayerDash += Handle_Dash;
    }

    private void SetUpCommand()
    {
        originalColor = playerSpriteRenderer.color;

        dashCommand = new DashCommand(playerRigidbody, dashForce);
    }

    private void SetUpTimer()
    {
        dashCooldownTimer = new CountdownTimer(2f);

        dashCooldownTimer.OnTimerStart += Handle_Cooldown_Start;
        dashCooldownTimer.OnTimerStop += Handle_Cooldown_Stop;
    }

    private void Handle_Cooldown_Start()
    {
        Debug.Log("Cooldown started");
        dashCommand.Execute();
    }

    private void Handle_Cooldown_Stop()
    {
        Debug.Log("Cooldown completed");
        StartCoroutine(FlashIndicator());
    }

    private void Handle_Dash()
    {
        Debug.Log("Dash action performed");
        if (!dashCooldownTimer.IsRunning) {
            dashCooldownTimer.Start();
        }
    }

    void Update() {
        dashCooldownTimer.Tick(Time.deltaTime);
    }

    IEnumerator FlashIndicator()
    {
        playerSpriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.5f);
        playerSpriteRenderer.color = originalColor;
    }
}
