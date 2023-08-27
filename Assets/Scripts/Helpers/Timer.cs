using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // ıı
    float elapsed;
    [SerializeField] TMP_Text timer;
    public void InitializeTimer(float duration)
    {
        elapsed = 0;
        StartCoroutine(Counter(duration));
    }
    IEnumerator Counter(float duration)
    {
        while(elapsed <duration)
        {
            yield return new WaitForSeconds(1);
            elapsed++;
            UpdateTimerText(duration); 
        }
        SendGameFinishedInfo();
    }

    private void SendGameFinishedInfo()
    {
        EventsManager.OnGameFinished?.Invoke();
    }

    private void UpdateTimerText(float duration)
    {
        float timeCounter = duration - elapsed;
        
        string totalTime = "";
        float totalMinutes = Mathf.FloorToInt(timeCounter / 60);
        float totalSeconds = timeCounter % 60;
        totalTime = $"{totalMinutes.ToString("00")} : {totalSeconds.ToString("00")}";
        
        timer.text = totalTime;
    } 
}
