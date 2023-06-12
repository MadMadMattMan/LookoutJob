using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraControllerHosp : MonoBehaviour
{
    public GameObject Camera1;
    public GameObject Camera2;
    public GameObject Camera3;
    public GameObject Camera4;

    public TextMeshProUGUI CamNumText;
    public TextMeshProUGUI CamRoomText;

    public static int CameraNum = 1;
    public string[] RoomName = new string[] {"Reception", "Hallway", "Morgue", "Hospital Ward"};
    public int maxCameras;
    public int currentCamera;

    private void Start()
    {
        maxCameras = RoomName.Length;
        Debug.Log(maxCameras);
        CameraRefresh();
    }

    private void Update()
    {
        currentCamera = CameraNum;
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
        }
    }

    public void NextButtonClicked()
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

    public void PrevButtonClicked()
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
            Camera4.SetActive(false);
        }
        if (CameraNum == 2)
        {
            Camera1.SetActive(false);
            Camera2.SetActive(true);
            Camera3.SetActive(false);
            Camera4.SetActive(false);
        }
        if (CameraNum == 3)
        {
            Camera1.SetActive(false);
            Camera2.SetActive(false);
            Camera3.SetActive(true);
            Camera4.SetActive(false);
        }
        if (CameraNum == 4)
        {
            Camera1.SetActive(false);
            Camera2.SetActive(false);
            Camera3.SetActive(false);
            Camera4.SetActive(true);
        }

        CamNumText.text = "Cam " + CameraNum;
        CamRoomText.text = RoomName[CameraNum-1];
        StartCoroutine(StaticPlayer());
    }

    public static IEnumerator StaticPlayer()
    {
        HospUIController.StaticVideo.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        HospUIController.StaticVideo.SetActive(false);
    }

    private void LostSignal()
    {
        if (CameraNum == 1)
        {
            HospUIController.NoSignalColors.SetActive(true);
        }
        else
        {
            HospUIController.NoSignalColors.SetActive(false);
        }
    }
}
