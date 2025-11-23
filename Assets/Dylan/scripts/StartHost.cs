using System.Threading;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
public class StartHost : MonoBehaviour
{
    public Button StartHostButton;
    public Button JoinHostButton;
    public Button StartGameButton;
    public void Host()
    {
        if (NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            NetworkManager.Singleton.Shutdown();
            Thread.Sleep(1500);
        }
        NetworkManager.Singleton.StartHost();
        StartHostButton.gameObject.SetActive(false);
        JoinHostButton.gameObject.SetActive(false);
        StartGameButton.gameObject.SetActive(true);
    }
}
