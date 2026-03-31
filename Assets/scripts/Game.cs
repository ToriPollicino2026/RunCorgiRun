using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public CanvasGroup StartScreen;

    public void OnStartButtonClicked()
    {
        UI.HideStartScreen();
        //start the game 
    }
}
