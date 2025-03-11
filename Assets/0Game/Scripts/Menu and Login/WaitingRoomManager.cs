using TMPro;
using UnityEngine;
using Photon.Pun;

public class WaitingRoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text RoomCodeTXT;

    private void Start()
    {
        RoomCodeTXT.text = GameManager.Instance.RoomCode;
    }

    //Togle Character select and Team select 

}
