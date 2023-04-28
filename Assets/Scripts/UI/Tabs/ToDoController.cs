using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDoController : MonoBehaviour
{
    public static ToDoController instance;
    private List<MissionData> missions = new List<MissionData>();
    public void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Awake()
    {
        MakeInstance();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
