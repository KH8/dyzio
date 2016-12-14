﻿using UnityEngine;
using System.Collections;

public class BallCollider : MonoBehaviour {
	public float pushPower = 2.0f;

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
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
