using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIResult : MonoBehaviour
{
    // ıı
    [SerializeField] TMP_Text result;

    [SerializeField] TMP_Text player1Nickname;
    [SerializeField] TMP_Text player1Wood;

    [SerializeField] TMP_Text player2Nickname;
    [SerializeField] TMP_Text player2Wood;

    private void Start()
    {
        InitializeResult();
        AudioManager.InitializeButtonClickSounds(gameObject);
    }

    private void InitializeResult()
    {
        string player1Nickname = GameManager.instance.gameStatus.players[0].playerName;
        int player1Id= GameManager.instance.gameStatus.players[0].playerId;
        int player1Wood = GameManager.instance.gameStatus.players[0].collectedWood;

        string player2Nickname = GameManager.instance.gameStatus.players[1].playerName; 
        int player2Wood = GameManager.instance.gameStatus.players[1].collectedWood;
        string resultText = "";
        bool isItDraw = player1Wood == player2Wood;
        if(!isItDraw)
        {
            bool isPlayer1Win = player1Wood > player2Wood;
            if( player1Id == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                resultText =isPlayer1Win? "You Win!" : "You Lose";
            } else  
            {
                resultText = isPlayer1Win ? "You Lose!" : "You Win";

            }
        } else
        {
            resultText = "Draw!";
        }
        result.text = resultText;
        this.player1Nickname.text = player1Nickname;
        this.player2Nickname.text = player2Nickname;

        this.player1Wood.text = player1Wood.ToString();
        this.player2Wood.text = player2Wood.ToString();

    }
    public void PlayAgain()
    {
        LevelLoader.LoadLevelAsync(3);
    }
    public void MainMenu()
    {
        PhotonNetwork.LeaveRoom();

        LevelLoader.LoadLevelAsync(1);

    }
}
