using UnityEngine;

public class TouchableController : MonoBehaviour {
    public float touchThreshold = 1.0f;
    public float touchSoundThreshold = 2.0f;
    public float touchSoundVolumeFactor = 9.0f;

    public AudioClip[] touchSounds;

    private AudioSource _audio;

    private TouchCounter _tc;

    private bool _touched;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        _tc = GameObject.Find("Game").GetComponent<TouchCounter>();
        _audio = GetComponent<AudioSource>();
    }

    public void Touch() {
        if (!_touched && transform.position.magnitude > touchThreshold) {
            _touched = true;
            _tc.Add(1);
            Debug.Log(this.name + " - touched");
        }
    }

    public void TouchWithSound() {
        this.Touch();
        if (transform.position.magnitude > touchSoundThreshold) {
            PlayTouchSound();
        }
    }

    private void PlayTouchSound() {
        if (touchSounds != null && touchSounds.Length > 0 && _audio != null && !_audio.isPlaying) {
            var randomIndex = Random.Range(0, touchSounds.Length);
            _audio.PlayOneShot(touchSounds[randomIndex], transform.position.magnitude / touchSoundVolumeFactor);
        }
    }

    /// <summary>
    /// OnCollisionStay is called once per frame for every collider/rigidbody
    /// that is touching rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionStay(Collision other) {
        TouchableController tc = other.gameObject.GetComponent<TouchableController>();
        if (tc != null && _touched) {
            tc.Touch();
        }
    }
}