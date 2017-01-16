using UnityEngine;

public class BackgroundAudioPlayer : MonoBehaviour {
    public AudioClip mainBackgroundSound;
    public AudioClip[] backgroundSounds;

    private GameController _gc;
    private AudioSource _audio;

    private GameMode _previousMode = GameMode.Quitting;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        _gc = GameObject.Find("Game").GetComponent<GameController>();
        _audio = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        Debug.Log(_gc.GetMode() + " : " + _previousMode);
        if (IsInMenu()) {
            PlayMainTheme();
        } else {
            KeepPlayingRandomSong();
        }
        _previousMode = _gc.GetMode();
    }

    private bool HasModeChanged() {
        return !_gc.GetMode().Equals(_previousMode);
    }

    private bool IsInMenu() {
        return GameMode.Menu.Equals(_gc.GetMode());
    }

    private bool WasInMenu() {
        return GameMode.Menu.Equals(_previousMode);
    }

    private void PlayMainTheme() {
        if (!WasInMenu()) {
            _audio.Stop();
            _audio.clip = mainBackgroundSound;
            _audio.loop = true;
            _audio.Play();
        }
    }

    private void KeepPlayingRandomSong() {
        if (WasInMenu()) {
            _audio.Stop();
        }
        if (!_audio.isPlaying) {
            var randomIndex = Random.Range(0, backgroundSounds.Length);
            _audio.clip = backgroundSounds[randomIndex];
            _audio.loop = false;
            _audio.Play();
        }
    }
}