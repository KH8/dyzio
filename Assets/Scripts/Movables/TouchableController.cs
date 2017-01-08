using UnityEngine;

public class TouchableController : MonoBehaviour {
    public float touchThreshold = 1.0f;

    private TouchCounter _tc;

    private bool _touchable = true;
    private bool _touched;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        _tc = GameObject.Find("Game").GetComponent<TouchCounter>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        if (_touched && transform.position.magnitude > touchThreshold) {
            _touched = false;
            _touchable = false;
            _tc.Increment();
        }
    }

    public void Touch() {
        if (_touchable) {
            _touched = true;
            Debug.Log(this.name + " - touched");
        }
    }
}