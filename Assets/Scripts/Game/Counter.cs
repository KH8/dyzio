using UnityEngine;
using UnityEngine.UI;

public abstract class Counter : MonoBehaviour {
    public Text valueDisplay;
    public Text newValueDisplay;

    public float pointFactor = 10.0f;
    public int initialFontSize = 360;
    public float decrementAlphaFactor = 0.05f;
    public int decrementSizeFactor = 20;
    public float tickSoundInterval = 0.05f;

    public AudioClip scoreSound;
    public AudioClip tickSound;

    private AudioSource _audio;

    private SmoothCounter _counter = new SmoothCounter();

    private bool _animationRunning = false;
    private float _lastTimeTickSoundPlayed = -1.0f;

    protected abstract string DisplayValueText(int counter);

    protected abstract string DisplayNewValueText(int value);

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected virtual void Awake() {
        _audio = GetComponent<AudioSource>();
        _audio.clip = scoreSound;
        newValueDisplay.enabled = false;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        valueDisplay.text = DisplayValueText(_counter.GetActual());
        RunNewValueAnimation();
        PlayTickSound();
    }

    public void Add(float points) {
        var p = Mathf.FloorToInt(pointFactor * points);
        _counter.Add(p);
        StartNewValueAnimation(p);
        PlayScoreSound();
    }

    public string GetValueText() {
        return DisplayValueText(_counter.GetSetPoint());
    }

    private void RunNewValueAnimation() {
        if (_animationRunning) {
            newValueDisplay.fontSize -= decrementSizeFactor;
            DecrementTextAlpha();
            if (newValueDisplay.fontSize < valueDisplay.fontSize) {
                newValueDisplay.enabled = false;
                _animationRunning = false;
            }
        }
    }

    private void StartNewValueAnimation(int points) {
        newValueDisplay.text = DisplayNewValueText(points);
        newValueDisplay.enabled = true;
        newValueDisplay.fontSize = initialFontSize;
        SetTextAlpha(1.0f);

        _animationRunning = true;
    }

    private void DecrementTextAlpha() {
        var alpha = newValueDisplay.color.a - decrementAlphaFactor;
        SetTextAlpha(alpha) ;
    }

    private void SetTextAlpha(float value) {
        var c = newValueDisplay.color;
        c.a = value;
        newValueDisplay.color = c;
    }

    private void PlayScoreSound() {
        if (!_audio.isPlaying) {
            _audio.PlayDelayed(0.2f);
        }
    }

    private void PlayTickSound() {
        if (_counter.IsCounting() && Time.realtimeSinceStartup - _lastTimeTickSoundPlayed > tickSoundInterval) {
            _audio.PlayOneShot(tickSound);
            _lastTimeTickSoundPlayed = Time.realtimeSinceStartup;
        }
    }
}