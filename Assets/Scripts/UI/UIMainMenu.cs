using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu: MonoBehaviour {
    // ıı

    [SerializeField] TMP_Text connectionInfoText;
    private void OnEnable()
    {
        EventsManager.OnReadyToJoin += ReadyToJoin;
    }


    private void OnDisable()
    {
        EventsManager.OnReadyToJoin -= ReadyToJoin;
    }
    private void ReadyToJoin()
    {
        Button[] allButtons = GetComponentsInChildren<Button>();

        for(int i = 0; i < allButtons.Length; i++)
        {
            allButtons[i].interactable = true;
        }
        connectionInfoText.text = "Connected";
        Destroy(connectionInfoText, 3);
    }
    public void JoinRoom()
    {
        print("join");
        EventsManager.OnJoinRoomRequested?.Invoke();
    }
    public void CreateRoom()
    {
        print("create");
        EventsManager.OnCreateRoomRequested?.Invoke();
    }
}
