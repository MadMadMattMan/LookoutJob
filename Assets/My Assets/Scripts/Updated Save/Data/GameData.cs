using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int Shifts;
    public int ShiftsCompleted;
    public int ShiftsFailed;
    public int AnomaliesFixed;

    //the values defined in this constructor will be default values
    //what the stats starts with when there is no data to load
    public GameData()
    {
        Shifts = 0;
        ShiftsCompleted = 0;
        ShiftsFailed = 0;
        AnomaliesFixed = 0;
    }
}
