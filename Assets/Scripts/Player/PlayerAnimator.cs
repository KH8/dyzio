using UnityEngine;

public class PlayerAnimator : MonoBehaviour {
    public CharacterController controller;
    public PlayerController pc;

    public AnimationClip idleAnimation;
    public AnimationClip walkAnimation;
    public AnimationClip runAnimation;
    public AnimationClip jumpPoseAnimation;

    public float walkMaxAnimationSpeed = 0.75f;
    public float trotMaxAnimationSpeed = 1.0f;
    public float runMaxAnimationSpeed = 1.0f;
    public float jumpAnimationSpeed = 1.15f;
    public float brokenRunFixTime = 0.5f;

    private Animation _animation;
    private CharacterState _state = CharacterState.Idle;
    private CharacterState _previousState = CharacterState.Idle;
    private float _brokenRunTimeStamp = -1000.0f;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        controller = GetComponent<CharacterController>();
        pc = GetComponent<PlayerController>();
        _animation = GetComponent<Animation>();
    }
    
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        ResolveCharacterState();
        ResolveAnimation();
        
    }

    private void ResolveCharacterState() {
        _state = CharacterState.Idle;
        if (!controller.isGrounded) {
            _state = CharacterState.Jumping;
        }
        var magnitude = controller.velocity.magnitude;
        if (magnitude > 0.0) {
            _state = CharacterState.Walking;
            if (magnitude < 0.9 * pc.movementSpeed) {
                _state = CharacterState.Trotting;
            } else if (magnitude > 1.1 * pc.movementSpeed) {
                _state = CharacterState.Running;
            }
        }
        HandleBrokenRunState();
        _previousState = _state;
    }

    private void HandleBrokenRunState() {
        if (CharacterState.Running.Equals(_previousState) && CharacterState.Idle.Equals(_state)) {
            _brokenRunTimeStamp = Time.realtimeSinceStartup;
        }
        KeepWalkingStateForFixTime(brokenRunFixTime);
    }

    private void KeepWalkingStateForFixTime(float time) {
        if (Time.realtimeSinceStartup - _brokenRunTimeStamp < time) {
            _state = CharacterState.Walking;
        }
    }

    private void ResolveAnimation() {
        switch(_state) {
            case CharacterState.Walking: 
                _animation[walkAnimation.name].speed = Mathf.Clamp(controller.velocity.magnitude, 0.0f, walkMaxAnimationSpeed);
                _animation.CrossFade(walkAnimation.name);
                break;
            case CharacterState.Trotting: 
                _animation[walkAnimation.name].speed = Mathf.Clamp(controller.velocity.magnitude, 0.0f, trotMaxAnimationSpeed);
                _animation.CrossFade(walkAnimation.name);
                break;
            case CharacterState.Running: 
                _animation[runAnimation.name].speed = Mathf.Clamp(controller.velocity.magnitude, 0.0f, runMaxAnimationSpeed);
                _animation.CrossFade(runAnimation.name);
                break;
            case CharacterState.Jumping: 
                _animation[jumpPoseAnimation.name].speed = jumpAnimationSpeed;
                _animation[jumpPoseAnimation.name].wrapMode = WrapMode.ClampForever;
                _animation.CrossFade(jumpPoseAnimation.name);
                break;
            case CharacterState.Idle:
                _animation.CrossFade(idleAnimation.name);
                break;
        }        
    }

    private enum CharacterState {
        Idle,
        Walking,
        Trotting,
        Running,
        Jumping
    }
}