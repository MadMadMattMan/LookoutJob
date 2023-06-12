using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospUIController : MonoBehaviour
{
    [Header("Camera Covers")]
    public GameObject StaticVideoT;
    public GameObject FixingAnomalyT;
    public GameObject NoAnomaliesFoundT;
    public GameObject NoSignalColorsT;
    public static GameObject StaticVideo;
    public static GameObject FixingAnomaly;
    public static GameObject NoAnomaliesFound;
    public static GameObject NoSignalColors;

    [Header("General UI")]
    public GameObject ReportingUI;
    public GameObject ReportButton;

    private void Start()
    {
        StaticVideo = StaticVideoT;
        FixingAnomaly = FixingAnomalyT;
        NoAnomaliesFound = NoAnomaliesFoundT;
        NoSignalColors = NoSignalColorsT;

        StaticVideo.SetActive(false);
        FixingAnomaly.SetActive(false);
        NoAnomaliesFound.SetActive(false);
        NoSignalColors.SetActive(false);

        ReportingUI.SetActive(false);
    }

    public void ReportingButton()
    {
        ReportingUI.SetActive(true);
        ReportButton.SetActive(false);
    }

    public void CancelButton()
    {
        ReportingUI.SetActive(false);
        ReportButton.SetActive(true);
    }

}
