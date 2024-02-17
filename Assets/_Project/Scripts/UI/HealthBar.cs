using KBCore.Refs;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField, Self] Slider healthSlider;
    void Start()
    {
        PlayerHealth.OnHealthUpdated += Handle_HealthUpdated;
    }

    private void Handle_HealthUpdated(int health)
    {
        healthSlider.value = health;
    }
}
