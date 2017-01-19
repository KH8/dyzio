using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public GameObject mainPlayer;
    public GameObject menuPlayer;
    public Camera mainCamera;
    public Camera menuCamera;
    public Camera gameOverCamera;
    public Image curtain;
    public Canvas mainCanvas;
    public Canvas menuCanvas;
    public Canvas pauseCanvas;
    public Canvas gameOverCanvas;
    public BackgroundAudioPlayer audioPlayer;
    public LightController lightController;
    public GameOverMenuController gameOverMenuController;

    private TimeCounter _timeCounter;
    private PointCounter _pointCounter;
    private TouchCounter _touchCounter;

    private GameMode _mode = GameMode.Menu;
	private GameMode _pausedMode;

    private CheatReader _cheatReader = new CheatReader("CATSLISTENTODUBSTEP");

    public void Reset() {
        if (ChangeMode(GameMode.Menu)) {
            SceneManager.LoadScene("Room");
            EnableGameObjects();
            audioPlayer.PlayMainTheme();
            lightController.MainLightMode();
        }
    }

    public void StartGame() {
        if (ChangeMode(GameMode.Running)) {
            EnableGameObjects();
            audioPlayer.PlayRandomSong(); 
            _timeCounter.Run();
        }
    }

	public void Resume() {
		if (ChangeMode(_pausedMode)) {
			EnableGameObjects();
            _timeCounter.Resume();
		}
	}

    public void Pause() {
		_pausedMode = _mode;
        if (ChangeMode(GameMode.Paused)) {
            EnableGameObjects();
            _timeCounter.Pause();  
        }
    }

    public void GameOver() {
        if (ChangeMode(GameMode.GameOver)) {
            EnableGameObjects();
            _timeCounter.Pause();
            gameOverMenuController.SetScores(_pointCounter.GetValueText(), _touchCounter.GetValueText());
        }
    }

    public void Exit() {
        if (ChangeMode(GameMode.Quitting)) {
            Application.Quit();
        }
    }

    public void DubStep() {
		if (ChangeMode(GameMode.DubStep)) {
            audioPlayer.PlaySomeDubStep(); 
            lightController.DiscoMode();
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

    private void EnableGameObjects() {
        EnablePlayers();
        EnableCameras();
        EnableCanvases();
    }

    private void EnablePlayers() {
        mainPlayer.SetActive(IsRunningOrPausedOrGameOver());
        menuPlayer.SetActive(IsInMenu());
    }

    private void EnableCameras() {
        mainCamera.enabled = IsRunningOrPaused();
        menuCamera.enabled = IsInMenu();
        gameOverCamera.enabled = IsGameOver();
    }

    private void EnableCanvases() {
        mainCanvas.enabled = IsRunningOrPaused();
        menuCanvas.enabled = IsInMenu();
        pauseCanvas.enabled = IsPaused();
        gameOverCanvas.enabled = IsGameOver();
        AnimateCommonCanvas();
    }

    private bool IsPaused() {
        return GameMode.Paused.Equals(_mode);
    }

    private bool IsRunning() {
		return GameMode.Running.Equals(_mode) || GameMode.DubStep.Equals(_mode);
    }

    private bool IsRunningOrPaused() {
        return IsRunning() || IsPaused();
    }

    private bool IsRunningOrPausedOrGameOver() {
        return IsRunningOrPaused() || IsGameOver();
    }

    private bool IsInMenu() {
        return GameMode.Menu.Equals(_mode);
    }

    private bool IsGameOver() {
        return GameMode.GameOver.Equals(_mode);
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
		EnableGameObjects();
		audioPlayer.PlayMainTheme();
        lightController.MainLightMode();
        _timeCounter = GetComponent<TimeCounter>();
        _pointCounter = GetComponent<PointCounter>();
        _touchCounter = GetComponent<TouchCounter>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
	void Update() {
        if (IsRunning() && Input.GetKey(KeyCode.Escape)) {
            Pause();
		}
		CheckCheats();
    }

    private void CheckCheats() {
        if (IsRunning() && Input.anyKeyDown) {
            _cheatReader.NextChar(ResolveKeyPressed());
            if (_cheatReader.IsValid()) {
                DubStep();
            }
        }
    }

    private char ResolveKeyPressed() {
        foreach(KeyCode kcode in KeyCode.GetValues(typeof(KeyCode))) {
            if (Input.GetKeyDown(kcode)) {
                return kcode.ToString()[0];
            }
        }
        return '_';
    }

    private void AnimateCommonCanvas() {
        curtain.enabled = true;
        curtain.CrossFadeAlpha(0.0f, 2.0f, false);
    }

    public GameMode GetMode() {
        return _mode;
    }
}