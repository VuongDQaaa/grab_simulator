using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GamePlayUI : MonoBehaviour
{
    public static GamePlayUI instance;
    [SerializeField] private Button _pauseButton, _resumeButton, _backButton, _getRewardButton, _restartButton, _quitButton, _acceptButton, _cancelButton, _missionsButton;
    [SerializeField] private GameObject _pauseMenu, _winMenu, _loseMenu, _missionInfor, _missionsMenu;
    [SerializeField] private TextMeshProUGUI _goldText, _missionName, _gold, _missionTime;
    [SerializeField] private string _fileName;
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
        _missionName.text = "Mission name: " + MissionManager.instance.missionName;
        _gold.text = "Gold: " + MissionManager.instance.gold;
        _missionTime.text = "Time: " + MissionManager.instance.missionTime + " Seconds";
    }

    private void ShowMission()
    {
        if(newMission == true)
        {
            _missionInfor.SetActive(true);
        }
        else
        {
            _missionInfor.SetActive(false);
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
        CurrentMission.instance.spawnLocation = true;
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
        //Save history
        HistoryManager.instance.missionName = CurrentMission.instance.missionName;
        HistoryManager.instance.completeDate = DateTime.Now;
        HistoryManager.instance.AddHistoryToList();

        //Remove Doing mission
        List<MissionElement> missions = FileHandler.ReadListFromJson<MissionElement>(_fileName);
        MissionElement el = missions.FirstOrDefault(x => x.missionId == CurrentMission.instance.missionId);
        if(el != null)
        {
            missions.Remove(el);
            FileHandler.SaveToJSON(missions, _fileName);
        }

        //Spawn new mission provider
        MissionSpawner.instance.SpawnNew = true;
        //Reset current mission
        CurrentMission.instance.ResetCurrentMission();
        //Close win menu
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

    public void MissionsButton()
    {
        _missionsMenu.SetActive(true);
    }
}
