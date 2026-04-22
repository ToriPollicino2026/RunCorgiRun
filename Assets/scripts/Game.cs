using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public CanvasGroup StartScreen;
    public UI UI;
    public GameTimer GameTimer;

    public void Start()
    {
        UI.ShowStartScreen();
    }
    

    public void Update()
    {
        UI.ShowTime();
    }

    public void OnStartButtonClicked()
    {
        UI.HideStartScreen();
        StartGame();
    }

    private void StartGame()
    {
        GameTimer.StartTimer(20, OnTimerFinsihed);
    }

    private void OnTimerFinsihed()
    {
        UI.ShowGameOverScreen();
    }

    public void OnPlayAgainButtonClick()
    {
        UI.HideGameOverScreen();
        StartGame();
    }
}
