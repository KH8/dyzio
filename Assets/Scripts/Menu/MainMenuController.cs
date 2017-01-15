using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MenuController {
    private static string PLAY = "Play";
    private static string EXIT = "Exit";

    public Text playDisplay;
    public Text exitDisplay;

    protected override MenuOperation InitOperation() {
        return MenuOperation.Play;
    }

    protected override MenuOperation[] InitOperations() {
        return new [] {MenuOperation.Play, MenuOperation.Exit};
    }

    protected override void DisplayOperationSelection() {
        switch(_operation) {
            case MenuOperation.Play:
                ActivateText(playDisplay, PLAY);
                DeactivateText(exitDisplay, EXIT);
                break;
            case MenuOperation.Exit:
                ActivateText(exitDisplay, EXIT);
                DeactivateText(playDisplay, PLAY);
                break;
        }
    }

    protected override void HandleOperation() {
        if (Input.GetKey(KeyCode.Return)) {
            switch(_operation) {
            case MenuOperation.Play:
                _gc.StartGame();
                break;
            case MenuOperation.Exit:
                _gc.Exit();
                break;
            }
        }
    }
}