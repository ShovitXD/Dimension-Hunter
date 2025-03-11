using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text ErrorTXT;
    [SerializeField] private TMP_InputField RoomCodeInp;
    [SerializeField] private GameObject RoomPannel;
    private string RoomCode;

    private void Start()
    {
        //Get the Room Code
        if (GameManager.Instance != null)
        {
            RoomCode = GameManager.Instance.RoomCode;
        }
        else
        {
            Debug.LogError("GameManager instance not found!");
            return;
        }

        RoomPannel.SetActive(false);
    }

    // Start Game/Room
    public void CreateRoom()
    {
        if (!string.IsNullOrEmpty(RoomCode))
        {
            RoomOptions options = new RoomOptions { MaxPlayers = 4 };
            PhotonNetwork.CreateRoom(RoomCode, options);
            LoadLevel.LoadLobby();
            Debug.Log("Trie to Created a Room");
        }
        else
        {
            Debug.LogError("Room code is empty.");
        }
    }

    //Joined the input Room
    public void JoinRoom()
    {
        if (string.IsNullOrEmpty(RoomCodeInp.text))
        {
            ErrorTXT.text = "Please input a valid code.";
        }
        else
        {
            PhotonNetwork.JoinRoom(RoomCodeInp.text);
            LoadLevel.LoadLobby();
            Debug.Log("Tried to Joined a Room");
        }
    }

    // Toggle for activating panel to join a room
    public void ToggleMainMenu()
    {
        RoomPannel.SetActive(!RoomPannel.activeSelf);
    }


    //Error Handling Ignore
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Failed to join room: " + message);
        ErrorTXT.text = "Failed to join room: " + message;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Room creation failed: " + message);
        ErrorTXT.text = "Room creation failed: " + message;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError("Disconnected from Photon: " + cause);
    }
}
