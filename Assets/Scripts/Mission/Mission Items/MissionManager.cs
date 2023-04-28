using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance;
    [SerializeField] private Button acceptButton;
    [SerializeField] string fileName;
    List<MissionElement> entries = new List<MissionElement>();
    [Header("Mission infor")]
    public int missionId;
    public string missionName;
    public Vector3 pickUpLocation, recieveLocation;
    public int gold;
    public float missionTime;
    public bool isDoing, isCompleted;
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
    }

    private void AddMissionToList()
    {
        entries = FileHandler.ReadListFromJson<MissionElement>(fileName);
        entries.Add(new MissionElement(missionId, missionName, pickUpLocation, recieveLocation, gold, missionTime, isDoing, isCompleted));
        FileHandler.SaveToJSON<MissionElement>(entries, fileName);
    }

    //Add the mission into Json file when the missions number is below 4
    public void AcceptButton()
    {
        if (entries.Count <= 3)
        {
            GamePlayUI.instance.newMission = false;
            GameObject _missionProvider = GameObject.FindGameObjectWithTag("Mission");
            if (_missionProvider != null)
            {
                Destroy(_missionProvider);
            }
            //Add mission to list
            AddMissionToList();
        }
        else
        {
            GamePlayUI.instance.newMission = false;
        }
    }
}
