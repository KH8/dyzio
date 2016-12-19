using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
    public Rigidbody rb;
    public CharacterController controller;

    public float gravity = 10.0f;

    public float movementSpeed = 2.0f;
    public float rotaionSpeed = 10.0f;
    public float jumpSpeed = 2.0f;

    private Vector3 _movementDirection = new Vector3();
    private Vector3 _rotationDirection = new Vector3();
    private float _speedModificator = 1.0f;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        HandleControlls();

        ApplyGravity();
        ApplyMovement();
        ApplyRotation();
    }

    private void HandleControlls() {
        if (controller.isGrounded) {
            HandleJump();
            HandleRunModes();
            HandleHorizontalMovement();
            HandleHorizontalRotation();
        }
    }

    private void HandleJump() {
        if (Input.GetKey(KeyCode.Space)) {
            _movementDirection.y = jumpSpeed;
        }
    }

    private void HandleRunModes() {
        _speedModificator = 1.0f;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            _speedModificator = 1.5f;
        }
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) {
            _speedModificator = 0.2f;
        }
    }

    private void HandleHorizontalMovement() {
        _movementDirection.z = Input.GetAxisRaw("Vertical");
    }

    private void HandleHorizontalRotation() {
        _rotationDirection.y = Input.GetAxisRaw("Horizontal");
    }

    private void ApplyGravity() {
        _movementDirection.y -= gravity * rb.mass * Time.deltaTime;
    }

    private void ApplyMovement() {
        var movement = transform.TransformDirection(_movementDirection);
        controller.Move(movement * movementSpeed * _speedModificator * Time.deltaTime);
    }

    private void ApplyRotation() {
        transform.Rotate(_rotationDirection * rotaionSpeed);
    }
}