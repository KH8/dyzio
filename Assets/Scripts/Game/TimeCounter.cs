using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour {
    public Text timerDisplay;

    public float initialTime = 120.0f;

    public AudioClip countDownSound;

    private GameController _gc;
    private AudioSource _audio;

    private float _timeLeft = 0.0f;
    private bool _running = false;
    private bool _countdown = false;
    private bool _stopped = false;

    private float _alpha = 1.0f;
    private float _alphaDirection = -1f;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        _gc = GameObject.Find("Game").GetComponent<GameController>();
        _audio = GetComponent<AudioSource>();
    }

    public void Run() {
        Debug.Log("Timer - Start!");
        _timeLeft = initialTime;
        _stopped = false;
        _running = true;
        _countdown = false;
    }

    public void Pause() {
        _running = false;
        _audio.Pause();
    }

    public void Stop() {
        _stopped = true;
        _running = false;
        _audio.Pause();
    }

    public void Resume() {
        _running = !_stopped;
        if (_countdown) {
            _audio.Play();
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        UpdateTimerDisplay();
        if (!_running) {
            return;
        }
        DecrementCounter();
        CountDown();
    }

    private void UpdateTimerDisplay() {
        System.TimeSpan t = System.TimeSpan.FromSeconds(_timeLeft);
        timerDisplay.text = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }

    private void DecrementCounter() {
        if (_timeLeft > 0.0f) {
            _timeLeft -= Time.deltaTime;
        } else {
            _gc.GameOver();
        }
    }

    private void CountDown() {
        if (_timeLeft < 11.0f) {
            AnimateTimer();
            if (!_countdown) {
                StartCountDownSound();
                _countdown = true;
            }
        }
    }

    private void StartCountDownSound() {
        _audio.clip = countDownSound;
        _audio.Play();
    }

    private void AnimateTimer() {
        BounceAlphaValue();
        Color c = timerDisplay.color;
        c.a = _alpha;
        timerDisplay.color = c;
    }

    private void BounceAlphaValue() {
        _alpha += _alphaDirection * 0.1f;
        if (_alpha < 0.0 || _alpha > 1) {
            _alphaDirection *= -1;
        }
    }
}