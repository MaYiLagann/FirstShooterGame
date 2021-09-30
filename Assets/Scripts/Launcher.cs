using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Using connecting to photon network server
/// </summary>
public class Launcher : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// Event when connected to master server
    /// </summary>
    public override void OnConnectedToMaster()
    {
        Debug.Log("@Launcher - Connected to Master");
        // base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        // base.OnJoinedLobby();
        Debug.Log("@Launcher - Joined Lobby");
    }



    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        Debug.Log("@Launcher - Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        
    }
}
