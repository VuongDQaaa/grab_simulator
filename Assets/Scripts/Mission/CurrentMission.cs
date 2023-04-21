using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentMission : MonoBehaviour
{
    [SerializeField] private GameObject _pickUp, _finish, _timer;
    [SerializeField] private TextMeshProUGUI _timeText;
    public static CurrentMission instance;
    public bool isStarted, isCompleted, spawnLocation, failed;
    public string missionName;
    public int missonGold;
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
    }

    void Update()
    {
        SpawnLocation();
        if(isStarted == true)
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
        isStarted = false;
        isCompleted = false;
        spawnLocation = false;
        failed = false;
    }

    public void RestartMission()
    {
        _timer.SetActive(false);
        isStarted = false;
        isCompleted = false;
        failed = false;
        missionTime = CurrentMissionTime;
        Instantiate(_pickUp, pickUpLocation, Quaternion.identity);
        Instantiate(_finish, finishLocation, Quaternion.identity);
    }


}
