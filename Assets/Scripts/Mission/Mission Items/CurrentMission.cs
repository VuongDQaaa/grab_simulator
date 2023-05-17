using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class CurrentMission : MonoBehaviour
{
    public static CurrentMission instance;
    [SerializeField] private GameObject _pickUp, _finish, _timer;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private string _fileName;
    public bool isStarted, isCompleted, spawnLocation, failed;
    public string missionName;
    public int missionId, missonGold;
    public float missionTime, CurrentMissionTime;
    public Vector3 pickUpLocation, finishLocation;

    private void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Awake()
    {
        MakeInstance();
        isStarted = false;
        isCompleted = false;
        spawnLocation = false;
        failed = false;
        CheckDoingMission();
    }

    void Update()
    {
        SpawnLocation();
        if (isStarted == true)
        {
            CountDown();
        }
        SpawnLocation();
    }

    private void SpawnLocation()
    {
        if (spawnLocation == true)
        {
            Instantiate(_pickUp, pickUpLocation, Quaternion.identity);
            Instantiate(_finish, finishLocation, Quaternion.identity);
        }
        spawnLocation = false;
    }

    private void CountDown()
    {
        _timer.SetActive(true);
        DisplayTime(missionTime);
        if (missionTime > 0)
        {
            missionTime -= Time.deltaTime;
        }
        else if (missionTime <= 0 & isCompleted == false)
        {
            missionTime = 0;
            failed = true;
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        _timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ResetCurrentMission()
    {
        _timer.SetActive(false);
        PlayerHealth.instance.health = 100;
        isStarted = false;
        isCompleted = false;
        spawnLocation = false;
        failed = false;
    }

    public void RestartMission()
    {
        _timer.SetActive(false);
        PlayerHealth.instance.health = 100;
        isStarted = false;
        isCompleted = false;
        failed = false;
        missionTime = CurrentMissionTime;
        Instantiate(_pickUp, pickUpLocation, Quaternion.identity);
        Instantiate(_finish, finishLocation, Quaternion.identity);
    }

    private void CheckDoingMission()
    {
        List<MissionElement> itemList = FileHandler.ReadListFromJson<MissionElement>(_fileName);
        if (itemList.Count != 0)
        {
            MissionElement currentMission = itemList.FirstOrDefault(x => x.isDoing == true);
            if (currentMission != null)
            {
                missionName = currentMission.missionName;
                missonGold = currentMission.gold;
                missionTime = currentMission.missionTime;
                CurrentMissionTime = currentMission.missionTime;
                pickUpLocation = currentMission.pickUpLocation;
                finishLocation = currentMission.recieveLocation;
                spawnLocation = true;
            }
        }
    }


}
