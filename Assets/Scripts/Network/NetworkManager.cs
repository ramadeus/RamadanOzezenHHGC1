using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    // ıı
    [SerializeField] ScriptableObjectClientInfo localClientInfo;

    //private void OnEnable()
    //{
    //    EventsManager.OnCreateRoom += CreateRoomAndJoin;
    //    EventsManager.OnJoinRoom += JoinRoom;
    //    print("onenable");
    //}
    //private void OnDisable()
    //{
    //    EventsManager.OnCreateRoom -= CreateRoomAndJoin;
    //    EventsManager.OnJoinRoom -= JoinRoom;
    //    print("ondisable");
    //}
    public static NetworkManager instance;
    private void Awake()
    {
        instance = this;
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

    public void InitializePlayer()
    {
        GameObject player = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity,0,null);
        player.GetComponent<PhotonView>().Owner.NickName = PlayerPrefs.GetString("nickname");
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
        print("joinedroom");
        PhotonNetwork.LoadLevel("2-Room"); 
    }
    public override void OnLeftRoom()
    {
    }
    public override void OnLeftLobby()
    { 
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    { 
    }
}
