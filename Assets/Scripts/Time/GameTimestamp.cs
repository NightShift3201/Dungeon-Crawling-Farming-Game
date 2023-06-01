using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameTimestamp
{

    public int year;
    public enum Season{
        Spring,
        Summer,
        Fall,
        Winter
    }

    public Season season;
    public int day;
    public int hour;
    public int minute;

    public GameTimestamp(int year, Season season, int day, int hour, int minute){
        this.year = year;
        this.season = season;
        this.day = day;
        this.minute =minute;
    }

    public GameTimestamp(){
        year = 1;
        season = Season.Spring;
        day = 1;
        hour = 6;
        minute = 0;
    }

    public void UpdateClock(){
        minute++;
        if(minute>=60){
            minute = 0;
            hour++;
        }
        if(hour>=24){
            hour = 0;
            day++;
        }

        if(day>30){
            day = 1;
            if(season == Season.Winter){
                season = Season.Spring;
                year++;
            }
            else
                season++;
        }
    }
}
