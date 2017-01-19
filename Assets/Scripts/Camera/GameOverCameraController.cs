using UnityEngine;

public class GameOverCameraController : MonoBehaviour {
	public float rotationSpeed = 0.4f;
	public float jerk = 0.05f;

    private GameController _gc;

	private float _angle = 15.0f;
	private float _angleMax = 60.0f;
	private float _angleMin = -15.0f;

	private float _rotationDirection = 1.0f;
	private float _actualSpeed = 0.0f;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
        _gc = GameObject.Find("Game").GetComponent<GameController>();
	}

	/// <summary>
	/// LateUpdate is called every frame, if the Behaviour is enabled.
	/// It is called after all Update functions have been called.
	/// </summary>
	void LateUpdate() {
		CalculateSpeed();
		CalculateAngle();
		CalculateDirection();
		ApplyTransformation();
	}

	private void CalculateSpeed() {
		if (_actualSpeed < rotationSpeed) {
			_actualSpeed += jerk;
        }
	}

	private void CalculateAngle() {
		if (GameMode.GameOver.Equals(_gc.GetMode())) {
			_angle += _rotationDirection * _actualSpeed;
        }
	}

	private void CalculateDirection() {
		if ((_rotationDirection < 0 && _angle < _angleMin) 
				|| (_rotationDirection > 0 && _angle > _angleMax)) {
			_rotationDirection *= -1; 
			_actualSpeed = 0.0f;
		}
	}

	private void ApplyTransformation() {
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, _angle, transform.rotation.eulerAngles.z);
	}
}