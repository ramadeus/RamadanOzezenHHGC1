using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelectionChecker : MonoBehaviour
{
    // ıı
    private void Start()
    { 
        GameManager.instance.pw.RPC("PlayerSelectedChar", RpcTarget.All,null); 
    }
}
