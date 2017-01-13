using UnityEngine;

public class PlayerMenuAnimator : MonoBehaviour {
    public AnimationClip idleAnimation;
    public AnimationClip ithcingAnimation;
    public AnimationClip meowingAnimation;
    public AnimationClip idleSitAnimation;
    public int animationInterval = 100;

    private Animation _animation;

    private AnimationClip[] _animations;
    public int counter = 0;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        _animation = GetComponent<Animation>();
        _animations = new [] {ithcingAnimation, meowingAnimation, idleSitAnimation};
    }
    
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        counter++;
        if (counter % animationInterval == 0) {
            AnimateNext();
        }
        if (!_animation.isPlaying) {
            _animation.CrossFade(idleAnimation.name);
        }
    }

    void AnimateNext() {
        Debug.Log("Next animation");
        var randomIndex = Random.Range(0, _animations.Length);
        var randomAnimation = _animations[randomIndex];
        _animation.CrossFade(randomAnimation.name);
    }
}