using UnityEngine;
using System.Collections;

public class ThirdPersonController : MonoBehaviour {
    public Rigidbody rb;
    public CharacterController controller;

    private bool isControllable;

    private Vector3 movement = new Vector3();

    private float gravity = 10.0f;

    private CollisionFlags collisionFlags; 

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
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");

        ApplyGravity();
        ApplyMovement();
    }

    private void ApplyGravity() {
        if (IsGrounded()) {
            movement.y = 0.0f;
        } else {
            movement.y -= gravity * rb.mass * Time.deltaTime;
        }
    }

    private bool IsGrounded() {
        return (collisionFlags & CollisionFlags.CollidedBelow) != 0;
    }

    private void ApplyMovement() {
        collisionFlags = controller.Move(movement * Time.deltaTime);
    }
}