using UnityEngine;

public class GameController : MonoBehaviour {
    private GameMode _mode = GameMode.Menu;

    public void Reset() {
        if (ChangeMode(GameMode.Menu)) {
        }
    }

    public void StartGame() {
        if (ChangeMode(GameMode.Running)) {

        }
    }

    public void Pause() {
        if (ChangeMode(GameMode.Paused)) {
        }
    }

    public void Exit() {
        if (ChangeMode(GameMode.Quitting)) {
            Application.Quit();
        }
    }

    private bool ChangeMode(GameMode mode) {
        if (_mode.Equals(mode)) {
            return false;
        }
        Debug.Log("Changing mode to: " + mode);
        _mode = mode;
        return true;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        if (GameMode.Running.Equals(_mode) && Input.GetKey(KeyCode.Escape)) {
            Reset();
        }
    }

    public GameMode GetMode() {
        return _mode;
    }
}