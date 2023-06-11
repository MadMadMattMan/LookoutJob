using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Overseer : MonoBehaviour
{
    public float AnomalyCounterCounter = 0;
    public int AnomalyWarningNumber = 0;
    public bool AnomalyCounterBool = false;
    public bool GameOver;
    public static bool SmilerDeath = false;

    public TextMeshProUGUI EvacTimeTemp;
    public static TextMeshProUGUI EvacTime;
    public static string HoursString;

    public TextMeshProUGUI AnomalyFixTemp;
    public static TextMeshProUGUI AnomalyFix;

    public TextMeshProUGUI DeathCauseTemp;
    public static TextMeshProUGUI DeathCause;

    public static bool IsWalkingSFX = false;
    public AudioSource DoorKnockingSFX;
    public AudioSource BreathingSFX;
    public float KnockTimer = 0;
    public static bool IsKnocked = false;

    public TextMeshProUGUI Winner;
    public static TextMeshProUGUI AnomaliesFixedWin;

    public static bool done = false;

    private void Awake()
    {
        EvacTime = EvacTimeTemp;
        AnomalyFix = AnomalyFixTemp;
        DeathCause = DeathCauseTemp;
        AnomaliesFixedWin = Winner;
    }

    void FixedUpdate()
    {
        if (AnomalyController.AnomalyCounter > 6)
        {
            AnomalyCounterCounter += Time.deltaTime;
            AnomalyCounterBool = true;
        }
        else if (AnomalyWarningNumber == 1)
        {
            AnomalyCounterCounter = 10;
            AnomalyCounterBool = false;
        }
        else if (AnomalyWarningNumber == 2) 
        { 
            AnomalyCounterCounter = 20;
            AnomalyCounterBool = false;
        }

        if (AnomalyCounterCounter > 0 && AnomalyWarningNumber == 0)
        {
            AnomalyWarningNumber = 1;
        }
        else if (AnomalyCounterCounter > 10 && AnomalyWarningNumber == 1)
        {
            AnomalyWarningNumber = 2;
        }


        if (AnomalyCounterCounter > 30)
        {
            AnomalyController.GameOver = true;
        }

        GameOver = AnomalyController.GameOver;

        if (IsWalkingSFX)
        {
            KnockTimer += Time.deltaTime;
            if (KnockTimer >= 41 && !IsKnocked)
            {
                DoorKnockingSFX.Play();
                IsKnocked = true;
                BreathingSFX.Play();
            }
        }

        if (AnomalyController.GameOver)
        {
            BreathingSFX.Stop();
            DoorKnockingSFX.Stop();
        }
    }

    public static void SecondsConverter()
    {
        if (AnomalyController.globalSeconds == 0)
        {
            AnomalyController.SecondsTimer = "00";
        }
        else if (AnomalyController.globalSeconds < 1)
        {
            AnomalyController.SecondsTimer = "01";
        }
        else if (AnomalyController.globalSeconds < 2)
        {
            AnomalyController.SecondsTimer = "02";
        }
        else if (AnomalyController.globalSeconds < 3)
        {
            AnomalyController.SecondsTimer = "03";
        }
        else if (AnomalyController.globalSeconds < 4)
        {
            AnomalyController.SecondsTimer = "04";
        }
        else if (AnomalyController.globalSeconds < 5)
        {
            AnomalyController.SecondsTimer = "05";
        }
        else if (AnomalyController.globalSeconds < 6)
        {
            AnomalyController.SecondsTimer = "06";
        }
        else if (AnomalyController.globalSeconds < 7)
        {
            AnomalyController.SecondsTimer = "07";
        }
        else if (AnomalyController.globalSeconds < 8)
        {
            AnomalyController.SecondsTimer = "08";
        }
        else if (AnomalyController.globalSeconds < 9)
        {
            AnomalyController.SecondsTimer = "09";
        }
        else
        {
            AnomalyController.SecondsTimer = AnomalyController.globalSeconds.ToString("00");
        }
    }

    public static void GameOverMethod()
    {
        GameObject.Find("SaveManager").GetComponent<DataSaveManager>().SaveGame();

        SecondsConverter();

        if (SmilerDeath)
        {
            DeathCause.text = "Signal was lost";
        }
        else
        {
            DeathCause.text = "Too many anomalies active";
        }
        EvacTime.text = "Time of failure: " + HoursString + ":" + AnomalyController.SecondsTimer;
        AnomalyFix.text = "Anomalies fixed: " + AnomalyController.AnomaliesFixed.ToString();
    }

    public static void GameWinMethod()
    {
        while (!done)
        {
            GameObject.Find("SaveManager").GetComponent<DataSaveManager>().SaveGame();
            AnomaliesFixedWin.text = "You fixed " + AnomalyController.AnomaliesFixed.ToString() + " anomalies";
            done = true;
        }
    }
}
