using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GamePlayUI : MonoBehaviour
{
    public static GamePlayUI instance;
    [SerializeField] private Button _pauseButton, _resumeButton, _backButton, _getRewardButton, _restartButton, _quitButton;
    [SerializeField] private GameObject _pauseMenu, _winMenu, _loseMenu;
    [SerializeField] private TextMeshProUGUI goldText;
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Awake()
    {
        MakeInstance();
    }

    private void Update()
    {
        ShowLoseMenu();
        ShowWinMenu();
        DisplayGold();
    }

    private void DisplayGold()
    {
        goldText.text = GameManager.instance.GetGold().ToString();
    }

    private void ShowLoseMenu()
    {
        if (CurrentMission.instance.failed == true)
        {
            _loseMenu.SetActive(true);
        }
    }

    private void ShowWinMenu()
    {
        if (CurrentMission.instance.isCompleted == true)
        {
            _winMenu.SetActive(true);
        }
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        _pauseMenu.SetActive(true);
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
    }

    public void BackButton()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        SceneManager.LoadScene("Main Menu");
    }

    public void GetReward()
    {
        if(GameManager.instance != null)
        {
            int newGold = GameManager.instance.GetGold() + CurrentMission.instance.missonGold;
            GameManager.instance.SetGold(newGold);
        }
        CurrentMission.instance.ResetCurrentMission();
        _winMenu.SetActive(false);

    }

    public void RestartButton()
    {
        CurrentMission.instance.RestartMission();
        _loseMenu.SetActive(false);
    }

    public void QuitButton()
    {
        CurrentMission.instance.isCompleted = true;
        MissionSpawner.instance.SpawnNew = true;
        _loseMenu.SetActive(false);
    }
}
