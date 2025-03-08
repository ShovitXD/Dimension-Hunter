using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PreviewSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject PlayerPrefab; // Player model prefab
    [SerializeField] private Transform[] SpawnList; // List of spawn points

    private void Start()
    {
        if (PhotonNetwork.InRoom)
        {
            SpawnPreviewPlayer();
        }
    }

    private void SpawnPreviewPlayer()
    {
        if (SpawnList.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }

        int spawnIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1; // Assigning spawn index based on player order
        spawnIndex = Mathf.Clamp(spawnIndex, 0, SpawnList.Length - 1); // Ensure it's within valid range

        GameObject playerPreview = Instantiate(PlayerPrefab, SpawnList[spawnIndex].position, Quaternion.identity);
        Debug.Log("Spawned preview player at index: " + spawnIndex);
    }
}
