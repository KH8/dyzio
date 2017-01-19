using UnityEngine;

public class BackgroundAudioPlayer : MonoBehaviour {
    private static int FADE_RAMP = 50;

    public AudioClip mainBackgroundSound;
    public AudioClip[] backgroundSounds;
    public AudioClip dubstepBackgroundSound;

    private AudioSource _audio;

    private bool _randomMode = false;

    public void PlayMainTheme() {
        StopPlayback();
        _audio.clip = mainBackgroundSound;
        _audio.loop = true;
        _audio.PlayDelayed(0.5f);
    }

    public void PlaySomeDubStep() {
        StopPlayback();
        _audio.clip = dubstepBackgroundSound;
        _audio.loop = true;
        _audio.Play();
    }

    public void PlayRandomSong() {
        StopPlayback();
        _randomMode = true;
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        _audio = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        if (_randomMode) {
            KeepPlayingRandomSong();
        }
    }
    private void KeepPlayingRandomSong() {
        if (!_audio.isPlaying) {
            var randomIndex = Random.Range(0, backgroundSounds.Length);
            _audio.clip = backgroundSounds[randomIndex];
            _audio.loop = false;
            _audio.Play();
        }
    }

    private void StopPlayback() {
        while (_audio.volume > 0.001f) {
            _audio.volume -= Time.deltaTime / FADE_RAMP;
        }
        _audio.Stop();
        _audio.volume = 1.0f;
        _randomMode = false;
    }
}