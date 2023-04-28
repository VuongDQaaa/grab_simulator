using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionUI : MonoBehaviour
{
    public static MissionUI instance;
    [SerializeField] private string _fileName;
    [SerializeField] private string _historyFileName;
    private List<HistoryElement> _historyElements = new List<HistoryElement>();
    private List<MissionElement> _missionElements = new List<MissionElement>();
    MissionElement doingMission;
    [SerializeField] private GameObject _missionElementFrefab;
    [SerializeField] private GameObject _doingMissionElement;
    [SerializeField] private GameObject _historyElementPrefab;
    [SerializeField] private GameObject _historyWrapper;
    [SerializeField] private GameObject _elementWrapper;
    [SerializeField] private GameObject _doingElementWrapper;
    List<GameObject> uiElement = new List<GameObject>();
    List<GameObject> historyCard = new List<GameObject>();

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

    private void OnEnable()
    {
        if (uiElement.Count == 0 || doingMission == null || historyCard.Count == 0)
        {
            LoadMission();
            UpdateDoingMission(_missionElements);
            UpdateToDoMission(_missionElements);
            UpdateHistory(_historyElements);
        }
    }

    private void OnDisable()
    {
        if (uiElement.Count != 0)
        {
            ResetInfor();
        }
    }

    private void UpdateToDoMission(List<MissionElement> list)
    {
        List<MissionElement> toDoList = new List<MissionElement>();
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].isDoing == false)
            {
                toDoList.Add(list[i]);
            }
        }
        // Create mission list on UI 
        for (int i = 0; i < toDoList.Count; i++)
        {
            MissionElement el = toDoList[i];
            if (i >= uiElement.Count)
            {
                var inst = Instantiate(_missionElementFrefab, Vector3.zero, Quaternion.identity);
                inst.transform.SetParent(_elementWrapper.transform);
                uiElement.Add(inst);
            }
            //set text in the element
            var texts = uiElement[i].GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = "Mission Name: " + el.missionName;
            texts[1].text = "Time: " + el.missionTime.ToString();
            texts[2].text = "Gold: " + el.gold.ToString();

            //set onClick for the element button
            Button beginButton = uiElement[i].GetComponentInChildren<Button>();
            beginButton.onClick.AddListener(delegate { BeginButton(el.missionId, toDoList); });
        }
    }

    private void UpdateDoingMission(List<MissionElement> list)
    {
        doingMission = list.FirstOrDefault(x => x.isDoing);
        if (doingMission != null)
        {
            var inst = Instantiate(_doingMissionElement, Vector3.zero, Quaternion.identity);
            inst.transform.SetParent(_doingElementWrapper.transform);

            //Set text
            var texts = inst.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = "Mission Name: " + doingMission.missionName;
            texts[1].text = "Time: " + doingMission.missionTime.ToString();
            texts[2].text = "Gold: " + doingMission.gold.ToString();

            //set Onclick for the element button
            Button cancelButton = inst.GetComponentInChildren<Button>();
            cancelButton.onClick.AddListener(delegate { CancelButton(doingMission.missionId, list); });
        }
    }

    private void UpdateHistory(List<HistoryElement> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            HistoryElement el = list[i];
            if (i >= historyCard.Count)
            {
                var inst = Instantiate(_historyElementPrefab, Vector3.zero, Quaternion.identity);
                inst.transform.SetParent(_historyWrapper.transform);
                historyCard.Add(inst);
            }
            //set text in the element
            var texts = historyCard[i].GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = "Mission Name: " + el.missionName;
            texts[1].text = "Completed Time: " + el.completedDate;
        }
    }

    public void LoadMission()
    {
        _missionElements = FileHandler.ReadListFromJson<MissionElement>(_fileName);
        _historyElements = FileHandler.ReadListFromJson<HistoryElement>(_historyFileName);
        _historyElements.Reverse();
    }

    private void BeginButton(int missionId, List<MissionElement> list)
    {
        if (doingMission == null)
        {
            MissionElement el = list.FirstOrDefault(x => x.missionId == missionId);
            if (el != null)
            {
                el.isDoing = true;
            }
            FileHandler.SaveToJSON<MissionElement>(list, _fileName);
            //reset data
            ResetInfor();
            LoadMission();
            UpdateDoingMission(_missionElements);
            UpdateToDoMission(_missionElements);

            //set current mission
            CurrentMission.instance.missionId = el.missionId;
            CurrentMission.instance.missionName = el.missionName;
            CurrentMission.instance.missonGold = el.gold;
            CurrentMission.instance.missionTime = el.missionTime;
            CurrentMission.instance.CurrentMissionTime = el.missionTime;
            CurrentMission.instance.pickUpLocation = el.pickUpLocation;
            CurrentMission.instance.finishLocation = el.recieveLocation;
            CurrentMission.instance.spawnLocation = true;
        }
    }

    private void CancelButton(int missionId, List<MissionElement> list)
    {
        MissionElement el = list.FirstOrDefault(x => x.missionId == missionId);
        if (el != null)
        {
            el.isDoing = false;
        }
        FileHandler.SaveToJSON<MissionElement>(list, _fileName);
        //reset data
        ResetInfor();
        LoadMission();
        UpdateDoingMission(_missionElements);
        UpdateToDoMission(_missionElements);

        //set current mission
        CurrentMission.instance.missionName = null;
        CurrentMission.instance.missonGold = 0;
        CurrentMission.instance.missionTime = 0f;
        CurrentMission.instance.CurrentMissionTime = 0f;
        CurrentMission.instance.pickUpLocation = Vector3.zero;
        CurrentMission.instance.finishLocation = Vector3.zero;
        GameObject pickUp = GameObject.FindGameObjectWithTag("Pick Up");
        GameObject finish = GameObject.FindGameObjectWithTag("Finish");
        if (pickUp != null && finish != null)
        {
            Destroy(pickUp);
            Destroy(finish);
        }
        else
        {
            Debug.LogError("Not find game object");
        }
    }

    private void ResetInfor()
    {
        uiElement.Clear();
        historyCard.Clear();
        doingMission = null;
        foreach (Transform child in _elementWrapper.transform)
        {
            GameObject card = child.gameObject;
            Destroy(card);
        }

        foreach (Transform child in _doingElementWrapper.transform)
        {
            GameObject card = child.gameObject;
            Destroy(card);
        }
        foreach (Transform child in _historyWrapper.transform)
        {
            GameObject card = child.gameObject;
            Destroy(card);
        }
    }
}
