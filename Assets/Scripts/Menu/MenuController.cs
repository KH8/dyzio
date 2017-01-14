using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    public Text play;
    public Text exit;
    
    private Color _ginger;
    private MenuOperation _operation = MenuOperation.Play;

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
        if (v > 0 && MenuOperation.Exit.Equals(_operation)) {
            _operation = MenuOperation.Play;
        }
        if (v < 0 && MenuOperation.Play.Equals(_operation)) {
            _operation = MenuOperation.Exit;
        }
    }

    void DisplayOperationSelection() {
        switch(_operation) {
            case MenuOperation.Play:
                play.color = Color.white;
                play.text = "- Play -";
                exit.color = _ginger;
                exit.text = "Exit";
                break;
            case MenuOperation.Exit:
                play.color = _ginger;
                play.text = "Play";
                exit.color = Color.white;
                exit.text = "- Exit -";
                break;
        }
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