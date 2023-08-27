using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PersistentPlayerInitializer : MonoBehaviour
{
    // ıı
    private void Start()
    {
        GameManager.instance.InitializePlayer();
    }

}
