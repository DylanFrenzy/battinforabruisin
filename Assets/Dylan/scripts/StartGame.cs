using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Button StartGameButton;
    public Canvas StartMenuCanvas;
    public Canvas UICanvas;
    public Image BackgroundImage;
    public int playerCount = 0;

    public void StartTheGame()
    {
        playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        if (playerCount < 2)
        {
            Debug.Log("Not enough players to start the game.");
            return;
        }
        StartGameButton.gameObject.SetActive(false);
        StartMenuCanvas.gameObject.SetActive(false);
        UICanvas.gameObject.SetActive(true);
        BackgroundImage.gameObject.SetActive(false);
    }
}
