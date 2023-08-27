using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader: MonoBehaviour {
    // ıı
    public static int currentLevel = 1;
    public static LevelLoader instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            currentLevel = 1;
        } else
        {
            Destroy(gameObject);
        }
    }

    public static void LoadNextLevel()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
            currentLevel++;

        if(PhotonNetwork.IsMasterClient)
        { 
            PhotonNetwork.LoadLevel(currentLevel);
        }
    }
    public static void LoadNextLevelAsync()
    {
        PhotonNetwork.AutomaticallySyncScene = false; 
        currentLevel++;
        PhotonNetwork.LoadLevel(currentLevel);

    }
    public static void LoadLevelAsync(int levelIndex)
    {
        PhotonNetwork.AutomaticallySyncScene = false; 
        currentLevel = levelIndex;
        PhotonNetwork.LoadLevel(levelIndex);

    }
}
