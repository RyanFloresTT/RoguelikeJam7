using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    public static LightManager Instance; 
    List<Light2D> lights = new();
    [SerializeField] LayerMask obstructionLayer;
    public static Action<bool> OnPlayerLit;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {  
            Instance = this;
        }
    }

    public void RegisterLight(Light2D newLight) {
        if (!lights.Contains(newLight)) {
            lights.Add(newLight);
        }
    }

    public void UnregisterLight(Light2D oldLight) {
        if (lights.Contains(oldLight)) {
            lights.Remove(oldLight);
        }
    }

    public bool IsPlayerInLight(Transform player) {
        Vector3 directionToPlayer;
        float distanceToPlayer;
        foreach (var light in lights) {
            directionToPlayer = player.position - light.transform.position;
            distanceToPlayer = directionToPlayer.magnitude;
            if (distanceToPlayer <= light.pointLightOuterRadius) {
                RaycastHit2D hit = Physics2D.Raycast(light.transform.position, directionToPlayer.normalized, distanceToPlayer, obstructionLayer);
                if (!hit.collider || hit.transform == player) {
                    OnPlayerLit?.Invoke(true);
                    return true;
                }
            }
        }
        OnPlayerLit?.Invoke(false);
        return false;
    }
}
