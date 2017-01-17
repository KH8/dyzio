using UnityEngine;

public class TouchCollider : MonoBehaviour {
    /// <summary>
    /// OnControllerColliderHit is called when the controller hits a
    /// collider while performing a Move.
    /// </summary>
    /// <param name="hit">The ControllerColliderHit data associated with this collision.</param>
    void OnControllerColliderHit(ControllerColliderHit hit) {
        TouchableController tc = hit.gameObject.GetComponent<TouchableController>();
        if (tc != null) {
            tc.TouchWithSound();
        }
    }
}