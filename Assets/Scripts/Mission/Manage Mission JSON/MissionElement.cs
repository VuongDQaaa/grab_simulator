using UnityEngine;
using System;

[Serializable]
public class MissionElement
{
    public int missionId;
    public string missionName;
    public Vector3 pickUpLocation;
    public Vector3 recieveLocation;
    public int gold;
    public float missionTime;
    public bool isDoing;
    public bool isCompleted;
    public MissionElement(int missionId, string missionName, Vector3 pickUpLocation, Vector3 recieveLocation, int gold, float missionTime, bool isDoing, bool isCompleted)
    {
        this.missionId = missionId;
        this.missionName = missionName;
        this.pickUpLocation = pickUpLocation;
        this.recieveLocation = recieveLocation;
        this.gold = gold;
        this.missionTime = missionTime;
        this.isDoing = isDoing;
        this.isCompleted = isCompleted;
    }
}
