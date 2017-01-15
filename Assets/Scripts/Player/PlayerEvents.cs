using UnityEngine;

public class PlayerEvents : MonoBehaviour {
    public AnimationClip ithcingAnimation;
    public AnimationClip meowingAnimation;
    public AnimationClip idleSitAnimation;
    public AudioClip meowingSound;

    private GameController _gc;
    private Animation _animation;
    private AudioSource _audio;

    public bool _isMeowing;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        _gc = GameObject.Find("Game").GetComponent<GameController>();
        _animation = GetComponent<Animation>();
        _animation[ithcingAnimation.name].layer  = 1;
        _animation[ithcingAnimation.name].wrapMode = WrapMode.Once;
        _animation[meowingAnimation.name].layer  = 1;
        _animation[meowingAnimation.name].wrapMode = WrapMode.Once;
        _animation[idleSitAnimation.name].layer  = 1;
        _animation[idleSitAnimation.name].wrapMode = WrapMode.Once;
        _audio = GetComponent<AudioSource>();
    }
    
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        if (GameMode.Running.Equals(_gc.GetMode())) {
            AnimateEvent(); 
            PlaySounds();
        }
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

    private void PlaySounds() {
        if (Input.GetKey(KeyCode.M)) {
            if (!_isMeowing) {
                _audio.PlayOneShot(meowingSound);
            }
            _isMeowing = true;
        } else {
            _isMeowing = false;
        }
    }
}