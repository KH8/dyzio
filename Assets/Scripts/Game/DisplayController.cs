using UnityEngine;

public class DisplayController : MonoBehaviour {
    public GameMode mode;
    private GameController _gc;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        _gc = GameObject.Find("Game").GetComponent<GameController>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        var enabled = _gc.GetMode().Equals(mode);
        var camera = GetComponent<Camera>();
        if (camera != null) {
            camera.enabled = enabled;
        }
        var canvas = GetComponent<Canvas>();
        if (canvas != null) {
            canvas.enabled = enabled;
        }
    }
}