using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneManager : MonoBehaviour
{
    // ıı
    public static InGameSceneManager instance;
    public Transform ground;
    public Timer timer;
    public Transform[] spawnPointsLocal;
    public Transform[] spawnPointsRemote;
    private void Awake()
    {
        instance = this;
       
    }
}
