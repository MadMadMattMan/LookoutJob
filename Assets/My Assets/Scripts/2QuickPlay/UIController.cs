using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [Header("ReportMenu")]
    public GameObject ReportButton;
    public GameObject CancelButton;
    public GameObject ReportPannel;

    public static bool ReportPannelTrue;
    public static bool ReportActive;

    private void Awake()
    {
        ReportButton.SetActive(true);
        CancelButton.SetActive(false);
        ReportPannel.SetActive(false);
        ReportPannelTrue = false;
        ReportActive = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            if (!ReportPannelTrue && ReportActive)
            {
                ReportButtonClicked();
            }
            else if (ReportActive)
            {
                CancelButtonClicked();
            }
        }
    }

    //For Arrow Camera Movement See CameraController.cs in Cameras GameObject

    public void ReportButtonClicked()
    {
        ReportButton.SetActive(false);
        CancelButton.SetActive(true);
        ReportPannel.SetActive(true);
        ReportPannelTrue = true;
    }
    public void CancelButtonClicked()
    {
        ReportButton.SetActive(true);
        CancelButton.SetActive(false);
        ReportPannel.SetActive(false);
        ReportPannelTrue = false;
    }
    public void MainMenuGameOver()
    {
        Destroy(GameObject.Find("System Managers"));
        SceneManager.LoadScene("Main Menu");
    }
}
