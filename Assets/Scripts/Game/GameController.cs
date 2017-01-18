using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public GameObject mainPlayer;
    public GameObject menuPlayer;
    public Camera mainCamera;
    public Camera menuCamera;
    public Canvas mainCanvas;
    public Canvas menuCanvas;
    public Canvas pauseCanvas;
    public BackgroundAudioPlayer audioPlayer;

    private GameMode _mode = GameMode.Menu;
	private GameMode _pausedMode;

    private CheatReader _cheatReader = new CheatReader("CATSLISTENTODUBSTEP");

    public void Reset() {
        if (ChangeMode(GameMode.Menu)) {
            SceneManager.LoadScene("Room");
            EnableGameObjects();
            audioPlayer.PlayMainTheme();
        }
    }

    public void StartGame() {
        if (ChangeMode(GameMode.Running)) {
            EnableGameObjects();
            audioPlayer.PlayRandomSong(); 
        }
    }

	public void Resume() {
		if (ChangeMode(_pausedMode)) {
			EnableGameObjects();
		}
	}

    public void Pause() {
		_pausedMode = _mode;
        if (ChangeMode(GameMode.Paused)) {
            EnableGameObjects();    
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
        mainPlayer.SetActive(IsRunningOrPaused());
        menuPlayer.SetActive(IsInMenu());
    }

    private void EnableCameras() {
        mainCamera.enabled = IsRunningOrPaused();
        menuCamera.enabled = IsInMenu();
    }

    private void EnableCanvases() {
        mainCanvas.enabled = IsRunningOrPaused();
        menuCanvas.enabled = IsInMenu();
        pauseCanvas.enabled = IsPaused();
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

    private bool IsInMenu() {
        return GameMode.Menu.Equals(_mode);
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
		EnableGameObjects();
		audioPlayer.PlayMainTheme();
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

    public GameMode GetMode() {
        return _mode;
    }
}