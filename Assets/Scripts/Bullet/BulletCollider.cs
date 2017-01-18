using UnityEngine;

public class BulletCollider : MonoBehaviour {
    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other) {
        TouchableController tc = other.gameObject.GetComponent<TouchableController>();
        if (tc != null) {
            tc.TouchWithSound();
        }
    }
}