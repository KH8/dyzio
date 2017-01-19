using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour {
    public Text timerDisplay;

    public float initialTime = 120.0f;

    private GameController _gc;

    private float _timeLeft = 0.0f;
    private bool _running = false;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        _gc = GameObject.Find("Game").GetComponent<GameController>();
    }

    public void Run() {
        Debug.Log("Timer - Start!");
        _timeLeft = initialTime;
        _running = true;
    }

    public void Pause() {
        _running = false;
    }

    public void Resume() {
        _running = true;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        UpdateTimerDisplay();
        if (!_running) {
            return;
        }
        Countdown();
    }

    private void UpdateTimerDisplay() {
        System.TimeSpan t = System.TimeSpan.FromSeconds(_timeLeft);
        timerDisplay.text = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }

    private void Countdown() {
        if (_timeLeft > 0.0f) {
            _timeLeft -= Time.deltaTime;
        } else {
            _gc.GameOver();
        }
    }
}