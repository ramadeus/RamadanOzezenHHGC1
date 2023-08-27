using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPersistent: MonoBehaviour {
    // ıı
    int playerId;
    PhotonView pw;
    private void Start()
    {
        playerId = PhotonNetwork.LocalPlayer.ActorNumber;
        pw = GetComponent<PhotonView>();
        if(pw.IsMine)
        {
            GameManager.instance.pw.RPC("PlayerReady", RpcTarget.All, playerId);
        }
    }
}
