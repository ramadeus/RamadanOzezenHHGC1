using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharSelection : MonoBehaviour
{
    // ıı
    [SerializeField] ScriptableObjectCharSelection chars;
    [SerializeField] Button buttonStart;
    [SerializeField] ItemSlot[] slots;
    private void OnEnable()
    {
        EventsManager.OnCharSelectionCheck += OnCharSelect;
        AudioManager.InitializeButtonClickSounds(gameObject);
    }
    private void OnDisable()
    {
        EventsManager.OnCharSelectionCheck -= OnCharSelect;

    }
    public void OnCharSelect()
    {
        if(GetAllSlotsReady())
        {
            buttonStart.interactable = true; 
            SetSelectedChars();
        } else
        {
            buttonStart.interactable = false;
            ClearSelectedChars();
        } 
    }

    private void ClearSelectedChars()
    {
        chars.selectedChars.Clear();
    }

    private void SetSelectedChars()
    {
        ClearSelectedChars();
        for(int i = 0; i < slots.Length; i++)
        {
            chars.selectedChars.Add(slots[i].currentItem.GetComponent<DragDrop>().charType);
        }
    }

    private bool GetAllSlotsReady()
    {
        bool ready = true;
        for(int i = 0; i < slots.Length; i++)
        {
            if(slots[i].currentItem == null)
            {
                ready = false;
                break;
            }
        }
        return ready;
    }
    public void LoadScene()
    {
        LevelLoader.LoadNextLevelAsync();

        //PhotonNetwork.LoadLevel("2-Room");
    }
}
