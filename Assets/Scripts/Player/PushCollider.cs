using UnityEngine;

public class PushCollider : MonoBehaviour {
	public float pushPower = 2.0f;

	/// <summary>
	/// OnControllerColliderHit is called when the controller hits a
	/// collider while performing a Move.
	/// </summary>
	/// <param name="hit">The ControllerColliderHit data associated with this collision.</param>
	void OnControllerColliderHit(ControllerColliderHit hit) {
		var body = hit.collider.attachedRigidbody;
		if (body == null || body.isKinematic) {
			return;
		} else if (hit.moveDirection.y < -0.3) {
			return;
		} else {
			var pushDir = new Vector3 (hit.moveDirection.x, 0, hit.moveDirection.z);
			body.velocity = pushDir * pushPower;
		}
	}
}
