using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CanvasManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button startButton;
    public Button quitButton;
    public Button settingsButton;
    public Button backButton;
    public Button returnToMenuButton;
    public Button returnToGameButton;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject settingsMenu;

    [Header("Text")]
    public Text livesText;
    public Text scoreText;
    public Text volSliderText;

    [Header("Slider")]
    public Slider volSlider;

    public void ShowMainMenu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ShowSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }


    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (startButton)
            startButton.onClick.AddListener(() => StartGame());

        if (settingsButton)
            settingsButton.onClick.AddListener(() => ShowSettingsMenu());

        if (backButton)
            backButton.onClick.AddListener(() => ShowMainMenu());

        if (returnToMenuButton)
            returnToMenuButton.onClick.AddListener(() => ReturnToMain());

        if (returnToGameButton)
            returnToGameButton.onClick.AddListener(() => pauseMenu.SetActive(!pauseMenu.activeSelf));

        if (quitButton)
            quitButton.onClick.AddListener(() => QuitGame());

        if (volSlider && volSliderText)
        {
            volSlider.onValueChanged.AddListener((value) => OnSliderValueChange(value));
            volSliderText.text = volSlider.value.ToString();
        }

        if (livesText)
            GameManager.instance.onLifeValueChange.AddListener((value) => OnLifeValueChange(value));

        if (scoreText)
            GameManager.instance.onScoreValueChange.AddListener((value) => OnScoreValueChange(value));
    }

    void OnSliderValueChange(float value)
    {
        volSliderText.text = value.ToString();
    }

    void OnLifeValueChange(int value)
    {
        livesText.text = value.ToString();
    }

    void OnScoreValueChange(int value)
    {
        scoreText.text = value.ToString();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseMenu)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                pauseMenu.SetActive(!pauseMenu.activeSelf);
            }
                
            //HUGE HINT FOR THE LAB
            if (pauseMenu.activeSelf)
            {
                PauseGame();
            }
            
            else
            {
                ResumeGame();
            }
                   
        }
            
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
