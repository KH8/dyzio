using UnityEngine;

public class CameraController : MonoBehaviour {
	public GameObject target;
	public float smoothTime = 0.5F;

	private Vector3 _offset;
	private Vector3 _desiredPosition;
	private float _desiredAngle;
	private float _overheadAngle = 15.0f;
	private float _overheadAngleMax = 45.0f;
	private float _overheadAngleMin = -15.0f;

    private Vector3 _refVelocity = Vector3.zero;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		_offset = target.transform.position - transform.position;
	}

	/// <summary>
	/// LateUpdate is called every frame, if the Behaviour is enabled.
	/// It is called after all Update functions have been called.
	/// </summary>
	void LateUpdate() {
    	Quaternion rotation = getTargetRotation();
		CalculatePosition(rotation);
		CalculateOverheadAngle();
		ApplyTransformation();
	}

	private Quaternion getTargetRotation() {
		_desiredAngle = target.transform.eulerAngles.y;
    	return Quaternion.Euler(0, _desiredAngle, 0);
	}

	private void CalculatePosition(Quaternion rotation) {
		_desiredPosition = target.transform.position - (rotation * _offset);
	}

	private void CalculateOverheadAngle() {
		_overheadAngle += Input.GetAxis("Mouse Y"); 
		_overheadAngle = Mathf.Max(_overheadAngle, _overheadAngleMin);
		_overheadAngle = Mathf.Min(_overheadAngle, _overheadAngleMax);
	}

	private void ApplyTransformation() {
		transform.position = Vector3.SmoothDamp(transform.position, _desiredPosition, ref _refVelocity, smoothTime);
		transform.LookAt(target.transform);
		transform.Rotate(-1 * _overheadAngle, 0, 0);
	}
}