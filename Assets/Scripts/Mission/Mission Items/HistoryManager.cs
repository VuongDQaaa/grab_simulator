using System;
using System.Collections.Generic;
using UnityEngine;

public class HistoryManager : MonoBehaviour
{
    public static HistoryManager instance;
    [SerializeField] private string _fileName;
    [SerializeField] private int MaxCount = 4;
    List<HistoryElement> entries = new List<HistoryElement>();

    [Header("history infor")]
    public string missionName;
    public DateTime completeDate;
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

    private void Start()
    {
        LoadHistory();
    }

    public void AddHistoryToList()
    {
        LoadHistory();
        entries.Add(new HistoryElement(missionName, completeDate.ToString("dddd, MMMM dd, yyyy h:mm:ss tt")));
        FileHandler.SaveToJSON<HistoryElement>(entries, _fileName);
    }

    private void LoadHistory()
    {
        entries = FileHandler.ReadListFromJson<HistoryElement>(_fileName);
        while (entries.Count == MaxCount)
        {
            entries.RemoveAt(0);
        }
    }
}
