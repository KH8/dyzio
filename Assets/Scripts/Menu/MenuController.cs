using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    private static string PLAY = "Play";
    private static string EXIT = "Exit";

    public Text play;
    public Text exit;
    
    private Color _ginger;
    private int _index = 0;
    private MenuOperation _operation = MenuOperation.Play;
    private MenuOperation[] _operations = new [] {MenuOperation.Play, MenuOperation.Exit};

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        ColorUtility.TryParseHtmlString ("#FF9638FF", out _ginger);
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
            case MenuOperation.Play:
                ActivateText(play, PLAY);
                DeactivateText(exit, EXIT);
                break;
            case MenuOperation.Exit:
                ActivateText(exit, EXIT);
                DeactivateText(play, PLAY);
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
            case MenuOperation.Play:
                break;
            case MenuOperation.Exit:
                Debug.Log("Quitting game");
                Application.Quit();
                break;
            }
        }
    }

    private enum MenuOperation {
        Play,
        Exit
    }
}