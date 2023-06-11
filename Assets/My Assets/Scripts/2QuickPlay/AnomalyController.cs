using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnomalyController : MonoBehaviour, IDataSave
{
    [Header("Misc")]
    [Header("Start")]
    public float globalTime = 0;
    public static string SecondsTimer;
    public static float globalSeconds;
    public static int AnomalyCounter = 0;
    public static int AnomaliesFixed = 0;
    private int MovementAnoamalyActiveNum;
    public TextMeshProUGUI ClockUI;
    public TextMeshProUGUI StabilityUI;
    public List<string> MovementAnomalyActiveList = new List<string>();
    private int ExtraAnomalyActiveNum;
    public List<string> ExtraAnomalyActiveList = new List<string>();
    private int AlterAnomalyActiveNum;
    public List<string> AlterAnomalyActiveList = new List<string>();
    private int PaintingAnomalyActiveNum;
    public List<string> PaintingAnomalyActiveList = new List<string>();
    private int IntruderAnomalyActiveNum;
    public List<string> IntruderAnomalyActiveList = new List<string>();
    private int ElectricalAnomalyActiveNum;
    public List<string> ElectricalAnomalyActiveList = new List<string>();

    public GameObject TempStatic;
    public static GameObject ScreenStatic;
    public GameObject TempSignal; 
    public static GameObject ColorNoSignal;
    public static int CamNoSignal;
    public static bool IsNoSignal;

    private float RandomTimer = 0;

    [Header("Audio Clips")]
    public AudioClip KickSFX;
    public AudioClip StaticSFX;
    public AudioClip SmilerDeathSFX;
    //BG Music
    public AudioSource one;
    public AudioSource two;
    public AudioSource three;

    [Header("End")]
    public static bool GameOver;
    public GameObject GameOverScreen;
    public Animator GameOverAni;
    public static bool Win;
    public GameObject GameWinScreen;
    private Animator GameWinAni;

    [Header("Kitchen Anomalies")]
    [Header("Stool")]
    public GameObject StoolNormal;
    public GameObject StoolDown;
    public GameObject StoolUp;
    private bool StoolActive;
    [Header("Rack")]
    private bool RackIsNormal;
    public GameObject RackNormal;
    public GameObject RackEmpty;
    private Vector3 RackNormalPos = new Vector3(-4.3968284f, 1.1082119f, -0.802781f);
    private Vector3 RackAnomalyPos = new Vector3(-4.3968284f, 1.1082119f, 0.102781f);
    [Header("Chairs")]
    public GameObject ExtraChair;
    private bool ExtraChairActive;
    public GameObject Chair;
    private Vector3 ChairNormalPos;
    private Vector3 ChairAnomalyPos;
    private Vector3 ChairAnomalyPos2;
    [Header("Seat 2")]
    public GameObject HotdogNormal;
    public GameObject HotDogAnomaly;
    private bool HotDogAnomalyBool;
    [Header("Cheese")]
    public GameObject CheeseNormal;
    public GameObject CheeseAnomaly;
    private bool CheeseAnomalyBool;
    [Header("CookerFire")]
    public GameObject CookerFire;
    private bool CookerFireAnomalyBool;
    [Header("FireExtingusher")]
    public Animator AntiFireAnimator;
    [Header("PantryDoors")]
    public Animator PantryDoorsAni;

    [Header("Lounge Anomalies")]
    [Header("CouchSwitch")]
    public GameObject TwoCouch;
    public GameObject ThreeCouch;
    public Transform CouchSpawnPos;
    private GameObject CurrentCouch;
    private bool CouchAnomaly;

    [Header("Toilet")]
    public Transform ToiletLoungeSpawnPos;
    public GameObject Toilet;
    public GameObject ToiletSpawned;

    [Header("Fireplace")]
    public Transform FireSpawnPos;
    public GameObject Fire;
    public GameObject FireplaceFire;

    [Header("Coffee Cup")]
    public GameObject CoffeeCupLounge;
    private bool CoffeeCupLoungeTrue;
    private Vector3 DefaultCupPos = new Vector3(1.3f, 0.595f, 2);
    private Vector3 AnomalyCupPos = new Vector3(1.3f, 0.595f, 1.35f);

    [Header("TV Stand")]
    public Animator TVStand;

    [Header("Plant")]
    public GameObject LoungePlant;

    [Header("Bedroom Anomalies")]
    [Header("Slender In Bed")]
    public Transform SlenderInBedSpawnPos;
    public GameObject SlenderInBed;
    private GameObject SlenderInBedSpawned;

    [Header("RC Car")]
    public GameObject RCCar;
    private Animator RCCarAnimator;

    [Header("Drawer")]
    public GameObject Draws;
    private bool DrawersBool;

    [Header("Rug")]
    public GameObject Rug;
    private MeshRenderer RugRenderer;
    private bool RugBool;
    public Material RugMaterial;
    public Material RugAnomaly;

    [Header("Couch Pillow")]
    public GameObject Pillow;
    private Animator PillowAnimator;

    [Header("Bed Pillow")]
    public Transform BedPillow;
    private bool BedPillowBool;

    [Header("Bedside Light")]
    public GameObject BedsideLamp;
    private int BedsideLampState;

    [Header("Bathroom Anomalies")]
    [Header("Showerman")]
    public GameObject ShowerMan;
    private Animator ShowerManAnimator;

    [Header("Basket")]
    public GameObject Basket;
    private Animator BasketAnimator;

    [Header("Toilet Seat")]
    public Animator ToiletSeat;

    [Header("Outside Anomalies - Windows/Doors")]
    [Header("Front Door Smiler")]
    public Light Sun;
    public GameObject BlackoutPannel;
    public Animator FrontDoorsAni;
    public Animator Camera4Zoom;
    public Light Cam1L;
    public Light Cam2L;
    public Light Cam3L;
    public Light Cam4L;
    private float SmilerCounter;
    private int SmilerPhase;
    public AudioSource Smiler;
    public AudioSource SmilerStatic;

    [Header("Lounge Window Ghost")]
    public GameObject PumpkinKingPrefab;
    public Transform PumpkinKingSpawner;
    private GameObject PumpkinKingSpawned;

    [Header("Bushman")]
    public GameObject Bushman;
    private bool BushmanIsActive = false;


    [Header("Report System")]
    public string RoomType;
    public string AnomalyType;
    public string AnomalyFix;
    private float InternetSpeed = 5;
    private bool Fixed;
    public Button ReportButton;
    //Report Screen
    public GameObject NoAnomaliesFound;
    public GameObject FixingAnomaly;

    [Header("Report Rooms")]
    public Toggle Kitchen;
    public Toggle Lounge;
    public Toggle Bedroom;
    public Toggle Bathroom;
    [Header("Report Anomalies")]
    public Toggle MovementR;
    public Toggle ExtraR;
    public Toggle AlterR;
    public Toggle PaintingR;
    public Toggle IntruderR;
    public Toggle ElectricalR;
    //Lists for Reporting Fix
    //Kitchen
    private List<string> KitchenMovementList = new List<string>();
    private List<string> KitchenExtraList = new List<string>();
    private List<string> KitchenAlterList = new List<string>();
    private List<string> KitchenPaintingList = new List<string>();
    private List<string> KitchenIntruderList = new List<string>();
    public List<string> KitchenElectricalList = new List<string>();
    //Lounge
    private List<string> LoungeMovementList = new List<string>();
    private List<string> LoungeExtraList = new List<string>();
    private List<string> LoungeAlterList = new List<string>();
    private List<string> LoungePaintingList = new List<string>();
    private List<string> LoungeIntruderList = new List<string>();
    private List<string> LoungeElectricalList = new List<string>();
    //Bedroom
    private List<string> BedroomMovementList = new List<string>();
    private List<string> BedroomExtraList = new List<string>();
    private List<string> BedroomAlterList = new List<string>();
    private List<string> BedroomPaintingList = new List<string>();
    private List<string> BedroomIntruderList = new List<string>();
    private List<string> BedroomElectricalList = new List<string>();
    //Bathroom
    private List<string> BathroomMovementList = new List<string>();
    private List<string> BathroomExtraList = new List<string>();
    private List<string> BathroomAlterList = new List<string>();
    private List<string> BathroomPaintingList = new List<string>();
    private List<string> BathroomIntruderList = new List<string>();
    private List<string> BathroomElectricalList = new List<string>();


    private void Awake()
    {
        if (GameObject.Find("SaveManager") == null)
        {
            SceneManager.LoadScene("Main Menu");
        }
        //Misc Start
        globalTime = 0;
        globalSeconds = 0;
        ClockUI.text = "00:00";
        MovementAnomalyActiveList.Clear();
        ExtraAnomalyActiveList.Clear();
        AlterAnomalyActiveList.Clear();
        PaintingAnomalyActiveList.Clear();
        IntruderAnomalyActiveList.Clear();
        ElectricalAnomalyActiveList.Clear();
        ReportButton.GetComponentInChildren<TextMeshProUGUI>().text = "Report";
        StabilityUI.text = "Stable";
        one.Play();

        //UI
        ScreenStatic = TempStatic;
        ScreenStatic.SetActive(false);
        ColorNoSignal = TempSignal;
        ColorNoSignal.SetActive(false);

        NoAnomaliesFound.SetActive(false);
        FixingAnomaly.SetActive(false);

        //Misc End
        GameOver = false;
        Win = true;
        GameOverScreen.SetActive(false);
        GameWinScreen.SetActive(false);
        GameWinAni = GameWinScreen.GetComponent<Animator>();
        GameWinAni.SetBool("Win", false);

        //Report System
        ReportButton.interactable = true;
        Fixed = false;

        //Lists for Reporting Fix
        //Kitchen
        KitchenMovementList.Clear();
        KitchenExtraList.Clear();
        KitchenAlterList.Clear();
        KitchenPaintingList.Clear();
        KitchenIntruderList.Clear();
        KitchenElectricalList.Clear();
        //Lounge
        LoungeMovementList.Clear();
        LoungeExtraList.Clear();
        LoungeAlterList.Clear();
        LoungePaintingList.Clear();
        LoungeIntruderList.Clear();
        LoungeElectricalList.Clear();
        //Bedroom
        BedroomMovementList.Clear();
        BedroomExtraList.Clear();
        BedroomAlterList.Clear();
        BedroomPaintingList.Clear();
        BedroomIntruderList.Clear();
        BedroomElectricalList.Clear();
        //Bathroom
        BathroomMovementList.Clear();
        BathroomExtraList.Clear();
        BathroomAlterList.Clear();
        BathroomPaintingList.Clear();
        BathroomIntruderList.Clear();
        BathroomElectricalList.Clear();

        //Kitchen Anomalies
        //Stool Anomalies
        StoolDown.SetActive(false);
        StoolUp.SetActive(false);
        StoolNormal.SetActive(true);
        StoolActive = false;

        //Rack Anomalies
        RackIsNormal = true;
        RackNormal.SetActive(true);
        RackEmpty.SetActive(false);
        RackNormal.GetComponent<Transform>().position = RackNormalPos;

        //HotDog Anomalies
        HotdogNormal.SetActive(true);
        HotDogAnomaly.SetActive(false);
        HotDogAnomalyBool = false;

        //Cheese
        CheeseNormal.SetActive(true);
        CheeseAnomaly.SetActive(false);
        CheeseAnomalyBool = false;

        //CookerFire
        CookerFire.SetActive(false);
        CookerFireAnomalyBool = false;

        //AntiFire
        AntiFireAnimator.SetBool("AntiFireAnomalyBool", false);

        //Extra Chair
        ExtraChair.SetActive(false);
        ExtraChairActive = false;

        //PantryDoor
        PantryDoorsAni.SetBool("PantryDoorDefault", true);

        //Smiler
        FrontDoorsAni.SetBool("isOpen_Obj_1", false);
        Sun.intensity = 0.75f;
        Cam1L.intensity = 0.5f; Cam2L.intensity = 0.5f; Cam3L.intensity = 0.5f; Cam4L.intensity = 0.5f;
        SmilerCounter = 0;
        BlackoutPannel.SetActive(false);
        SmilerPhase = 0;
        Camera4Zoom.SetBool("IsSmilerAnomaly", false);


        //Lounge Anomalies
        //Coffee Cup
        CoffeeCupLounge.GetComponent<Transform>().position = DefaultCupPos;
        CoffeeCupLoungeTrue = false;

        //TV
        TVStand.SetBool("isOpen_Obj_1", false);

        //Lounge Toilet
        if (ToiletSpawned != null)
        {
            Destroy(ToiletSpawned);
        }
        ToiletSpawned = null;

        if (CurrentCouch != null)
        {
            Destroy(CurrentCouch);
        }
        CurrentCouch = Instantiate(ThreeCouch, CouchSpawnPos.position, CouchSpawnPos.rotation);
        CouchAnomaly = false;

        if(FireplaceFire != null)
        {
            Destroy(FireplaceFire);
        }
        FireplaceFire = null;

        //lounge PumpkinKing
        if (PumpkinKingSpawned != null)
        {
            Destroy(PumpkinKingSpawned);
        }

        //Lounge Bushman
        BushmanIsActive = false;
        Bushman.SetActive(false);

        //Lounge Plant
        LoungePlant.SetActive(true);

        //Bedroom Start
        //Slender in bed
        if (SlenderInBedSpawned != null)
        {
            Destroy(SlenderInBedSpawned);
        }

        //RC Car
        RCCarAnimator = RCCar.GetComponent<Animator>();
        RCCarAnimator.SetBool("Anomaly", false);

        //Draws
        Draws.SetActive(false);
        DrawersBool = false;

        //Rug
        RugRenderer = Rug.GetComponent<MeshRenderer>();
        RugRenderer.material = RugMaterial;
        RugBool = false;

        //Couch Pillow
        PillowAnimator = Pillow.GetComponent<Animator>();
        PillowAnimator.SetBool("Pillow Anomaly", false);

        //Bed Pillow
        BedPillow.localScale = new Vector3(1, 1, 1);
        BedPillowBool = false;

        //Bedside Lamp
        BedsideLamp.SetActive(false);
        BedsideLampState = 0;


        //Bathroom Anomalies
        //Showerman
        ShowerManAnimator = ShowerMan.GetComponent<Animator>();
        ShowerMan.SetActive(false);
        ShowerManAnimator.SetBool("Anomaly", false);

        //Basket
        BasketAnimator = Basket.GetComponent<Animator>();
        BasketAnimator.SetBool("Anomaly", false);

        //Toilet Seat
        ToiletSeat.SetBool("Anomaly", false);

        //Camera Break
        CamNoSignal = 0;
        IsNoSignal = false;

        globalSeconds = 0;

        StartCoroutine(AnomalyTimer());
    }

    private void Update()
    {
        if (!GameOver)
        {
            globalTime += Time.deltaTime;
            globalSeconds += Time.deltaTime;
            RandomTimer += Time.deltaTime;
        }

        //start spawns
        if (globalTime < 10 && globalTime > 9 && AnomalyCounter == 0)
        {
            SpawnAnomaly();
            SpawnAnomaly();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SpawnAnomaly();
        }
        if (Input.GetKeyDown(KeyCode.P))                                                              //Testing
        {
            globalTime = 355;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            AnomalyCounter = 100;
            SpawnAnomaly();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            AnomalyCounter = 2;
            SpawnAnomaly();
        }

        int RandomInt = Random.Range(0, 11);
        string RandomStr = "";
        if (RandomInt == 0)
        {
            RandomStr = "11";
        }
        else if (RandomInt == 1)
        {
            RandomStr = "12";
        }
        else if (RandomInt == 2)
        {
            RandomStr = "21";
        }
        else if (RandomInt == 3)
        {
            RandomStr = "23";
        }
        else if (RandomInt == 4)
        {
            RandomStr = "42";
        }
        else if (RandomInt == 5)
        {
            RandomStr = "16";
        }
        else if (RandomInt == 6)
        {
            RandomStr = "12";
        }
        else if (RandomInt == 7)
        {
            RandomStr = "25";
        }
        else if (RandomInt == 8)
        {
            RandomStr = "61";
        }
        else if (RandomInt == 9)
        {
            RandomStr = "78";
        }
        else
        {
            RandomStr = "??";
        }

        if (RandomTimer > 0.075f)
        { 
            //time-clock
            if (globalTime > 360)
            {
                ClockUI.text = "6:00";
                Win = true;
                GameWinScreen.SetActive(true);
                GameWinAni.SetBool("Win", true);
                Overseer.GameWinMethod();
                TitleScript.QuickPlayWin = true;
            }
            else if (globalTime > 300)
            {
                ClockUI.text = "5:" + RandomStr;
                Overseer.HoursString = "5";
                globalSeconds = 0;
            }
            else if (globalTime > 240)
            {
                ClockUI.text = "4:" + RandomStr;
                Overseer.HoursString = "4";
                globalSeconds = 0;
            }
            else if (globalTime > 180)
            {
                ClockUI.text = "3:" + RandomStr;
                Overseer.HoursString = "3";
                globalSeconds = 0;
            }
            else if (globalTime > 120)
            {
                ClockUI.text = "2:" + RandomStr;
                Overseer.HoursString = "2";
                globalSeconds = 0;
            }
            else if (globalTime > 60)
            {
                ClockUI.text = "1:" + RandomStr;
                Overseer.HoursString = "1";
                globalSeconds = 0;
            }
            else
            {
                ClockUI.text = "12:" + RandomStr;
                Overseer.HoursString = "12";
            }
            RandomTimer = 0;
        }



        //Smiler Code
        if (FrontDoorsAni.GetBool("isOpen_Obj_1") && CameraController.CameraNum == 4 && SmilerPhase > 0)
        {
            SmilerCounter += Time.deltaTime;
            //Add Heartbeat SFX + Screaming;
            if (Cam4L.isActiveAndEnabled == true)
            {
                BlackoutPannel.SetActive(false);
                Camera4Zoom.SetBool("IsSmilerAnomaly", true);
            }
            if (SmilerCounter > 39.8)
            {
                BlackoutPannel.SetActive(false);
                Camera4Zoom.SetTrigger("IsDead");
                Smiler.clip = SmilerDeathSFX;
                Smiler.Play();
                SmilerStatic.Stop();
            }
            if (SmilerCounter > 30.4 && SmilerPhase == 1)
            {
                BlackoutPannel.SetActive(true);
                SmilerPhase = 2;
                BlackoutPannel.SetActive(true);
                GameOverScreen.SetActive(true);
                GameOverAni.SetTrigger("Death");
                SmilerStatic.Stop();
                Smiler.Stop();
                one.Stop();
                two.Stop();
                three.Stop();
                Overseer.SmilerDeath = true;
                Overseer.GameOverMethod();
            }
        }


        //Game Over
        if (GameOver == true)
        {
            GameOverScreen.SetActive(true);
            GameOverAni.SetTrigger("Game Over");
            Overseer.GameOverMethod();
        }
    }

    IEnumerator AnomalyTimer()
    {
        if (globalTime < 120)
        {
            yield return new WaitForSecondsRealtime(Random.Range(30, 50));
        }
        else if (globalTime < 240)
        {
            yield return new WaitForSecondsRealtime(Random.Range(27.5f, 40));
        }
        else
        {
            yield return new WaitForSecondsRealtime(Random.Range(8, 26));
        }

        SpawnAnomaly();

            StartCoroutine(AnomalyTimer());
    }

                                                                                                            //Need to add anomaly HERE
    public void SpawnAnomaly()
    {
        if (AnomalyCounter < 1000)
        {
            AnomalyCounter++;
            int SpawnNumber = Random.Range(0, 30);
            if (SpawnNumber == 0 && !StoolActive)
            {
                int StoolType = Random.Range(0, 2);
                if (StoolType == 0)
                {
                    StoolDown.SetActive(true);
                    StoolUp.SetActive(false);
                    StoolNormal.SetActive(false);
                    StoolActive = true;
                }
                else if (StoolType == 1)
                {
                    StoolDown.SetActive(false);
                    StoolUp.SetActive(true);
                    StoolNormal.SetActive(false);
                    StoolActive = true;
                }
                else
                {
                    StoolDown.SetActive(false);
                    StoolUp.SetActive(true);
                    StoolNormal.SetActive(false);
                    StoolActive = true;
                }
                AlterAnomalyActiveList.Add("Kitchen Stool");
                AlterAnomalyActiveNum++;
            }
            else if (SpawnNumber == 1 && RackIsNormal)
            {
                int RackType = Random.Range(0, 1);
                if (RackIsNormal == true)
                {
                    if (RackType == 0)
                    {
                        RackNormal.SetActive(false);
                        RackEmpty.SetActive(true);
                        RackIsNormal = false;
                        AlterAnomalyActiveList.Add("Kitchen Rack");
                        AlterAnomalyActiveNum++;
                    }
                }
            }
            else if (SpawnNumber == 2 && HotDogAnomalyBool == false)
            {
                HotdogNormal.SetActive(false);
                HotDogAnomaly.SetActive(true);
                HotDogAnomalyBool = true;
                AlterAnomalyActiveList.Add("Kitchen Hotdog");
                AlterAnomalyActiveNum++;
            }
            else if (SpawnNumber == 3 && !CheeseAnomalyBool)
            {
                CheeseNormal.SetActive(false);
                CheeseAnomaly.SetActive(true);
                CheeseAnomalyBool = true;
                AlterAnomalyActiveList.Add("Kitchen Cheese");
                AlterAnomalyActiveNum++;
            }
            else if (SpawnNumber == 4 && !CookerFireAnomalyBool)
            {
                CookerFire.SetActive(true);
                CookerFireAnomalyBool = true;
                AlterAnomalyActiveList.Add("Kitchen Cooker"); ExtraAnomalyActiveList.Add("Kitchen Cooker");
                AlterAnomalyActiveNum++; ExtraAnomalyActiveNum++;
            }
            else if (SpawnNumber == 5 && !AntiFireAnimator.GetBool("AntiFireAnomalyBool"))
            {
                AntiFireAnimator.SetBool("AntiFireAnomalyBool", true);
                MovementAnomalyActiveList.Add("Kitchen AntiFire");
                MovementAnoamalyActiveNum++;
            }
            else if (SpawnNumber == 6 && PantryDoorsAni.GetBool("PantryDoorDefault"))
            {
                PantryDoorsAni.SetBool("PantryDoorDefault", false);
                MovementAnomalyActiveList.Add("Kitchen Pantry Movement");
                MovementAnoamalyActiveNum++;
                Smiler.Play();
            }
            else if (SpawnNumber == 7 && !FrontDoorsAni.GetBool("isOpen_Obj_1"))
            {
                FrontDoorsAni.SetBool("isOpen_Obj_1", true);

                Sun.intensity = 0; Cam1L.intensity = 0.1f; Cam2L.intensity = 0.1f; Cam3L.intensity = 0.1f; Cam4L.intensity = 0.1f;
                SmilerPhase = 1;
                Smiler.clip = KickSFX;
                Smiler.Play();
                SmilerStatic.Play();
                IntruderAnomalyActiveList.Add("Kitchen Smiler");
                IntruderAnomalyActiveNum++;
            }
            else if (SpawnNumber == 8 && ExtraChairActive == false)
            {
                ExtraChair.SetActive(true);
                ExtraAnomalyActiveList.Add("Kitchen Extra Chair");
                ExtraAnomalyActiveNum++;
                ExtraChairActive = true;

            }
            else if (SpawnNumber == 9 && !CoffeeCupLoungeTrue)
            {
                CoffeeCupLounge.GetComponent<Transform>().position = AnomalyCupPos;
                CoffeeCupLoungeTrue = true;
                MovementAnomalyActiveList.Add("Lounge Coffee Cup");
                MovementAnoamalyActiveNum++;
            }
            else if (SpawnNumber == 10 && !IsNoSignal)
            {
                int TempCamError = Random.Range(1, 4);
                CamNoSignal = TempCamError;
                IsNoSignal = true;
                ElectricalAnomalyActiveNum++;
                ElectricalAnomalyActiveList.Add("Cam No Signal");
                if (CamNoSignal == 1)
                {
                    KitchenElectricalList.Add("Cam No Signal");
                }
                else if (CamNoSignal == 2)
                {
                    LoungeElectricalList.Add("Cam No Signal");
                }
                if (CamNoSignal == 3)
                {
                    BedroomElectricalList.Add("Cam No Signal");
                }
                if (CamNoSignal == 4)
                {
                    BathroomElectricalList.Add("Cam No Signal");
                }
                StartCoroutine(CameraController.StaticPlayer());
            }
            else if (SpawnNumber == 11 && !TVStand.GetBool("isOpen_Obj_1"))
            {
                TVStand.SetBool("isOpen_Obj_1", true);
                MovementAnomalyActiveList.Add("Lounge TV Stand");
                MovementAnoamalyActiveNum++;
            }
            else if (SpawnNumber == 12 && ToiletSpawned == null)
            {
                ToiletSpawned = Instantiate(Toilet, ToiletLoungeSpawnPos.position, ToiletLoungeSpawnPos.rotation);
                ExtraAnomalyActiveList.Add("Lounge Toilet Spawned");
                ExtraAnomalyActiveNum++;
            }
            else if (SpawnNumber == 13 && CouchAnomaly == false)
            {
                CouchAnomaly = true;
                AlterAnomalyActiveList.Add("Lounge Couch");
                AlterAnomalyActiveNum++;
                Destroy(CurrentCouch);
                CurrentCouch = Instantiate(TwoCouch, CouchSpawnPos.position, CouchSpawnPos.rotation);
                StartCoroutine(CameraController.StaticPlayer());
            }
            else if (SpawnNumber == 14 && FireplaceFire == null)
            {
                FireplaceFire = Instantiate(Fire, FireSpawnPos.position, FireSpawnPos.rotation);
                AlterAnomalyActiveNum++; ExtraAnomalyActiveNum++;
                AlterAnomalyActiveList.Add("Fireplace"); ExtraAnomalyActiveList.Add("Fireplace");
                StartCoroutine(CameraController.StaticPlayer());
            }
            else if (SpawnNumber == 15 && PumpkinKingSpawned == null)
            {
                PumpkinKingSpawned = Instantiate(PumpkinKingPrefab, PumpkinKingSpawner);
                IntruderAnomalyActiveList.Add("Pumpkin King");
                IntruderAnomalyActiveNum++;
            }
            else if (SpawnNumber == 16 && !BushmanIsActive)
            {
                BushmanIsActive = true;
                Bushman.SetActive(true);
                StartCoroutine(CameraController.StaticPlayer());
                IntruderAnomalyActiveList.Add("Bushman");
                IntruderAnomalyActiveNum++;
            }
            else if (SpawnNumber == 17 && SlenderInBedSpawned == null)
            {
                SlenderInBedSpawned = Instantiate(SlenderInBed, SlenderInBedSpawnPos.position, SlenderInBedSpawnPos.rotation);
                IntruderAnomalyActiveList.Add("SlenderInBed");
                IntruderAnomalyActiveNum++;
            }
            else if (SpawnNumber == 18 && RCCarAnimator.GetBool("Anomaly") == false)
            {
                RCCarAnimator.SetBool("Anomaly", true);
                MovementAnoamalyActiveNum++;
                MovementAnomalyActiveList.Add("RC Car");
            }
            else if (SpawnNumber == 19 && !DrawersBool)
            {
                Draws.SetActive(true);
                DrawersBool = true;
                ExtraAnomalyActiveList.Add("Drawers");
                ExtraAnomalyActiveNum++;
                StartCoroutine(CameraController.StaticPlayer());
            }
            else if (SpawnNumber == 20 && !RugBool)
            {
                RugRenderer.material = RugAnomaly;
                RugBool = true;
                AlterAnomalyActiveList.Add("Bedroom Rug");
                AlterAnomalyActiveNum++;
            }
            else if (SpawnNumber == 21 && !PillowAnimator.GetBool("Pillow Anomaly"))
            {
                PillowAnimator.SetBool("Pillow Anomaly", true);
                MovementAnomalyActiveList.Contains("Bedroom Pillow");
                MovementAnoamalyActiveNum++;
            }
            else if (SpawnNumber == 22 && !BedPillowBool)
            {
                BedPillow.localScale = new Vector3(1, 1, 2);
                BedPillowBool = true;
                AlterAnomalyActiveList.Add("Bed Pillow");
                AlterAnomalyActiveNum++;
            }
            else if (SpawnNumber == 23 && BedsideLampState == 0)
            {
                BedsideLampState = 1;
                BedsideLamp.SetActive(true);
                ElectricalAnomalyActiveList.Add("Bedside Lamp On");
                ElectricalAnomalyActiveNum++;
            }
            else if (SpawnNumber == 24 && BedsideLampState == 0)
            {
                BedsideLampState = 2;
                ElectricalAnomalyActiveNum++;
                ElectricalAnomalyActiveList.Add("Bedside Lamp Flash");
                StartCoroutine(BedsideLampFlash());
            }
            else if (SpawnNumber == 25 && !ShowerManAnimator.GetBool("Anomaly"))
            {
                ShowerMan.SetActive(true);
                ShowerManAnimator.SetBool("Anomaly", true);
                IntruderAnomalyActiveList.Add("Shower Man");
                IntruderAnomalyActiveNum++;
            }
            else if (SpawnNumber == 26 && LoungePlant.activeSelf)
            {
                LoungePlant.SetActive(false);
                AlterAnomalyActiveList.Add("Lounge Plant");
                AlterAnomalyActiveNum++;
            }
            else if (SpawnNumber == 27 && !BasketAnimator.GetBool("Anomaly"))
            {
                BasketAnimator.SetBool("Anomaly", true);
                MovementAnoamalyActiveNum++;
                MovementAnomalyActiveList.Add("Basket");
            }
            else if (SpawnNumber == 28 && !ToiletSeat.GetBool("Anomaly"))
            {
                ToiletSeat.SetBool("Anomaly", true);
                MovementAnoamalyActiveNum++;
                MovementAnomalyActiveList.Add("Toilet Seat");
            }
            else
            {
                SpawnAnomaly();
            }
        }
        else
        {
            Debug.Log("No More Anomalies");
        }

        if (AnomalyCounter <= 2)
        {
            StabilityUI.text = "Stable";
            if (!one.isPlaying)
            {
                one.Play();
            }
            two.Stop();
            three.Stop();
            Overseer.IsWalkingSFX = false;
        }
        else if (AnomalyCounter <= 6)
        {
            StabilityUI.text = "Unstable";
            if (!two.isPlaying)
            {
                two.Play();
                Overseer.IsWalkingSFX = true;
                Overseer.IsKnocked = false;
            }
            one.Stop();
            three.Stop();
        }
        else if (AnomalyCounter > 6)
        {
            StabilityUI.text = "Critical";
            if (!three.isPlaying)
            {
                three.Play();
            }
            one.Stop();
            two.Stop();
            Overseer.IsWalkingSFX = false;
        }
    }
                                                                                                                    //Need to add anomaly
    public void AnomalyCheck()
    {
        //Find the room toggled
        if (Kitchen.isOn == true)
        {
            RoomType = "Kitchen";
        }
        else if (Lounge.isOn == true)
        {
            RoomType = "Lounge";
        }
        else if (Bedroom.isOn == true)
        {
            RoomType = "Bedroom";
        }
        else if (Bathroom.isOn == true)
        {
            RoomType = "Bathroom";
        }

        //Find the anomaly toggled
        if (MovementR.isOn == true)
        {
            AnomalyType = "Movement";
        }
        else if (ExtraR.isOn == true)
        {
            AnomalyType = "Extra";
        }
        else if (AlterR.isOn == true)
        {
            AnomalyType = "Alter";
        }
        else if (PaintingR.isOn == true)
        {
            AnomalyType = "Painting";
        }
        else if (IntruderR.isOn == true)
        {
            AnomalyType = "Intruder";
        }
        else if (ElectricalR == true)
        {
            AnomalyType = "Electrical";
        }

        //Reporting Anomaly in terms of Report

                                                                                                                  //Need to add anomaly HERE
        if (AnomalyType == "Movement" && MovementAnoamalyActiveNum > 0)
        {
            if (Kitchen.isOn == true)
            {
                if (MovementAnomalyActiveList.Contains("Kitchen AntiFire"))
                {
                    KitchenMovementList.Add("Kitchen AntiFire");
                }
                else
                {
                    KitchenMovementList.Remove("Kitchen AntiFire");
                }

                if (MovementAnomalyActiveList.Contains("Kitchen Pantry Movement"))
                {
                    KitchenMovementList.Add("Kitchen Pantry Movement");
                }
                else
                {
                    KitchenMovementList.Remove("Kitchen Pantry Movement");
                }
            }

            if (Lounge.isOn == true)
            {
                if (MovementAnomalyActiveList.Contains("Lounge TV Stand"))
                {
                    LoungeMovementList.Add("Lounge TV Stand");
                }
                else
                {
                    LoungeMovementList.Remove("Lounge TV Stand"); 
                }

                if (MovementAnomalyActiveList.Contains("Lounge Coffee Cup"))
                {
                    LoungeMovementList.Add("Lounge Coffee Cup");
                }
                else
                {
                    LoungeMovementList.Remove("Lounge Coffee Cup");
                }
            }

            if (RoomType == "Bedroom")
            {
                if (MovementAnomalyActiveList.Contains("RC Car"))
                {
                    BedroomMovementList.Add("RC Car");
                }
                else
                {
                    BedroomMovementList.Remove("RC Car");
                }

                if (MovementAnomalyActiveList.Contains("Bedroom Pillow"))
                {
                    BedroomMovementList.Add("Bedroom Pillow");
                }
                else
                {
                    BedroomMovementList.Remove("Bedroom Pillow");
                }
            }

            if (RoomType == "Bathroom")
            {
                if (MovementAnomalyActiveList.Contains("Basket"))
                {
                    BathroomMovementList.Add("Basket");
                }
                else
                {
                    BathroomMovementList.Remove("Basket");
                }

                if (MovementAnomalyActiveList.Contains("Toilet Seat"))
                {
                    BathroomMovementList.Add("Toilet Seat");
                }
                else
                {
                    BathroomMovementList.Remove("Toilet Seat");
                }
            }
        }
        else if (AnomalyType == "Extra" && ExtraAnomalyActiveNum > 0)
        {
            if (Kitchen.isOn == true)
            {
                if (ExtraAnomalyActiveList.Contains("Kitchen Cooker"))
                {
                    KitchenExtraList.Add("Kitchen Cooker");
                }
                else
                {
                    KitchenExtraList.Remove("Kitchen Cooker");
                }
                if (ExtraAnomalyActiveList.Contains("Kitchen Extra Chair"))
                {
                    KitchenExtraList.Add("Kitchen Extra Chair");
                }
                else
                {
                    KitchenExtraList.Remove("Kitchen Extra Chair");
                }
            }

            if (RoomType == "Lounge")
            {
                if (ExtraAnomalyActiveList.Contains("Lounge Toilet Spawned"))
                {
                    LoungeExtraList.Add("Lounge Toilet");
                }
                else
                {
                    LoungeExtraList.Remove("Lounge Toilet");
                }
            }

            if (RoomType == "Bedroom")
            {
                if (ExtraAnomalyActiveList.Contains("Drawers"))
                {
                    BedroomExtraList.Add("Drawers");
                }
                else
                {
                    BedroomExtraList.Remove("Drawers");
                }
            }
        }
        else if (AnomalyType == "Alter" && AlterAnomalyActiveNum > 0)
        {
            if (RoomType == "Kitchen")
            {
                if (AlterAnomalyActiveList.Contains("Kitchen Cooker"))
                {
                    KitchenAlterList.Add("Kitchen Cooker");
                }
                else
                {
                    KitchenAlterList.Remove("Kitchen Cooker");
                }

                if (AlterAnomalyActiveList.Contains("Kitchen Stool"))
                {
                    KitchenAlterList.Add("Kitchen Stool");
                }
                else
                {
                    KitchenAlterList.Remove("Kitchen Stool");
                }

                if (AlterAnomalyActiveList.Contains("Kitchen Rack"))
                {
                    KitchenAlterList.Add("Kitchen Rack");
                }
                else
                {
                    KitchenAlterList.Remove("Kitchen Rack");
                }

                if (AlterAnomalyActiveList.Contains("Kitchen Hotdog"))
                {
                    KitchenAlterList.Add("Kitchen Hotdog");
                }
                else
                {
                    KitchenAlterList.Remove("Kitchen Hotdog");
                }

                if (AlterAnomalyActiveList.Contains("Kitchen Cheese"))
                {
                    KitchenAlterList.Add("Kitchen Cheese");
                }
                else
                {
                    KitchenAlterList.Remove("Kitchen Cheese");
                }
            }

            if (RoomType == "Lounge")
            {
                if (AlterAnomalyActiveList.Contains("Lounge Couch"))
                {
                    LoungeAlterList.Add("Lounge Couch");
                }
                else
                {
                    LoungeAlterList.Remove("Lounge Couch");
                }

                if (AlterAnomalyActiveList.Contains("Lounge Plant"))
                {
                    LoungeAlterList.Add("Lounge Plant");
                }
                else
                {
                    LoungeAlterList.Remove("Lounge Plant");
                }

            }

            if (RoomType == "Bedroom")
            {
                if (AlterAnomalyActiveList.Contains("Bedroom Rug"))
                {
                    BedroomAlterList.Add("Rug");
                }
                else
                {
                    BedroomAlterList.Remove("Rug");
                }

                if (AlterAnomalyActiveList.Contains("Bed Pillow"))
                {
                    BedroomAlterList.Add("Bed Pillow");
                }
                else
                {
                    BedroomAlterList.Remove("BedPillow");
                }
            }
        }
        else if (AnomalyType == "Painting" && PaintingAnomalyActiveNum > 0)
        {
            if (PaintingAnomalyActiveList.Contains("Error"))
            {
                KitchenPaintingList.Add("Error");
            }
            else
            {
                KitchenPaintingList.Remove("Error");
            }

        }
        else if (AnomalyType == "Intruder" && IntruderAnomalyActiveNum > 0)
        {
            if (RoomType == "Kitchen")
            {
                if (IntruderAnomalyActiveList.Contains("Kitchen Smiler"))
                {
                    KitchenIntruderList.Add("Kitchen Smiler");
                }
                else
                {
                    KitchenIntruderList.Remove("Kitchen Smiler");
                }
            }

            else if (RoomType == "Lounge")
            {
                if (IntruderAnomalyActiveList.Contains("Pumpkin King"))
                {
                    LoungeIntruderList.Add("Pumpkin King");
                }
                else
                {
                    LoungeIntruderList.Remove("Pumpkin King");
                }
                if (IntruderAnomalyActiveList.Contains("Bushman"))
                {
                    LoungeIntruderList.Add("Bushman");
                }
                else
                {
                    LoungeIntruderList.Remove("Bushman");
                }
            }

            else if (RoomType == "Bedroom")
            {
                if (IntruderAnomalyActiveList.Contains("SlenderInBed"))
                {
                    BedroomIntruderList.Add("SlenderInBed");
                }
                else
                {
                    BedroomIntruderList.Remove("SlenderInBed");
                }
            }

            else if (RoomType == "Bathroom")
            {
                if (IntruderAnomalyActiveList.Contains("Shower Man"))
                {
                    BathroomIntruderList.Add("Shower Man");
                }
                else
                {
                    BathroomIntruderList.Remove("Shower Man");
                }
            }

        }
        else if (AnomalyType == "Electrical" && ElectricalAnomalyActiveNum > 0)
        {
            List<string> KitchenElectricalList = new List<string> { };
            if (ElectricalAnomalyActiveList.Contains("Cam No Signal"))
            {
                if (CamNoSignal == 1)
                {
                    KitchenElectricalList.Add("Cam No Signal");
                }
                else if (CamNoSignal == 2)
                {
                    LoungeElectricalList.Add("Cam No Signal");
                }
                else if (CamNoSignal == 3)
                {
                    BedroomElectricalList.Add("Cam No Signal");
                }
                else if (CamNoSignal == 4)
                {
                    BathroomElectricalList.Add("Cam No Signal");
                }
            }
            else
            {
                KitchenElectricalList.Remove("Cam No Signal");
                LoungeElectricalList.Remove("Cam No Signal");
                BedroomElectricalList.Remove("Cam No Signal");
                BathroomElectricalList.Remove("Cam No Signal");
            }

            if (RoomType == "Bedroom")
            {
                if (ElectricalAnomalyActiveList.Contains("Bedside Lamp Flash") || ElectricalAnomalyActiveList.Contains("Bedside Lamp On"))
                {
                    BedroomElectricalList.Add("Bedside Lamp");
                }
                else
                {
                    BedroomElectricalList.Remove("Bedside Lamp");
                }
            }
        }

        if (RoomType == "Lounge")
        {
            if (AnomalyType == "Extra" || AnomalyType == "Alter")
            {
                if (AlterAnomalyActiveList.Contains("Fireplace") || ExtraAnomalyActiveList.Contains("Fireplace"))
                {
                    LoungeAlterList.Add("Lounge Fireplace"); LoungeExtraList.Add("Lounge Fireplace");
                }
                else
                {
                    LoungeAlterList.Remove("Lounge Fireplace"); LoungeExtraList.Remove("Lounge Fireplace");
                }
            }
        }


        //Sets AnomalyFix
        if (RoomType == "Kitchen")
        {
            if (AnomalyType == "Movement")
            {
                if (KitchenMovementList.Count > 0)
                {
                    AnomalyFix = KitchenMovementList[0];
                    KitchenMovementList.Remove(AnomalyFix);
                    MovementAnomalyActiveList.Remove(AnomalyFix);
                }
            }
            else if (AnomalyType == "Extra")
            {
                if (KitchenExtraList.Count > 0)
                {
                    AnomalyFix = KitchenExtraList[0];
                    KitchenExtraList.Remove(AnomalyFix);
                    ExtraAnomalyActiveList.Remove(AnomalyFix);
                }
            }
            else if (AnomalyType == "Alter")
            {
                if (KitchenAlterList.Count > 0)
                {
                    AnomalyFix = KitchenAlterList[0];
                    KitchenAlterList.Remove(AnomalyFix);
                    AlterAnomalyActiveList.Remove(AnomalyFix);
                }
            }
            else if (AnomalyType == "Painting")
            {
                if (KitchenPaintingList.Count > 0)
                {
                    AnomalyFix = KitchenPaintingList[0];
                    KitchenPaintingList.Remove(AnomalyFix);
                    PaintingAnomalyActiveList.Remove(AnomalyFix);
                }
            }
            else if (AnomalyType == "Intruder")
            {
                if (KitchenIntruderList.Count > 0)
                {
                    AnomalyFix = KitchenIntruderList[0];
                    KitchenIntruderList.Remove(AnomalyFix);
                    IntruderAnomalyActiveList.Remove(AnomalyFix);
                }
            }
            else if (AnomalyType == "Electrical")
            {
                if (KitchenElectricalList.Count > 0)
                {
                    AnomalyFix = KitchenElectricalList[0];
                    if (AnomalyFix != "Cam No Signal")
                    {
                       KitchenElectricalList.Remove(AnomalyFix);
                    }
                    ElectricalAnomalyActiveList.Remove(AnomalyFix);
                }
            }
        }
        else if (RoomType == "Lounge")
        {
            if (AnomalyType == "Movement")
            {
                if (LoungeMovementList.Count > 0)
                {
                    AnomalyFix = LoungeMovementList[0];
                    LoungeMovementList.Remove(AnomalyFix);
                    MovementAnomalyActiveList.Remove(AnomalyFix);
                }
            }
            else if (AnomalyType == "Extra")
            {
                if (LoungeExtraList.Count > 0)
                {
                    AnomalyFix = LoungeExtraList[0];
                    LoungeExtraList.Remove(AnomalyFix);
                    ExtraAnomalyActiveList.Remove(AnomalyFix);
                }
            }
            else if (AnomalyType == "Alter")
            {
                if (LoungeAlterList.Count > 0)
                {
                    AnomalyFix = LoungeAlterList[0];
                    LoungeAlterList.Remove(AnomalyFix);
                    AlterAnomalyActiveList.Remove(AnomalyFix);
                }
            }
            else if (AnomalyType == "Painting")
            {
                if (LoungePaintingList.Count > 0)
                {
                    AnomalyFix = LoungePaintingList[0];
                    LoungePaintingList.Remove(AnomalyFix);
                    PaintingAnomalyActiveList.Remove(AnomalyFix);
                }
            }
            else if (AnomalyType == "Intruder")
            {
                if (LoungeIntruderList.Count > 0)
                {
                    AnomalyFix = LoungeIntruderList[0];
                    LoungeIntruderList.Remove(AnomalyFix);
                    IntruderAnomalyActiveList.Remove(AnomalyFix);
                }
            }
            else if (AnomalyType == "Electrical")
            {
                if (LoungeElectricalList.Count > 0)
                {
                    AnomalyFix = LoungeElectricalList[0];
                    if (AnomalyFix != "Cam No Signal")
                    {
                        LoungeElectricalList.Remove(AnomalyFix);
                    }
                    ElectricalAnomalyActiveList.Remove(AnomalyFix);
                }
            }
        }
        else if (RoomType == "Bedroom")
        {
            if (AnomalyType == "Movement")
            {
                if (BedroomMovementList.Count > 0)
                {
                    AnomalyFix = BedroomMovementList[0];
                    BedroomMovementList.Remove(AnomalyFix);
                    MovementAnomalyActiveList.Remove(AnomalyFix);
                }
            }
            else if (AnomalyType == "Extra")
            {
                if (BedroomExtraList.Count > 0)
                {
                    AnomalyFix = BedroomExtraList[0];
                    BedroomExtraList.Remove(AnomalyFix);
                    ExtraAnomalyActiveList.Remove(AnomalyFix);
                }
            }
            else if (AnomalyType == "Alter")
            {
                if (BedroomAlterList.Count > 0)
                {
                    AnomalyFix = BedroomAlterList[0];
                    BedroomAlterList.Remove(AnomalyFix);
                    AlterAnomalyActiveList.Remove(AnomalyFix);
                }
            }
            else if (AnomalyType == "Painting")
            {
                if (BedroomPaintingList.Count > 0)
                {
                    AnomalyFix = BedroomPaintingList[0];
                    BedroomPaintingList.Remove(AnomalyFix);
                    PaintingAnomalyActiveList.Remove(AnomalyFix);
                }
            }
            else if (AnomalyType == "Intruder")
            {
                if (BedroomIntruderList.Count > 0)
                {
                    AnomalyFix = BedroomIntruderList[0];
                    BedroomIntruderList.Remove(AnomalyFix);
                    IntruderAnomalyActiveList.Remove(AnomalyFix);
                }
            }
            else if (AnomalyType == "Electrical")
            {
                if (BedroomElectricalList.Count > 0)
                {
                    AnomalyFix = BedroomElectricalList[0];
                    if (AnomalyFix != "Cam No Signal")
                    {
                        BedroomElectricalList.Remove(AnomalyFix);
                    }
                    ElectricalAnomalyActiveList.Remove(AnomalyFix);
                }
            }
        }
        else if (RoomType == "Bathroom")
        {
            if (AnomalyType == "Movement")
            {
                if (BathroomMovementList.Count > 0)
                {
                    AnomalyFix = BathroomMovementList[0];
                    BathroomMovementList.Remove(AnomalyFix);
                    MovementAnomalyActiveList.Remove(AnomalyFix);
                }
            }
            else if (AnomalyType == "Extra")
            {
                if (BathroomExtraList.Count > 0)
                {
                    AnomalyFix = BathroomExtraList[0];
                    BathroomExtraList.Remove(AnomalyFix);
                    ExtraAnomalyActiveList.Remove(AnomalyFix);
                }
            }
            else if (AnomalyType == "Alter")
            {
                if (BathroomAlterList.Count > 0)
                {
                    AnomalyFix = BathroomAlterList[0];
                    BathroomAlterList.Remove(AnomalyFix);
                    AlterAnomalyActiveList.Remove(AnomalyFix);
                }
            }
            else if (AnomalyType == "Painting")
            {
                if (BathroomPaintingList.Count > 0)
                {
                    AnomalyFix = BathroomPaintingList[0];
                    BathroomPaintingList.Remove(AnomalyFix);
                    PaintingAnomalyActiveList.Remove(AnomalyFix);
                }
            }
            else if (AnomalyType == "Intruder")
            {
                if (BathroomIntruderList.Count > 0)
                {
                    AnomalyFix = BathroomIntruderList[0];
                    BathroomIntruderList.Remove(AnomalyFix);
                    IntruderAnomalyActiveList.Remove(AnomalyFix);
                }
            }
            else if (AnomalyType == "Electrical")
            {
                if (BathroomElectricalList.Count > 0)
                {
                    AnomalyFix = BathroomElectricalList[0];
                    if (AnomalyFix != "Cam No Signal")
                    {
                        BathroomElectricalList.Remove(AnomalyFix);
                    }
                    ElectricalAnomalyActiveList.Remove(AnomalyFix);
                }
            }
        }



        if (AnomalyFix == null)
        {
            SpawnAnomaly();
        }

        //Fixing Anomaly
        StartCoroutine(AnoamlyFixer());
    }

                                                                                                                                                    //Need to add anomaly
    IEnumerator AnoamlyFixer()
    {
        ReportButton.interactable = false;
        UIController.ReportPannelTrue = false;
        UIController.ReportActive = false;
        ReportButton.GetComponentInChildren<TextMeshProUGUI>().text = "Reporting";
        yield return new WaitForSecondsRealtime(InternetSpeed/8);
        ReportButton.GetComponentInChildren<TextMeshProUGUI>().text = "Reporting.";
        yield return new WaitForSecondsRealtime(InternetSpeed/8);
        ReportButton.GetComponentInChildren<TextMeshProUGUI>().text = "Reporting..";
        yield return new WaitForSecondsRealtime(InternetSpeed/8);
        ReportButton.GetComponentInChildren<TextMeshProUGUI>().text = "Reporting...";
        yield return new WaitForSecondsRealtime(InternetSpeed/8);
        ReportButton.GetComponentInChildren<TextMeshProUGUI>().text = "Reporting";
        yield return new WaitForSecondsRealtime(InternetSpeed/8);
        ReportButton.GetComponentInChildren<TextMeshProUGUI>().text = "Reporting.";
        yield return new WaitForSecondsRealtime(InternetSpeed/8);
        ReportButton.GetComponentInChildren<TextMeshProUGUI>().text = "Reporting..";
        yield return new WaitForSecondsRealtime(InternetSpeed/8);
        ReportButton.GetComponentInChildren<TextMeshProUGUI>().text = "Reporting...";
        yield return new WaitForSecondsRealtime(InternetSpeed/8);
        ReportButton.GetComponentInChildren<TextMeshProUGUI>().text = "Report";
        ReportButton.interactable = true;
        UIController.ReportActive = true;

                                                                                                                                    //Need to add anomaly HERE
        //Movement Fixes
        if (AnomalyFix == "Kitchen AntiFire")
        {
            AntiFireAnimator.SetBool("AntiFireAnomalyBool", false);
            Fixed = true;
        }
        else if (AnomalyFix == "Kitchen Pantry Movement")
        {
            PantryDoorsAni.SetBool("PantryDoorDefault", true);
            Fixed = true;
        }
        else if (AnomalyFix == "Lounge Coffee Cup")
        {
            CoffeeCupLounge.GetComponent<Transform>().position = DefaultCupPos;
            CoffeeCupLoungeTrue = false;
            MovementAnoamalyActiveNum++;
            Fixed = true;
        }
        else if (AnomalyFix == "Lounge TV Stand")
        {
            TVStand.SetBool("isOpen_Obj_1", false);
            Fixed = true;
        }
        else if (AnomalyFix == "RC Car")
        {
            RCCarAnimator.SetBool("Anomaly", false);
            MovementAnomalyActiveList.Remove("RC Car");
            Fixed = true;
        }
        else if (AnomalyFix == "Bedroom Pillow")
        {
            MovementAnomalyActiveList.Remove("Bedroom Pillow");
            PillowAnimator.SetBool("Pillow Anomaly", false);
            Fixed = true;
        }
        else if (AnomalyFix == "Toilet Seat")
        {
            Fixed = true;
            ToiletSeat.SetBool("Anomaly", false);
            MovementAnomalyActiveList.Remove("Toilet Seat");
        }

        //Extra Fixes
        else if (AnomalyFix == "Kitchen Extra Chair")
        {
            ExtraChair.SetActive(false);
            ExtraChairActive = false;
            Fixed = true;
        }
        else if (AnomalyFix == "Lounge Toilet")
        {
            Destroy(ToiletSpawned);
            Fixed = true;
            ExtraAnomalyActiveList.Remove("Lounge Toilet Spawned");
        }
        else if (AnomalyFix == "Drawers")
        {
            Draws.SetActive(false);
            DrawersBool = false;
            Fixed = true;
            ExtraAnomalyActiveList.Remove("Drawers");
        }
        else if (AnomalyFix == "Basket")
        {
            BasketAnimator.SetBool("Anomaly", false);
            MovementAnomalyActiveList.Remove("Basket");
            Fixed = true;
        }

        //Alter Fixes
        else if (AnomalyFix == "Kitchen Rack")
        {
            RackIsNormal = true;
            RackNormal.SetActive(true);
            RackEmpty.SetActive(false);
            RackNormal.GetComponent<Transform>().position = RackNormalPos;
            MovementAnomalyActiveList.Remove("Kitchen Rack Movement");
            AlterAnomalyActiveList.Remove("Kitchen Rack Alter");
            Fixed = true;
        }
        else if (AnomalyFix == "Kitchen Stool")
        {
            StoolDown.SetActive(false);
            StoolUp.SetActive(false);
            StoolNormal.SetActive(true);
            StoolActive = false;
            AlterAnomalyActiveList.Remove("Kitchen Stool");
            Fixed = true;
        }
        else if (AnomalyFix == "Kitchen Cooker")
        {
            CookerFire.SetActive(false);
            CookerFireAnomalyBool = false;
            AlterAnomalyActiveList.Remove("Kitchen Cooker");
            Fixed = true;
        }
        else if (AnomalyFix == "Kitchen Hotdog")
        {
            AlterAnomalyActiveList.Remove("Kitchen Hotdog");
            HotdogNormal.SetActive(true);
            HotDogAnomaly.SetActive(false);
            HotDogAnomalyBool = false;
            Fixed = true;
        }
        else if (AnomalyFix == "Kitchen Cheese")
        {
            AlterAnomalyActiveList.Remove("Kitchen Cheese");
            CheeseNormal.SetActive(true);
            CheeseAnomaly.SetActive(false);
            CheeseAnomalyBool = false;
            Fixed = true;
        }
        else if (AnomalyFix == "Lounge Couch")
        {
            Destroy(CurrentCouch);
            CurrentCouch = Instantiate(ThreeCouch, CouchSpawnPos.position, CouchSpawnPos.rotation);
            CouchAnomaly = false;
            Fixed = true;
        }
        else if (AnomalyFix == "Lounge Fireplace")
        {
            Destroy(FireplaceFire);
            FireplaceFire = null;
            Fixed = true;
            ExtraAnomalyActiveList.Remove("Fireplace");
            AlterAnomalyActiveList.Remove("Fireplace");
        }
        else if (AnomalyFix == "Lounge Plant")
        {
            LoungePlant.SetActive(true);
            Fixed = true;
            AlterAnomalyActiveList.Remove("Lounge Plant");
        }
        else if (AnomalyFix == "Rug")
        {
            RugRenderer.material = RugMaterial;
            RugBool = false;
            AlterAnomalyActiveList.Remove("Bedroom Rug");
            Fixed = true;
        }
        else if (AnomalyFix == "Bed Pillow")
        {
            BedPillow.localScale = new Vector3(1, 1, 1);
            BedPillowBool = false;
            AlterAnomalyActiveList.Remove("Bed Pillow");
            Fixed = true;
        }

        //Painting Fixess

        //Intruder Fixes
        else if (AnomalyFix == "Kitchen Smiler")
        {
            FrontDoorsAni.SetBool("isOpen_Obj_1", false);
            Sun.intensity = 0.75f;
            Cam1L.intensity = 0.5f; Cam2L.intensity = 0.5f; Cam3L.intensity = 0.5f; Cam4L.intensity = 0.5f;
            SmilerCounter = 0;
            BlackoutPannel.SetActive(false);
            SmilerPhase = 0;
            Camera4Zoom.SetBool("IsSmilerAnomaly", false);
            Smiler.Play();
            SmilerStatic.Stop();
            Fixed = true;
        }
        else if (AnomalyFix == "Pumpkin King")
        {
            Destroy(PumpkinKingSpawned);
            Fixed = true;
            IntruderAnomalyActiveList.Remove("Pumpkin King");
        }
        else if (AnomalyFix == "Bushman")
        {
            BushmanIsActive = false;
            Bushman.SetActive(false);
            IntruderAnomalyActiveList.Remove("Bushman");
            Fixed = true;
        }
        else if (AnomalyFix == "SlenderInBed")
        {
            Destroy(SlenderInBedSpawned);
            Fixed = true;
            IntruderAnomalyActiveList.Remove("SlenderInBed");
        }
        else if (AnomalyFix == "Shower Man")
        {
            ShowerMan.SetActive(false);
            ShowerManAnimator.SetBool("Anomaly", false);
            IntruderAnomalyActiveList.Remove("Shower Man");
            Fixed = true;
        }

        //Electrical Fixes
        else if (AnomalyFix == "Cam No Signal")
        {
            if (RoomType == "Kitchen")
            {
                if (KitchenElectricalList.Contains(AnomalyFix))
                {
                    CamNoSignal = 0;
                    IsNoSignal = false;
                    StartCoroutine(CameraController.StaticPlayer());
                    ColorNoSignal.SetActive(false);
                    Fixed = true;
                    KitchenElectricalList.Remove(AnomalyFix);
                }
            }
            else if (RoomType == "Lounge")
            {
                if (LoungeElectricalList.Contains(AnomalyFix))
                {
                    CamNoSignal = 0;
                    IsNoSignal = false;
                    StartCoroutine(CameraController.StaticPlayer());
                    ColorNoSignal.SetActive(false);
                    Fixed = true;
                    LoungeElectricalList.Remove(AnomalyFix);
                }
            }
            else if (RoomType == "Bedroom")
            {
                if (BedroomElectricalList.Contains(AnomalyFix))
                {
                    CamNoSignal = 0;
                    IsNoSignal = false;
                    StartCoroutine(CameraController.StaticPlayer());
                    ColorNoSignal.SetActive(false);
                    Fixed = true;
                    BedroomElectricalList.Remove(AnomalyFix);
                }
            }
            else if (RoomType == "Bathroom")
            {
                if (BathroomElectricalList.Contains(AnomalyFix))
                {
                    CamNoSignal = 0;
                    IsNoSignal = false;
                    StartCoroutine(CameraController.StaticPlayer());
                    ColorNoSignal.SetActive(false);
                    Fixed = true;
                    BathroomElectricalList.Remove(AnomalyFix);
                }
            }
        }
        else if (AnomalyFix == "Bedside Lamp")
        {
            BedsideLampState = 0;
            BedsideLamp.SetActive(false);
            ElectricalAnomalyActiveList.Remove("Bedside Lamp On");
            ElectricalAnomalyActiveList.Remove("Bedside Lamp Flash");
            Fixed = true;
        }

        //End of report fixing
        if (Fixed == true)
        {
            AnomaliesFixed++;
            AnomalyCounter--;
            AnomalyFix = null;
            Fixed = false;

            FixingAnomaly.SetActive(true);
            yield return new WaitForSecondsRealtime(2f);
            FixingAnomaly.SetActive(false);
            if (AnomalyCounter < 3)
            {
                StabilityUI.text = "Stable";
                if (!one.isPlaying)
                {
                    one.Play();
                }
                two.Stop();
                three.Stop();
            }
            else if (AnomalyCounter == 4 || AnomalyCounter == 5)
            {
                StabilityUI.text = "Unstable";
                if (!two.isPlaying)
                {
                    two.Play();
                }
                one.Stop();
                three.Stop();
            }
            else if (AnomalyCounter > 5)
            {
                StabilityUI.text = "Critical";
                if (!three.isPlaying)
                {
                    three.Play();
                }
                one.Stop();
                two.Stop();
            }
        }
        else
        {
            NoAnomaliesFound.SetActive(true);
            yield return new WaitForSecondsRealtime(5f);
            NoAnomaliesFound.SetActive(false);
        }
    }

    private IEnumerator BedsideLampFlash()
    {
        while (BedsideLampState == 2)
        {
            BedsideLamp.SetActive(false);
            yield return new WaitForSecondsRealtime(1);
            BedsideLamp.SetActive(true);
            yield return new WaitForSecondsRealtime(1);
        }
    }

    public void SaveData(ref GameData data)
    {
        data.Shifts++;
        data.AnomaliesFixed = AnomaliesFixed;
        if (!Win)
        {
            data.ShiftsFailed++;
        }
        else
        {
            data.ShiftsCompleted++;
        }

        Debug.Log(data.Shifts);
        Debug.Log(data.AnomaliesFixed);
        Debug.Log(data.ShiftsCompleted);

        Debug.Log("Saved gameData");
    }
    public void LoadData(GameData data)
    {
        Debug.LogError("Can not load data when in quickplay gamemode");
    }
}
