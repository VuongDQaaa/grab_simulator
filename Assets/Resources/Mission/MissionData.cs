using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mission", menuName = "ScriptableObject/Mission")]
public class MissionData : ScriptableObject
{
    public string missionName;
    public Vector3 pickUpLocation;
    public Vector3 recieveLocation;
    public int gold;
    public float missionTime;
}
