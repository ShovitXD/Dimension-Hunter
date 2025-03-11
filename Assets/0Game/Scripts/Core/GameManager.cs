using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public string RoomCode { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            GenerateRoomCode(); // Ensure a room code is generated on startup.
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GenerateRoomCode()
    {
        RoomCode = Random.Range(1000, 9999).ToString();
    }
}
