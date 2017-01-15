using UnityEngine;
using UnityEngine.UI;

public abstract class MenuController : MonoBehaviour {
    public AudioClip tickSound;

    private GameController _gc;
    private AudioSource _audio;
    
    private Color _ginger;
    private int _index = 0;
    private MenuOperation _operation;
    private MenuOperation[] _operations;

    protected abstract MenuOperation InitOperation();

    protected abstract MenuOperation[] InitOperations();

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        ColorUtility.TryParseHtmlString("#FF9638FF", out _ginger);
        _gc = GameObject.Find("Game").GetComponent<GameController>();
        _audio = GetComponent<AudioSource>();
        _operation = InitOperation();
        _operations = InitOperations();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        ChangeOperation();
        DisplayOperation(_operation);
        HandleOperation(_operation);
    }

    private void ChangeOperation() {
        var v = Input.GetAxisRaw("Vertical");
        if (v < 0) {
            SetNextOperation();
        } else if (v > 0) {
            SetPreviousOperation();
        }
    }

   private void SetNextOperation() {
        if (_index < _operations.Length - 1) {
            _index++;
            _operation = _operations[_index];
            PlayTick();
        }
    }

    private void SetPreviousOperation() {
        if (_index > 0) {
            _index--;
            _operation = _operations[_index];
            PlayTick();
        }
    }

    private void PlayTick() {
        _audio.PlayOneShot(tickSound);
    }

    protected abstract void DisplayOperation(MenuOperation operation);

    protected abstract void HandleOperation(MenuOperation operation);

    protected void ActivateText(Text text, string content) {
        text.color = Color.white;
        text.text = "- " + content + " -";
    }

    protected void DeactivateText(Text text, string content) {
        text.color = _ginger;
        text.text = content;
    }

    protected GameController GetGameController() {
        return _gc;
    }

    protected enum MenuOperation {
        Continue,
        MainMenu,
        Play,
        Exit
    }
}