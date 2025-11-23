using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class JoinHost : MonoBehaviour
{
    public Button StartHostButton;
    public Button JoinHostButton;
    public Button StartGameButton;
    public void Join()
    {
        NetworkManager.Singleton.StartClient();
        if (NetworkManager.Singleton.IsClient && NetworkManager.Singleton.IsConnectedClient)
        {
            StartHostButton.gameObject.SetActive(false);
            JoinHostButton.gameObject.SetActive(false);
            StartGameButton.gameObject.SetActive(false);
        }
    }
}   

