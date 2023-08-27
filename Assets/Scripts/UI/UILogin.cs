using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILogin : MonoBehaviour
{
    // ıı
    [SerializeField] TMP_InputField inputField;
    [SerializeField] ScriptableObjectClientInfo clientInfo;
    private void OnEnable()
    {
        
        if(PlayerPrefs.HasKey("nickname"))
        {
            inputField.text = PlayerPrefs.GetString("nickname");
        }
        AudioManager.InitializeButtonClickSounds(gameObject);
    }
    public void SaveNickname()
    {
        PlayerPrefs.SetString("nickname", inputField.text);
        PlayerPrefs.Save();
        Login();
    }

    private void Login()
    {
        clientInfo.nickname = inputField.text;
        SceneManager.LoadScene("1-MainMenu");
    }
}
