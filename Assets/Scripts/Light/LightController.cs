using UnityEngine;

public class LightController : MonoBehaviour {
    public Light mainLight;
    public Light[] strobes1;
    public Light[] strobes2;

	public float interval = 0.05f;

    private bool _lightToggle = false;
    private float _lastStrobeTime = -1.0f;
    private bool _disco;

    public void MainLightMode() {
        _disco = false;
        ToggleLights(strobes1, false);
        ToggleLights(strobes2, false);
        mainLight.enabled = true;
    }

    public void DiscoMode() {
        _disco = true;
        mainLight.enabled = false;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        if (_disco) {
            if (Time.realtimeSinceStartup - _lastStrobeTime > interval) {
                ToggleLights(strobes1, _lightToggle);
                ToggleLights(strobes2, !_lightToggle);
                _lightToggle = !_lightToggle;
                _lastStrobeTime = Time.realtimeSinceStartup;
            }
        }
    }

    private void ToggleLights(Light[] lights, bool enabled) {
        foreach(Light s in lights) {
            s.enabled = enabled;
        }
    }
}