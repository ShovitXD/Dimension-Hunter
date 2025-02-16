
using Photon.Pun;

public static class LoadLevel
{
    public static void LoadServerSelect() => PhotonNetwork.LoadLevel(0);
    public static void LoadLoginScreen() => PhotonNetwork.LoadLevel(1);
    public static void LoadMainMenu() => PhotonNetwork.LoadLevel(2);
    public static void LoadLobby() => PhotonNetwork.LoadLevel(3);
    public static void LoadGameLevel1() => PhotonNetwork.LoadLevel(4);  
}
