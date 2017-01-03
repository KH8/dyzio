using UnityEngine;

public class PlayerEvents : MonoBehaviour {
    public AnimationClip ithcingAnimation;
    public AnimationClip meowingAnimation;
    public AnimationClip idleSitAnimation;

    private Animation _animation;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        _animation = GetComponent<Animation>();
        _animation[ithcingAnimation.name].layer  = 1;
        _animation[ithcingAnimation.name].wrapMode = WrapMode.Once;
        _animation[meowingAnimation.name].layer  = 1;
        _animation[meowingAnimation.name].wrapMode = WrapMode.Once;
        _animation[idleSitAnimation.name].layer  = 1;
        _animation[idleSitAnimation.name].wrapMode = WrapMode.Once;
    }
    
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        AnimateEvent();   
    }

    private void AnimateEvent() {      
        if (Input.GetKey(KeyCode.C)) {
            _animation.Play(ithcingAnimation.name);
        }
        if (Input.GetKey(KeyCode.M)) {
            _animation.Play(meowingAnimation.name);
        }
        if (Input.GetKey(KeyCode.Z)) {
            _animation.Play(idleSitAnimation.name);
        }
    }
}