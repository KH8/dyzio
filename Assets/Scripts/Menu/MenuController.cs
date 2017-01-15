using UnityEngine;
using UnityEngine.UI;

public abstract class MenuController : MonoBehaviour {
    protected GameController _gc;
    
    private Color _ginger;
    private int _index = 0;
    protected MenuOperation _operation;
    private MenuOperation[] _operations;

    protected abstract MenuOperation InitOperation();

    protected abstract MenuOperation[] InitOperations();

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        ColorUtility.TryParseHtmlString("#FF9638FF", out _ginger);
        _gc = GameObject.Find("Game").GetComponent<GameController>();
        _operation = InitOperation();
        _operations = InitOperations();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {
        ChangeOperation();
        DisplayOperationSelection();
        HandleOperation();
    }

    void ChangeOperation() {
        var v = Input.GetAxisRaw("Vertical");
        if (v < 0) {
            SetNextOperation();
        } else if (v > 0) {
            SetPreviousOperation();
        }
    }

    void SetNextOperation() {
        if (_index < _operations.Length - 1) {
            _index++;
            _operation = _operations[_index];
        }
    }

    void SetPreviousOperation() {
        if (_index > 0) {
            _index--;
            _operation = _operations[_index];
        }
    }

    protected abstract void DisplayOperationSelection();

    protected abstract void HandleOperation();

    protected void ActivateText(Text text, string content) {
        text.color = Color.white;
        text.text = "- " + content + " -";
    }

    protected void DeactivateText(Text text, string content) {
        text.color = _ginger;
        text.text = content;
    }

    protected enum MenuOperation {
        Continue,
        MainMenu,
        Play,
        Exit
    }
}