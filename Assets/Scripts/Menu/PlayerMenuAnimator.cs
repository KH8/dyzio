using UnityEngine;

public class PlayerMenuAnimator : MonoBehaviour {
    public AnimationClip idleAnimation;
    public AnimationClip ithcingAnimation;
    public AnimationClip meowingAnimation;
    public AnimationClip idleSitAnimation;
    public int animationInterval = 100;
    public float brokenRunFixTime = 0.1f;

    private Animation _animation;

    private AnimationClip[] _animations;
    public int _counter = 0;
    public int _index = 0;
    private float _brokenRunTimeStamp = -1000.0f;

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
        _counter++;
        if (_counter % animationInterval == 0) {
            AnimateNext();
        }
        if (!_animation.isPlaying) {
            if (_index == 2) {
                _brokenRunTimeStamp = Time.realtimeSinceStartup;
                _index = -1;
            }
            PlayIdleAnimation(brokenRunFixTime);
        }
    }

    void AnimateNext() {
        Debug.Log("Next animation");
        _index = Random.Range(0, _animations.Length);
        var randomAnimation = _animations[_index];
        _animation.CrossFade(randomAnimation.name);
    }

    private void PlayIdleAnimation(float time) {
        if (Time.realtimeSinceStartup - _brokenRunTimeStamp < time) {
            _animation.CrossFade(meowingAnimation.name);
        } else {
            _animation.CrossFade(idleAnimation.name);
        }
    }
}