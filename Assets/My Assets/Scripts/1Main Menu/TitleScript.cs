using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour
{
    public Animator Title;
    public TextMeshProUGUI SelectedText;
    public string PlayState = "Quick Play";
    public static bool QuickPlayWin;
    public static bool StatsBool;

    public TextMeshProUGUI QuickPlayButtonText;

    public GameObject ResetStats;
    public GameObject YesToReset; 

    private void Start()
    {
        QuickPlayWin = false;
        StatsBool = false;
    }

    void Awake()
    {
        Title.SetInteger("Fade", 0);
        PlayState = "Quick Play";

        if (QuickPlayWin)
        {
            QuickPlayButtonText.color = Color.green;
        }
        else
        {
            QuickPlayButtonText.color = Color.red;
        }
    }

    public void GameStartButtonPressed()
    {
        if (Title.GetInteger("Fade") == 0) 
        {
            Title.SetInteger("Fade", 1);
            PlayState = "Quick Play";
        }
        else
        {
            if (PlayState == "Quick Play")
            {
                SceneManager.LoadScene("Quick Play");
            }
        }
    }

    public void QuickPlayButton()
    {
        PlayState = "Quick Play";
        SelectedText.text = "Selected mode: " + PlayState;
    }

    public void StatsButtonPressed()
    {
        GameObject.Find("SaveManager").GetComponent<DataSaveManager>().LoadGame();
        if (!StatsBool)
        {
            StatsBool = true;
            Title.SetBool("Stats", true);
        }
        else
        {
            StatsBool = false;
            Title.SetBool("Stats", false);
        }
    }

    public void BackButtonStartGame()
    {
        Title.SetInteger("Fade", 0);
    }


    public void ResetStatsButton()
    {
        YesToReset.SetActive(true);
    }
    public void SureResetStats()
    {
        YesToReset.SetActive(false);
    }
}
