using UnityEngine;

public class MovableController : MonoBehaviour {
    public float minFreeFallSpeed = 0.04f;
    public float minLiftingSpeed = 0.005f;
    public float gravity = 10.0f;

    private Rigidbody _rb;
    private PointCounter _pc;

    private float _distance = float.NaN;
    private float _previousDistance = float.NaN;
    private float _speed = float.NaN;

    private State _state;
    private State _previousState;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {    
        _rb = GetComponent<Rigidbody>();
        _pc = GameObject.Find("Game").GetComponent<PointCounter>();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {
        HandleFreeFall();
    }

    private void HandleFreeFall() {
        ApplyAdditionalGravityForce();
        CalculateSpeed();
        ResolveFreeFallState();
        CalculateKinematicEnergy();
        _previousState = _state;
    }

    private void ApplyAdditionalGravityForce() {
        _rb.AddForce(-Vector3.up * gravity);
    }

    private void CalculateSpeed() {
        _distance = transform.position.y;
        _previousDistance = InitDistanceIfNan(_previousDistance);
        _speed = _distance - _previousDistance;
        _previousDistance = _distance;
    }

    private float InitDistanceIfNan(float distance) {
        return float.IsNaN(distance) ? _distance : distance;
    }

    private void ResolveFreeFallState() {
        if (-1 * _speed > minFreeFallSpeed) {
            _state = State.Falling;
        } else if (_speed > minLiftingSpeed) {
            _state = State.Lifting;
        } else {
            _state = State.Grounded;
        }
    }

    private void CalculateKinematicEnergy() {
        if (!_state.Equals(_previousState) && State.Falling.Equals(_previousState)) {
            var magnitude = transform.position.magnitude;
            var energy = _rb.mass * magnitude * magnitude * 0.5f;
            Debug.Log(gameObject.name + " - Hit with energy: " + energy);
            AddPoints(energy);
        }
    }

    private void AddPoints(float energy) {
            _pc.Add(energy);
    }

    private enum State {
        Lifting,
        Falling,
        Grounded
    }
}