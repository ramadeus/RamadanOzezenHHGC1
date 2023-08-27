using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager: MonoBehaviourPunCallbacks {
    // ıı
    public static NetworkManager instance;
    [SerializeField] ScriptableObjectClientInfo localClientInfo;
    private void Awake()
    {
        if(instance == null)
        {
        instance = this;
        } else
        {
            Destroy(gameObject);
        }
        EventsManager.OnCreateRoomRequested += CreateRoomAndJoin;
        EventsManager.OnJoinRoomRequested += JoinRoom;
    }
    private void OnDestroy()
    {
        EventsManager.OnCreateRoomRequested -= CreateRoomAndJoin;
        EventsManager.OnJoinRoomRequested -= JoinRoom;
    }
    private void Start()
    {
        PhotonNetwork.NickName = localClientInfo.nickname;
        PhotonNetwork.ConnectUsingSettings();
    }
    private void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    private void CreateRoomAndJoin()
    {
        string roomId = GetRandomRoomName();
        PhotonNetwork.JoinOrCreateRoom(roomId, new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);
    }

    private string GetRandomRoomName()
    {
        return UnityEngine.Random.Range(0, int.MaxValue).ToString();
    } 
    public override void OnConnectedToMaster()
    {
        print(PhotonNetwork.NickName + " logged in.");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        print(PhotonNetwork.NickName + " joined to lobby.");
        EventsManager.OnReadyToJoin?.Invoke();

    }
    public override void OnJoinedRoom()
    {
        if(PhotonNetwork.IsMasterClient)
        { 
            PhotonNetwork.InstantiateRoomObject("GameManager", Vector3.zero, Quaternion.identity, default, null);
        } 
        LevelLoader.LoadNextLevelAsync(); 
    } 
    public override void OnLeftRoom()
    {
        print("You left the room.");
    }
    public override void OnLeftLobby()
    {
        print("You left the lobby.");
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        print("Player entered the room.");

    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {

        print("Player left the room.");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("Random join failed.");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("Creating room failed.");
    }



}
