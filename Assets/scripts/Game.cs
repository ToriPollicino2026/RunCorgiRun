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
        
    }

    private void StartGame()
    {
        GameTimer.StartTimer(10, OnTimerFinsihed);
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
