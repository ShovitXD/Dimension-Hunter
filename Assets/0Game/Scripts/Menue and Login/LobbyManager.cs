using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject MainCanvas,CCcanvas;

    private void Start()
    {
        ConnectToLobby();
        CCcanvas.SetActive(false);
    }

    private void ConnectToLobby()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinLobby();
        }
        else
        {
            Debug.LogWarning("Photon is noi connected!");
            LoadLevel.LoadServerSelect();
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joinned Lobby");
    }

    public void ToLobby()
    {
        LoadLevel.LoadLobby();
    }

    public void ToggleMainMenu()
    {
        if (MainCanvas.activeSelf)
        {
            MainCanvas.SetActive(false);
            CCcanvas.SetActive(true);
        }
        else
        {
            CCcanvas.SetActive(false);
            MainCanvas.SetActive(true);
        }
    }

    public void Quit()
    {
        PhotonNetwork.Disconnect();
        LoadLevel.LoadServerSelect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError("Disconnected from Photon" + cause);
    }

}
