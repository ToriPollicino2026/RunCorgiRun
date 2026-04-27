using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public TMP_Text scoreText;
    public CanvasGroup StartScreenCanvasGroup;
    public TMP_Text TimeText;
    public GameTimer GameTimer;
    public CanvasGroup GameOverScreenCanvasGroup;
    public void SetScoreText(int score)
    {
        scoreText.text = "Score: " + ScoreKeeper.GetScore();
    }
    
    public void ResetText()
    {
        scoreText.text = "Score: 0";
    }

    public void ShowStartScreen()
    {
        CanvasGroupDisplayer.Show(StartScreenCanvasGroup);
        CanvasGroupDisplayer.Hide(GameOverScreenCanvasGroup);
    }

    public void HideStartScreen()
    {
        CanvasGroupDisplayer.Hide(StartScreenCanvasGroup);
    }

    public void ShowTime()
    {
        TimeText.text = GameTimer.GetTimeAsString();
    }

    public void ShowGameOverScreen()
    {
        CanvasGroupDisplayer.Show(GameOverScreenCanvasGroup);
        CanvasGroupDisplayer.Hide(StartScreenCanvasGroup);
    }

    public void HideGameOverScreen()
    {
        CanvasGroupDisplayer.Hide(GameOverScreenCanvasGroup);
    }
}
