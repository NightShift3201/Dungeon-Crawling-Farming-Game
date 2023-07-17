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
        this.hour = hour;
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

    public static int MinutesBetween(GameTimestamp time1, GameTimestamp time2)
    {
        int minutes1 = GetTotalMinutes(time1);
        int minutes2 = GetTotalMinutes(time2);
        return Mathf.Abs(minutes1 - minutes2);
    }

    private static int GetTotalMinutes(GameTimestamp time)
    {
        int days = time.day - 1;
        int hours = time.hour;
        int minutes = time.minute;

        int totalMinutes = (days * 24 * 60) + (hours * 60) + minutes;

        int seasonOffset = (int)time.season * 30 * 24 * 60; // Offset for season change
        totalMinutes += seasonOffset;

        int yearOffset = (time.year - 1) * 4 * 30 * 24 * 60; // Offset for year change (assuming 4 seasons per year)
        totalMinutes += yearOffset;

        return totalMinutes;
    }
}
