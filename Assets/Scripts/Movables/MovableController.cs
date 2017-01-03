using UnityEngine;

public class MovableController : MonoBehaviour {
    public float minDistanceLift = 0.2f;
    public float minDistanceChange = 0.05f;

    private float _distance = float.NaN;
    private float _previousDistance = float.NaN;
    private float _minDistance = float.NaN;
    private float _maxDistance = float.NaN;
    private bool _isGrounded;
    private bool _wasGrounded;

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {
        HandleFreeFall();
    }

    private void HandleFreeFall() {
        ResolveDistances();
        ResolveFreeFallState();
    }

    private void ResolveDistances() {
        _distance = transform.position.y;
        _previousDistance = InitDistanceIfNan(_previousDistance);

        _minDistance = InitDistanceIfNan(_minDistance);
        _maxDistance = InitDistanceIfNan(_maxDistance);

        _minDistance = Mathf.Min(_minDistance, _distance);
        _maxDistance = Mathf.Max(_maxDistance, _distance);

        Debug.Log("dist: " + _distance + ", pre: " + _previousDistance + ", min: " + _minDistance + ", max: " + _maxDistance);
    }

    private float InitDistanceIfNan(float distance) {
        return float.IsNaN(distance) ? _distance : distance;
    }

    private void ResolveFreeFallState() {
        if (_distance - _minDistance > minDistanceLift) {
            _isGrounded = false;
            if (_wasGrounded) Debug.Log("Object is lifted");
        }
        if (Mathf.Abs(_previousDistance - _distance) < minDistanceChange) {
            _isGrounded = true;
            if (!_wasGrounded) Debug.Log("Object is grounded");
        }
        _wasGrounded = _isGrounded;
        _previousDistance = _distance;
    }
}