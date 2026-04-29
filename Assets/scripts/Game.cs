using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public CanvasGroup StartScreen;
    public UI UI;
    public GameTimer GameTimer;

    private bool isGameRunning = false;
    
    public BeerPlacer BeerPlacer;
    public BonePlacer BonePlacer;
    public PillPlacer PillPlacer;
    public MoonshinePlacer MoonshinePlacer;
    public Music Music;
    public Sounds Sounds;

    public corgi Corgi; 

    public void Start()
    {
        UI.ShowStartScreen();
        Music.PlayMenuMusic();
    }
    

    public void Update()
    {
        UI.ShowTime();
    }

    public bool IsPlaying()
    {
        return isGameRunning;
    }

    public void OnStartButtonClicked()
    {
        UI.HideStartScreen();
        Sounds.PlayPoopSound();
        InitializeGame();
    }

    public void InitializeGame()
    {
        isGameRunning = true;
        GameTimer.StartTimer(20, OnTimerFinsihed);
        StartPlacers();
        ScoreKeeper.ResetScore();
        UI.ResetText();
        Corgi.Reset();
        Music.PlayGameMusic();
    }

    private void StartPlacers()
    {
        BeerPlacer.StartPlacing();
        PillPlacer.StartPlacing();
        BonePlacer.StartPlacing();
        MoonshinePlacer.StartPlacing();
    }

    private void StopPlacers()
    {
        BeerPlacer.StopPlacing();
        PillPlacer.StopPlacing();
        BonePlacer.StopPlacing();
        MoonshinePlacer.StopPlacing();
    }
    

    private void OnTimerFinsihed()
    {
        UI.ShowGameOverScreen();
        StopPlacers();
        Music.PlayMenuMusic();
    }

    public void OnPlayAgainButtonClick()
    {
        UI.HideGameOverScreen();
        Sounds.PlayPoopSound();
        InitializeGame();
    }
}
