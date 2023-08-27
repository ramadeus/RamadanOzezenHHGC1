using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRoom : MonoBehaviour
{
    // ıı

    public PlayerInfo[]  playerInfos ; 
    private void Start()
    {

        for(int i = 0; i < playerInfos.Length; i++)
        {
            InitializeInfos(i);
        }

    }
    private void OnEnable()
    {
        EventsManager.OnCollectedCollectible += OnCollectedInfoChanged;

    }
    private void OnDisable()
    {
        EventsManager.OnCollectedCollectible -= OnCollectedInfoChanged;
    }

    private void OnCollectedInfoChanged(int playerId)
    {
        UpdateCollectedCount(GetPlayer(playerId));
    }

    private void UpdateCollectedCount(PlayerInfo player)
    {
        int totalWood = (int.Parse(player.collectedWood.text));
        totalWood++;
        player.collectedWood.text = totalWood.ToString();
    }

    PlayerInfo GetPlayer(int playerId)
    {
        for(int i = 0; i < playerInfos.Length; i++)
        {
            if(playerInfos[i].playerId == playerId)
            {
                return playerInfos[i];
            }
        }
        return null;
    }
    void InitializeInfos(int playerIndex)
    {
        playerInfos[playerIndex].playerId = PhotonNetwork.PlayerList[playerIndex].ActorNumber;
        playerInfos[playerIndex].playerName.text = PhotonNetwork.PlayerList[playerIndex].NickName;
        playerInfos[playerIndex].collectedWood.text = 0.ToString();
    }
    [Serializable]
    public class PlayerInfo
    {
        public int playerId;
        public TMP_Text playerName;
        public TMP_Text collectedWood;
    }
}
