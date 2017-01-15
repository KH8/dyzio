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

    protected override void DisplayOperation(MenuOperation operation) {
        switch(operation) {
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

    protected override void HandleOperation(MenuOperation operation) {
        if (Input.GetKey(KeyCode.Return)) {
            switch(operation) {
            case MenuOperation.Play:
                GetGameController().StartGame();
                break;
            case MenuOperation.Exit:
                GetGameController().Exit();
                break;
            }
        }
    }
}