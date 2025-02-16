using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Photon.Pun;

public class RemoveAccount : MonoBehaviourPunCallbacks
{
    [Header("UI Elements")]
    [SerializeField] private GameObject confirmationPopup;
    [SerializeField] private GameObject loadingIndicator;

    private void Start()
    {
        // Ensure UI elements are inactive initially
        confirmationPopup.SetActive(false);
        loadingIndicator.SetActive(false);
    }

    public void ShowConfirmation()
    {
        confirmationPopup.SetActive(true);
    }

    public void HideConfirmation()
    {
        confirmationPopup.SetActive(false);
    }

    public async void DeleteAccountAsync()
    {
        if (!AuthenticationService.Instance.IsSignedIn)
            return;

        try
        {
            loadingIndicator.SetActive(true);
            confirmationPopup.SetActive(false);

            // Remove player data from Cloud Save
            await CloudSaveService.Instance.Data.Player.DeleteAsync("PlayerName");

            // Disconnect from Photon
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.Disconnect();
            }

            // Delete Unity Authentication account
            await AuthenticationService.Instance.DeleteAccountAsync();

            // Load back to the login screen
            LoadLevel.LoadServerSelect();
        }
        catch
        {
            // Handle errors (optional: log to console if needed)
        }
        finally
        {
            loadingIndicator.SetActive(false);
        }
    }

    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
    {
        Debug.Log("Disconnected from Photon: " + cause);
    }
}