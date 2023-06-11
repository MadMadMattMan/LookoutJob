using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Camera1;
    public GameObject Camera2;
    public GameObject Camera3;
    public GameObject Camera4;

    public TextMeshProUGUI CamNumText;
    public TextMeshProUGUI CamRoomText;

    public Animator SmilerAni;
    public Animator Camera4ZoomSmiler;

    public static int CameraNum = 1;
    public string[] RoomName = new string[] {"Kitchen", "Lounge", "Bedroom", "Bathroom"};

    void Start()
    {
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

            if (SmilerAni.GetBool("isOpen_Obj_1") && CameraNum == 4)
            {
                Camera4ZoomSmiler.SetBool("IsSmilerAnomaly", true);
            }

            if (AnomalyController.CamNoSignal > 0)
            {
                LostSignal();
            }
        }
    }

    public void NextButtonClicked()
    {
        if (!SmilerAni.GetBool("isOpen_Obj_1"))
        {
            if (CameraNum < 4)
            {
                CameraNum++;
            }
            else
            {
                CameraNum = 1;
            }
            CameraRefresh();
        }
        else
        {
            if (CameraNum != 4)
            {
                if (CameraNum < 4)
                {
                    CameraNum++;
                }
                else
                {
                    CameraNum = 1;
                }
                CameraRefresh();
            }
        }
    }

    public void PrevButtonClicked()
    {
        if (!SmilerAni.GetBool("isOpen_Obj_1"))
        {
            if (CameraNum > 1)
            {
                CameraNum--;
            }
            else
            {
                CameraNum = 4;
            }
            CameraRefresh();
        }
        else 
        { 
            if (CameraNum != 4)
            {
                if (CameraNum > 1)
                {
                    CameraNum--;
                }
                else
                {
                    CameraNum = 4;
                }
                CameraRefresh();
            }
        }
    }

    public  void CameraRefresh()
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
