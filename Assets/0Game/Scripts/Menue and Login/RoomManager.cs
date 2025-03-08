using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text RoomCode, ErrorTXT;
    [SerializeField] private TMP_InputField RoomCodeInp;
    [SerializeField] private GameObject RoomPannel;

    private string roomToJoin = ""; // Stores the desired room name when switching

    private void Start()
    {
        RoomPannel.SetActive(false);
        CreateRoom();
    }

    public void CreateRoom()
    {
        string roomCode = Random.Range(1000, 9999).ToString();
        RoomOptions options = new RoomOptions { MaxPlayers = 4 };
        PhotonNetwork.CreateRoom(roomCode, options);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room Created: " + PhotonNetwork.CurrentRoom.Name);
        RoomCode.text = "Room Code: " + PhotonNetwork.CurrentRoom.Name;
    }

    public void JoinRoom()
    {
        if (string.IsNullOrEmpty(RoomCodeInp.text))
        {
            Debug.LogError("Cannot join room. RoomCodeInp is empty.");
            ErrorTXT.text = "Please input a valid code.";
            return;
        }

        if (PhotonNetwork.InRoom)
        {
            // Store the room we want to join, then leave the current room first
            roomToJoin = RoomCodeInp.text;
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            PhotonNetwork.JoinRoom(RoomCodeInp.text);
        }
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left Room");

        // If we stored a room to join, try joining it now
        if (!string.IsNullOrEmpty(roomToJoin))
        {
            PhotonNetwork.JoinRoom(roomToJoin);
            roomToJoin = ""; // Reset after joining
        }
    }

    public void ActivateRoomPannel()
    {
        RoomPannel.SetActive(true);
    }

    public void DeactivateRoomPannel()
    {
        RoomPannel.SetActive(false);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Join room failed: " + returnCode + " - " + message);
        ErrorTXT.text = "Failed to join room: " + message;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError("Disconnected from Photon: " + cause);
    }
}
