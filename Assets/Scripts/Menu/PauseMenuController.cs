using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MenuController {
    private static string CONTINUE = "Continue";
    private static string MAIN_MENU = "Main Menu";

    public Text continueDisplay;
    public Text mainMenuDisplay;

    protected override MenuOperation InitOperation() {
        return MenuOperation.Continue;
    }

    protected override MenuOperation[] InitOperations() {
        return new [] {MenuOperation.Continue, MenuOperation.MainMenu};
    }

    protected override void DisplayOperationSelection() {
        switch(_operation) {
            case MenuOperation.Continue:
                ActivateText(continueDisplay, CONTINUE);
                DeactivateText(mainMenuDisplay, MAIN_MENU);
                break;
            case MenuOperation.MainMenu:
                ActivateText(mainMenuDisplay, MAIN_MENU);
                DeactivateText(continueDisplay, CONTINUE);
                break;
        }
    }

    protected override void HandleOperation() {
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
}