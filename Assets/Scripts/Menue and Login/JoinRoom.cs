using Photon.Pun;
using UnityEngine;
using Unity.Services.CloudSave;
using System.Threading.Tasks;
using System.Collections.Generic;

public class JoinRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject LoadingScene;
    private string playerName = string.Empty;

    private async void Start()
    {
        await FetchPlayerName();
    }

    private async Task FetchPlayerName()
    {
        try
        {
            // Change to HashSet<string> instead of string[]
            var savedData = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { "PlayerName" });
            if (savedData.TryGetValue("PlayerName", out var nameValue))
            {
                playerName = nameValue.ToString();
                PhotonNetwork.LocalPlayer.NickName = playerName;
            }
        }
        catch
        {
            Debug.LogError("Failed to load player name from Cloud Save.");
        }
    }

    public void EnterRoom()
    {
        if (!string.IsNullOrWhiteSpace(playerName))
        {
            PhotonNetwork.AutomaticallySyncScene = true;

            if (PhotonNetwork.IsConnectedAndReady)
            {
                PhotonNetwork.JoinRandomRoom();
                LoadingScene.SetActive(true);
            }
            else
            {
                Debug.LogError("Not connected to server");
            }
        }
        else
        {
            Debug.LogWarning("Player name is empty");
        }

        Debug.Log("C/J BTN Pressed");
    }

    public void ExitBtn()
    {
        Application.Quit();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(3);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        string randomRoomName = "Room_" + Random.Range(1000, 9999);
        PhotonNetwork.CreateRoom(randomRoomName);
    }
}
