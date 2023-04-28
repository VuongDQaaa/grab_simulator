using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MissionProvider : MonoBehaviour
{
    private List<MissionElement> _elements = new List<MissionElement>();
    [SerializeField] private string _fileName;
    public MissionData[] missions;
    [System.Obsolete]
    private void Start()
    {
        SelectMission();
    }

    [System.Obsolete]
    private void SelectMission()
    {
        int random = Random.RandomRange(0, missions.Length);
        MissionManager.instance.missionId = GenerateID(_elements);
        MissionManager.instance.missionName = missions[random].missionName;
        MissionManager.instance.missionTime = missions[random].missionTime;
        MissionManager.instance.gold = missions[random].gold;
        MissionManager.instance.pickUpLocation = missions[random].pickUpLocation;
        MissionManager.instance.recieveLocation = missions[random].recieveLocation;
        MissionManager.instance.isDoing = false;
        MissionManager.instance.isCompleted = false;
    }

    private int GenerateID(List<MissionElement> list)
    {
        int missionId = 0;
        _elements = FileHandler.ReadListFromJson<MissionElement>(_fileName);
        for (int i = 0; i <= _elements.Count; i++)
        {
            MissionElement element = _elements.FirstOrDefault(item => item.missionId == i);
            if(element == null)
            {
                missionId = i;
            }
        }
        return missionId;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GamePlayUI.instance.newMission = true;
        }
    }
}
