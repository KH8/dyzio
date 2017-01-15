using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
    public float gravity = 10.0f;

    public float movementSpeed = 2.0f;
    public float rotaionSpeed = 2.0f;
    public float jumpSpeed = 2.0f;

    private Rigidbody _rb;
    private GameController _gc;
    private CharacterController _controller;

    private Vector3 _movementDirection = new Vector3();
    private Vector3 _rotationDirection = new Vector3();
    private float _speedModificator = 1.0f;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        Cursor.visible = false;
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        _rb = GetComponent<Rigidbody>();
        _gc = GameObject.Find("Game").GetComponent<GameController>();
        _controller = GetComponent<CharacterController>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        HandleControlls();

        if (!GameMode.Running.Equals(_gc.GetMode())) {
            ResetMovements();
        }

        ApplyGravity();
        ApplyMovement();
        ApplyRotation();
    }

    private void HandleControlls() {
        if (_controller.isGrounded) {
            HandleJump();
            HandleRunModes();
            HandleVerticalMovement();
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

    private void HandleVerticalMovement() {
        _movementDirection.z = Input.GetAxisRaw("Vertical");
    }

    private void HandleHorizontalRotation() {
        var axisRawHorizontal = Input.GetAxisRaw("Horizontal");
        var axisMouseX = Input.GetAxis("Mouse X");;

        if (Mathf.Abs(axisRawHorizontal) > Mathf.Abs(axisMouseX)) {
            _rotationDirection.y = axisRawHorizontal;
        } else {
            _rotationDirection.y = axisMouseX;
        }
    }

    private void ResetMovements() {
        _movementDirection.y = 0;
        _movementDirection.z = 0;
        _rotationDirection.y = 0;
    }

    private void ApplyGravity() {
        _movementDirection.y -= gravity * _rb.mass * Time.deltaTime;
    }

    private void ApplyMovement() {
        var movement = transform.TransformDirection(_movementDirection);
        _controller.Move(movement * movementSpeed * _speedModificator * Time.deltaTime);
    }

    private void ApplyRotation() {
        transform.Rotate(_rotationDirection * rotaionSpeed);
    }
}