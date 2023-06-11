using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatsUpdater : MonoBehaviour, IDataSave
{
    public int Shifts;
    public int ShiftsCompleted;
    public int ShiftsFailed;
    public int AnomaliesFixed;

    public bool QuickPlayWon;

    public TextMeshProUGUI ShiftsTMP;
    public TextMeshProUGUI ShiftsCompletedTMP;
    public TextMeshProUGUI ShiftsFailedTMP;
    public TextMeshProUGUI AnomaliesFixedTMP;

    public GameObject QuickPlayButton;

    public void Update()
    {
        TextUpdate();
        SetButtonBools();

        if (Input.GetKeyUp(KeyCode.F10))
        {
            GameObject.Find("SaveManager").GetComponent<DataSaveManager>().SaveGame();
        }
        if (Input.GetKeyUp(KeyCode.F9))
        {
            GameObject.Find("SaveManager").GetComponent<DataSaveManager>().LoadGame();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shifts++;
            ShiftsCompleted+=2;
        }
    }

    private void TextUpdate()
    {
        ShiftsTMP.text = "Shift Count: " + Shifts.ToString();
        ShiftsCompletedTMP.text = "Shifts Completed: " + ShiftsCompleted.ToString();
        ShiftsFailedTMP.text = "Shifts Failed: " + ShiftsFailed.ToString();
        AnomaliesFixedTMP.text = "Anomalies Fixed: " + AnomaliesFixed.ToString();
    }
    private void SetButtonBools()
    {
        if (QuickPlayWon)
        {
            QuickPlayButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            QuickPlayButton.GetComponent<Image>().color = Color.red;
        }
    }

    public void AnomalyFixed()
    {
        AnomaliesFixed++;
    }

    public void LoadData(GameData data)
    {
        Shifts = data.Shifts;
        ShiftsCompleted = data.ShiftsCompleted;
        ShiftsFailed = data.ShiftsFailed;
        AnomaliesFixed = data.AnomaliesFixed;
        TextUpdate();
    }

    public void SaveData(ref GameData data)
    {
        data.Shifts = Shifts;
        data.ShiftsCompleted = ShiftsCompleted;
        data.ShiftsFailed = ShiftsFailed;
        data.AnomaliesFixed = AnomaliesFixed;
    }
}
