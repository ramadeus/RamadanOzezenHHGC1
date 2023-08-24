using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRoom : MonoBehaviour
{
    // ıı

    [SerializeField] TMP_Text infoTest;

    private void Start()
    {
        NetworkManager.instance.InitializePlayer();
    }
}
