using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraControllerHosp : MonoBehaviour
{
    public GameObject Camera1;
    public GameObject Camera2;
    public GameObject Camera3;

    public TextMeshProUGUI CamNumText;
    public TextMeshProUGUI CamRoomText;

    public static int CameraNum = 1;
    public string[] RoomName = new string[] {"Reception", "Hallway", "Morgue"};
    private int maxCameras;

    private void Start()
    {
        maxCameras = RoomName.Length;
        Debug.Log(maxCameras);
        CameraRefresh();
    }

    private void Update()
    {
        if (!AnomalyController.GameOver)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                NextButtonClicked();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                PrevButtonClicked();
            }

            if (AnomalyController.CamNoSignal > 0)
            {
                LostSignal();
            }
        }
    }

    private void NextButtonClicked()
    {
        if (CameraNum < maxCameras)
        {
            CameraNum++;
        }
        else
        {
            CameraNum = 1;
        }
        CameraRefresh();
    }

    private void PrevButtonClicked()
    {
        if (CameraNum > 1)
        {
            CameraNum--;
        }
        else
        {
            CameraNum = maxCameras;
        }
        CameraRefresh();
    }

    private void CameraRefresh()
    {
        if (CameraNum == 1)
        {
            Camera1.SetActive(true);
            Camera2.SetActive(false);
            Camera3.SetActive(false);
        }
        if (CameraNum == 2)
        {
            Camera1.SetActive(false);
            Camera2.SetActive(true);
            Camera3.SetActive(false);
        }
        if (CameraNum == 3)
        {
            Camera1.SetActive(false);
            Camera2.SetActive(false);
            Camera3.SetActive(true);
        }

        CamNumText.text = "Cam " + CameraNum;
        CamRoomText.text = RoomName[CameraNum-1];
        StartCoroutine(StaticPlayer());
    }

    public static IEnumerator StaticPlayer()
    {
        AnomalyController.ScreenStatic.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        AnomalyController.ScreenStatic.SetActive(false);
    }

    private void LostSignal()
    {
        if (CameraNum == AnomalyController.CamNoSignal)
        {
            AnomalyController.ColorNoSignal.SetActive(true);
        }
        else
        {
            AnomalyController.ColorNoSignal.SetActive(false);
        }
    }
}
