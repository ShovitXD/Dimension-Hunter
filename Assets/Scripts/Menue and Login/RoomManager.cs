using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text RoomCode;
    [SerializeField] private TMP_InputField RoomCodeInp;
    [SerializeField] private GameObject RoomPannel;

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
        PhotonNetwork.JoinRoom(RoomCodeInp.text);
    }

    public void ActivateRoomPannel()
    {
        RoomPannel.SetActive(true);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("return code: " + returnCode + message);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError("Disconnected from Photon" + cause);
    }
}
