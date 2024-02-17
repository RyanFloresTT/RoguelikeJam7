using UnityEngine.Rendering.Universal;
using UnityEngine;
using KBCore.Refs;

public class LightSource : MonoBehaviour {
    LightManager lightManager;
    [SerializeField, Self] Light2D lightSource;

    private void Start()
    {
        lightManager = LightManager.Instance;
        Debug.Log(lightManager + " " +  lightSource);
        lightManager.RegisterLight(lightSource);
    }
    void OnEnable()
    {
        if (lightManager != null) {
            lightManager.RegisterLight(lightSource);
        }
    }

    void OnDisable() {
        lightManager.UnregisterLight(lightSource);
    }
}
