using UnityEngine.UI;

public class GameOverMenuController : MenuController {
    private static string MAIN_MENU = "Main Menu";

    private static string SCORE = "You've scored {0:D2} points\nand moved {1:D2} objects ";

    public Text scoreDisplay;
    public Text mainMenuDisplay;

    public void SetScores(string points, string touches) {
        scoreDisplay.text = string.Format(SCORE, points, touches);
    }

    protected override MenuOperation InitOperation() {
        return MenuOperation.MainMenu;
    }

    protected override MenuOperation[] InitOperations() {
        return new [] {MenuOperation.MainMenu};
    }

    protected override Text InitActiveText() {
        return mainMenuDisplay;
    }

    protected override void DisplayOperation(MenuOperation operation) {
        switch(operation) {
            case MenuOperation.MainMenu:
                ActivateText(mainMenuDisplay, MAIN_MENU);
                break;
        }
    }

    protected override void HandleOperation(MenuOperation operation) {
        switch(operation) {
            case MenuOperation.MainMenu:
                GetGameController().Reset();
                break;
        }
    }
}