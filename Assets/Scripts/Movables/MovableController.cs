using UnityEngine;

public class MovableController : MonoBehaviour {
    public Rigidbody rb;
    public float minFreeFallSpeed = 0.05f;
    public float minLiftingSpeed = 0.005f;

    private float _distance = float.NaN;
    private float _previousDistance = float.NaN;
    private float _speed = float.NaN;

    private State _state;
    private State _previousState;

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {
        HandleFreeFall();
    }

    private void HandleFreeFall() {
        CalculateSpeed();
        ResolveFreeFallState();
        CalculateKinematicEnergy();
        _previousState = _state;
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
        if (!_state.Equals(_previousState)) {
            Debug.Log("State: " + _state);
        }
    }

    private void CalculateKinematicEnergy() {
        if (!_state.Equals(_previousState) && State.Falling.Equals(_previousState)) {
            var energy = rb.mass * _speed * _speed;
            energy *= 0.5f;
            Debug.Log("Hit with energy: " + energy);
            AddPoints(energy);
        }
    }

    private void AddPoints(float energy) {
            var pc = GameObject.Find("Game").GetComponent<PointCounter>();
            pc.Add(energy);
    }

    private enum State {
        Lifting,
        Falling,
        Grounded
    }
}