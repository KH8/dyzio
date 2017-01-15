using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour {
    private static string CONTINUE = "Continue";
    private static string MAIN_MENU = "Main Menu";

    public Text cont;
    public Text mainMenu;

    private GameController _gc;
    
    private Color _ginger;
    private int _index = 0;
    private MenuOperation _operation = MenuOperation.Continue;
    private MenuOperation[] _operations = new [] {MenuOperation.Continue, MenuOperation.MainMenu};

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        _gc = GameObject.Find("Game").GetComponent<GameController>();
        ColorUtility.TryParseHtmlString("#FF9638FF", out _ginger);
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

    void DisplayOperationSelection() {
        switch(_operation) {
            case MenuOperation.Continue:
                ActivateText(cont, CONTINUE);
                DeactivateText(mainMenu, MAIN_MENU);
                break;
            case MenuOperation.MainMenu:
                ActivateText(mainMenu, MAIN_MENU);
                DeactivateText(cont, CONTINUE);
                break;
        }
    }

    void ActivateText(Text text, string content) {
        text.color = Color.white;
        text.text = "- " + content + " -";
    }

    void DeactivateText(Text text, string content) {
        text.color = _ginger;
        text.text = content;
    }

    void HandleOperation() {
        if (Input.GetKey(KeyCode.Return)) {
            switch(_operation) {
            case MenuOperation.Continue:
                _gc.StartGame();
                break;
            case MenuOperation.MainMenu:
                _gc.Reset();
                break;
            }
        }
    }

    private enum MenuOperation {
        Continue,
        MainMenu
    }
}