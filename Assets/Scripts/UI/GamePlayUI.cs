using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GamePlayUI : MonoBehaviour
{
    public static GamePlayUI instance;
    [SerializeField] private Button _pauseButton, _resumeButton, _backButton, _getRewardButton, _restartButton, _quitButton, _acceptButton, _cancelButton;
    [SerializeField] private GameObject _pauseMenu, _winMenu, _loseMenu, _missionMenu;
    [SerializeField] private TextMeshProUGUI _goldText, _missionName, _gold, _missionTime;
    public GameObject missionProvider;
    public bool newMission;
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Awake()
    {
        newMission = false;
        MakeInstance();
    }

    private void Update()
    {
        ShowLoseMenu();
        ShowWinMenu();
        DisplayGold();
        ShowMission();
        DisplayMissionInfor();
    }

    private void DisplayGold()
    {
        _goldText.text = GameManager.instance.GetGold().ToString();
    }

    private void DisplayMissionInfor()
    {
        _missionName.text = "Mission name: " + CurrentMission.instance.missionName;
        _gold.text = "Gold: " + CurrentMission.instance.missonGold;
        _missionTime.text = "Time: " + CurrentMission.instance.CurrentMissionTime + " Seconds";
    }

    private void ShowMission()
    {
        if(newMission == true)
        {
            _missionMenu.SetActive(true);
        }
        else
        {
            _missionMenu.SetActive(false);
        }
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

    public void AcceptButton()
    {
        newMission = false;
        CurrentMission.instance.spawnLocation = true;
        GameObject _missionProvider = GameObject.FindGameObjectWithTag("Mission");
        if (_missionProvider != null)
        {
            Destroy(_missionProvider);
        }
    }

    public void CancelButton()
    {
        newMission = false;
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
        if (GameManager.instance != null)
        {
            int newGold = GameManager.instance.GetGold() + CurrentMission.instance.missonGold;
            GameManager.instance.SetGold(newGold);
        }
        MissionSpawner.instance.SpawnNew = true;
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
