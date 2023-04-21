using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionController : MonoBehaviour
{
    public Mission[] missions;
    [System.Obsolete]
    private void Start()
    {
        SelectMission();
    }

    [System.Obsolete]
    private void SelectMission()
    {
        int random = Random.RandomRange(0, missions.Length);
        CurrentMission.instance.missionName = missions[random].missionName;
        CurrentMission.instance.missionTime = missions[random].missionTime;
        CurrentMission.instance.CurrentMissionTime = missions[random].missionTime;
        CurrentMission.instance.missonGold = missions[random].gold;
        CurrentMission.instance.pickUpLocation = missions[random].pickUpLocation;
        CurrentMission.instance.finishLocation = missions[random].recieveLocation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GamePlayUI.instance.newMission = true;
        }
    }
}
