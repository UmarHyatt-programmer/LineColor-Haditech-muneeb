using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{
    public static UiManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameObject("UiManager").AddComponent<UiManager>(); //create game manager object if required
            return instance;
        }
    }

    private static UiManager instance = null;

    Player playerScript;
    public GameObject playerRef;
    public GameObject[] levels;
    [Header("Panels")]
    public GameObject levelCompletePanel;
    public GameObject levelFailedPanel;
    public GameObject continuePlaying;
    public GameObject pausePanel;
    public GameObject mainMenu;
    public GameObject gamePlay;

    public Text levelText;

    [Header("Rate US Links")]
    public string RateUsAndriod = "";
    public string RateUsiOS = "";

    [Header("MoreApp Links")]
    public string MoreAppAndriod = "";
    public string MoreAppiOS = "";

    [Header("Audio Source")]
    public AudioSource bgMusic, buttonClick;

    public Button music;
    public Sprite on,off;
    bool isSound = false;
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        int i = PlayerPrefs.GetInt("LevelNo");
        levels[i - 1].SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        playerScript = playerRef.GetComponent<Player>();
        levelText.text = "Level " + (PlayerPrefs.GetInt("GameComplete")*40) + PlayerPrefs.GetInt("LevelNo").ToString();
        
    }

    #region Panels
    public void TapPlay()
    {
        buttonClick.Play();
        mainMenu.SetActive(false);
        gamePlay.SetActive(true);
        playerScript.ActivatePlayerMovement();
       // AdsManager.instance.Show_Banner();
    }

    public void OnLevelComplete()
    {
        Invoke("CompleteLevel", 2f);
        //AdsManager.instance.Show_AdmobInterstitial();
    }

    void CompleteLevel()
    { 
        levelCompletePanel.SetActive(true);
        PlayerPrefs.SetInt("ColorAssign", PlayerPrefs.GetInt("ColorAssign") + 1);
        PlayerPrefs.SetInt("LevelNo", PlayerPrefs.GetInt("LevelNo") + 1);
        if (PlayerPrefs.GetInt("levelNo") > levels.Length)
        {
            PlayerPrefs.SetInt("LevelNo", 1);
            PlayerPrefs.SetInt("ColorAssign", 1);
            PlayerPrefs.SetInt("GameComplete", PlayerPrefs.GetInt("GameComplete") + 1);
        }
    }

    public void OnLevelFailed()
    {
        levelFailedPanel.SetActive(true);
        //AdsManager.instance.Show_AdmobInterstitial();       
    }
    public void OnContinuePlaying()
    {
        Time.timeScale = 0;
        gamePlay.SetActive(false);
        continuePlaying.SetActive(true);
    }
    #endregion


    #region Button CLicks
    public void OnPauseClick()
    { 
        buttonClick.Play();
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        //AdsManager.instance.Show_AdmobInterstitial();
    }

    public void OnResumeClick()
    {
        buttonClick.Play();
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void PlayAgain()
    {
        buttonClick.Play();
        Time.timeScale = 1;
       
        //SceneManager.LoadScene(SceneManager.GetActiveScene());
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
        //AdsManager.instance.Destroy_Banner();
    }
    public void OnNextClick()
    {
        buttonClick.Play();
        SceneManager.LoadScene("Loading");
        //AdsManager.instance.Destroy_Banner();
    }

    public void OnContinueClick()
    {
        buttonClick.Play();
        ////AdsManager.instance.Show_ResumeVideo();
    }

    public void ResumeGameAfterVideoWatched()
    {
        if (GameManager.resume == true)
        {
            Time.timeScale = 1;
            continuePlaying.SetActive(false);
            playerRef.SetActive(true);
            playerScript.ContinuePlay();
            GameManager.resume = false;
        }
        else
        {
            OnLevelFailed();
        }
        
    }

    public void NoThanks()
    {
        buttonClick.Play();
        continuePlaying.SetActive(false);
        gamePlay.SetActive(false);
        levelFailedPanel.SetActive(true);
    }

    public void Skiptrack()
    {
        buttonClick.Play();
        //AdsManager.instance.Show_SkipTrackVideo();
        //AdsManager.instance.Destroy_Banner();
    }

    public void TrackSkippedComplete()
    {
        if (GameManager.skipable == true)
        {
            PlayerPrefs.SetInt("ColorAssign", PlayerPrefs.GetInt("ColorAssign") + 1);
            PlayerPrefs.SetInt("LevelNo", PlayerPrefs.GetInt("LevelNo") + 1);
            SceneManager.LoadScene("Loading");
        } 
    }

    #endregion


    #region Buttons
    public void MoreGamesButtonClick()
    {

    #if UNITY_ANDROID
        buttonClick.Play();
        Application.OpenURL(MoreAppAndriod);

    #elif UNITY_IOS
          Application.OpenURL(MoreAppiOS); 
#endif
    }

    public void RateUsButtonClick()
    {
    #if UNITY_ANDROID
        buttonClick.Play();
        Application.OpenURL(RateUsAndriod);
    #elif UNITY_IOS
          Application.OpenURL(RateUsiOS); 
    #endif
    }

    public void Privacy()
    {
        buttonClick.Play();
        Application.OpenURL("https://sites.google.com/view/hadi-technologies/home");
    }
    #endregion

    #region Sound
    public void MusicOnOff()
    {
        buttonClick.Play();
        if (!isSound)
        {
            isSound = true;
            bgMusic.GetComponent<AudioSource>().mute = true;
            music.GetComponent<Image>().sprite = off;
        }
        else
        {
            isSound = false;
            bgMusic.GetComponent<AudioSource>().mute = false;
            music.GetComponent<Image>().sprite = on;

        }
    }
    #endregion

   
}
