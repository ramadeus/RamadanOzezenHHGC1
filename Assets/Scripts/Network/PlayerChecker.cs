using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerChecker : MonoBehaviour {
    // ıı
    [SerializeField] TMP_Text readyText;
    private void Start()
    {
        StartCoroutine(CheckGameReady());

    }
    IEnumerator CheckGameReady()
    {
        if(PhotonNetwork.PlayerList.Length == 2)
        {
            StartCoroutine(StartTheGame());
        } else
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(CheckGameReady());

        }
    }
    IEnumerator StartTheGame()
    {
        for(int i = 3; i > 0; i--)
        {
           readyText.text = "Game is starting in " + i;
            yield return new WaitForSeconds(1);
        }

        LevelLoader.LoadNextLevelAsync(); 
    }
}
