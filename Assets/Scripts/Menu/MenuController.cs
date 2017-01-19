using UnityEngine;
using UnityEngine.UI;

public abstract class MenuController : MonoBehaviour {
    public GameMode mode;
    public AudioClip tickSound;

    private GameController _gc;
    private AudioSource _audio;
    
    private Color _ginger;
    private int _index = 0;
    private MenuOperation _operation;
    private MenuOperation[] _operations;

    private Text _activeText;
    private float _alpha = 1.0f;
    private float _alphaDirection = -1f;

    protected abstract MenuOperation InitOperation();

    protected abstract MenuOperation[] InitOperations();

    protected abstract Text InitActiveText();

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        ColorUtility.TryParseHtmlString("#FF9638FF", out _ginger);
        _gc = GameObject.Find("Game").GetComponent<GameController>();
        _audio = GetComponent<AudioSource>();
        _operation = InitOperation();
        _operations = InitOperations();
        _activeText = InitActiveText();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        if (_gc.GetMode().Equals(mode)) {
            ChangeOperation();
            HandleOperationSelection();
        }
        DisplayOperation(_operation);
        AnimateActiveText();
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

    private void HandleOperationSelection() {
        if (Input.GetKey(KeyCode.Return)) {
            PlayTick();
            HandleOperation(_operation);
        }
    }

    protected abstract void HandleOperation(MenuOperation operation);

    protected void ActivateText(Text text, string content) {
        text.color = Color.white;
        text.text = "- " + content + " -";
        _activeText = text;
    }

    protected void DeactivateText(Text text, string content) {
        text.color = _ginger;
        text.text = content;
    }

    private void AnimateActiveText() {
        BounceAlphaValue();
        Color c = _activeText.color;
        c.a = _alpha;
        _activeText.color = c;
    }

    private void BounceAlphaValue() {
        _alpha += _alphaDirection * 0.05f;
        if (_alpha < 0.4 || _alpha > 1) {
            _alphaDirection *= -1;
        }
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