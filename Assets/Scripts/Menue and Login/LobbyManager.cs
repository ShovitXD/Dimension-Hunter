using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        ConnectToLobby();
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
