using UnityEngine;

public class TouchableController : MonoBehaviour {
    public float touchThreshold = 1.0f;

    private TouchCounter _tc;

    private bool _touched;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        _tc = GameObject.Find("Game").GetComponent<TouchCounter>();
    }

    public void Touch() {
        if (!_touched && transform.position.magnitude > touchThreshold) {
            _touched = true;
            _tc.Add(1);
            Debug.Log(this.name + " - touched");
        }
    }

    /// <summary>
    /// OnCollisionStay is called once per frame for every collider/rigidbody
    /// that is touching rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionStay(Collision other) {
        TouchableController tc = other.gameObject.GetComponent<TouchableController>();
        if (tc != null && _touched) {
            tc.Touch();
        }
    }
}