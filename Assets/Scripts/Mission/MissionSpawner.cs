using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSpawner : MonoBehaviour
{
    public static MissionSpawner instance;
    [SerializeField] private GameObject _missionProvider;
    public Vector3[] missionLocation;
    public List<GameObject> mission = new List<GameObject>();
    public bool SpawnNew;
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    private void Awake()
    {
        SpawnNew = false;
        MakeInstance();
    }

    private void Update()
    {
        SpawnMissionProvider();
    }

    private void SpawnMissionProvider()
    {
        if (SpawnNew == true)
        {
            int randomNum = Random.Range(0, 10);
            Instantiate(_missionProvider, missionLocation[randomNum], Quaternion.identity);
            SpawnNew = false;
            mission.Add(_missionProvider);
        }
    }
}
