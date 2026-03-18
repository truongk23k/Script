using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TimerManager : Singleton<TimerManager>
{
    [LunaPlaygroundField(fieldSection: "Timer Settings")]
    public float timer = 60;
    public TMP_Text timerText;
    private bool IsStart = false;
    [LunaPlaygroundField(fieldSection: "Timer Settings")]
    public bool haveTimer = true;
    [LunaPlaygroundField(fieldSection: "Timer Settings")]
    public bool isShowUiTimer = true;
    public Slider slider;
    [SerializeField] private bool isFormatTimeMMSS = true;
    [SerializeField] private int timeShowWarning = 5;
    [SerializeField] private Warning warning;
    private bool isShowWarning = false;
    [SerializeField] private List<GameObject> uiElements;

    private float maxTimer; // L?u max value cho slider
    
    void Start()
    {
        maxTimer = timer; // L?u giá tr? ban ??u
        
        if (slider)
        {
            slider.maxValue = timer;
            slider.value = timer;
        }
        if (!haveTimer)
        {
            gameObject.SetActive(false);
            return;
        }
        if (uiElements != null && uiElements.Count > 0)
        {
            foreach (var uiElement in uiElements)
            {
                uiElement.SetActive(isShowUiTimer);
            }
        }
        if (timerText) SetTime(timer);
    }

    void Update()
    {
        if (!IsStart)
            if (Input.GetMouseButtonDown(0))
            {
                IsStart = true;
            }
        if (IsStart && timer >= 0)
        {
            timer -= Time.deltaTime;
            
            if (timerText) SetTime(timer >= 0 ? timer : 0);
            
            if (slider) slider.value = timer;
            if (timer <= timeShowWarning && warning && !isShowWarning)
            {
                isShowWarning = true;
                warning.gameObject.SetActive(true);
            }
            if (timer <= 0)
            {
                //GameManager.Instance.GameOver();
            }
        }
    }

    private void SetTime(float time)
    {
        if (isFormatTimeMMSS)
        {

            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            timerText.text = (time >= 10 ? "" : "0") + Mathf.FloorToInt(time).ToString();
        }
    }

    public void StopTimer()
    {
        IsStart = false;
    }

    public void ResumeTimer()
    {
        IsStart = true;
    }

    public bool GetIsTimerRunning() => IsStart;

    public float GetRemainingTime() => Mathf.Max(0, timer);
}
