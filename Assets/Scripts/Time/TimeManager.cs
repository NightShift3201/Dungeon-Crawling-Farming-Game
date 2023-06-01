using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{

    [SerializeField]
    Light globalLight;
    [SerializeField]
    GameTimestamp timestamp;
    public float timescale;
    [SerializeField]
    TextMeshProUGUI time;
    [SerializeField]
    TextMeshProUGUI day;


    void Start()
    {
        timestamp = new GameTimestamp();
        StartCoroutine(TimeUpdate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator TimeUpdate(){
        while(true){
            yield return new WaitForSeconds(1/timescale);
            Tick();
            UpdateUI();
        }
    }

    public void Tick(){
        timestamp.UpdateClock();
    }

    public void UpdateUI()
    {
        string amPm = "AM";
        int hour = timestamp.hour;

        if (hour >= 12)
        {
            amPm = "PM";
            if (hour > 12)
            {
                hour -= 12;
            }
        }
        if(hour==0){
            hour=12;
        }

        time.text = hour + ":" + timestamp.minute.ToString("00") + " " + amPm;
        day.text = timestamp.season.ToString() +" "+timestamp.day;
    }

    private void UpdateGlobalLightIntensity()
    {
        float currentIntensity = globalLight.intensity;

        // Get the current hour
        int currentHour = timestamp.hour;

        if (currentHour >= 18 && currentHour < 21)
        {
            if (currentIntensity > 0.4f)
            {
                currentIntensity -= ((1/timescale)/180)*0.4f/0.3f;
                globalLight.intensity = currentIntensity;
            }
        }
        // Brightening
        else if (currentHour >= 3 && currentHour < 6)
        {
            if (currentIntensity < 1f)
            {
                currentIntensity += ((1/timescale)/180)*0.4f;
                globalLight.intensity = currentIntensity;
            }
        }

        globalLight.intensity = currentIntensity;
    }
}
