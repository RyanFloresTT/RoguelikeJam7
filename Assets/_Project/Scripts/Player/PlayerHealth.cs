using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Utilities;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField, Range(5, 20)] int maxHealth;
    [SerializeField] float tickTimeInterval;
    [SerializeField] int healthDeduction;
    public int Health {  get; private set; }
    bool isLit;

    CountdownTimer deductionTimer;

    public static Action<int> OnHealthUpdated;


    void Awake() {
        LightManager.OnPlayerLit += Handle_PlayerLitStatus;

        deductionTimer = new(tickTimeInterval);
        deductionTimer.OnTimerStart += Handle_TimerStart;
        deductionTimer.OnTimerStop += Handle_TimerStop;
    }

    private void Update() {
        if(isLit)
        {
            deductionTimer.Tick(Time.deltaTime);
        }
    }

    private void Handle_TimerStart() {
        Health -= healthDeduction;
        OnHealthUpdated?.Invoke(Health);
        if (Health <= 0)
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        gameObject.SetActive(false);
    }

    private void Handle_TimerStop() {
        if (isLit)
        {
            deductionTimer.Start();
        }
    }

    void Start() {
        Health = maxHealth;
        OnHealthUpdated?.Invoke(Health);
    }

    void Handle_PlayerLitStatus(bool isInLight) {
        isLit = isInLight;
        if (isLit) {
            deductionTimer.Start();
        }
    }
}
