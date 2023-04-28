using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TabController : MonoBehaviour
{
    [SerializeField] private GameObject _toDoTabButton, _doingTabButton, _doneTabButton, _exitButton;
    [SerializeField] private GameObject _toDoContent, _doingContent, _doneContent, _missionMenu;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void HideAllTabs()
    {
        _toDoContent.SetActive(false);
        _doingContent.SetActive(false);
        _doneContent.SetActive(false);

        _toDoTabButton.GetComponent<Button>().image.color = new Color32(176, 176, 176, 255);
        _doingTabButton.GetComponent<Button>().image.color = new Color32(176, 176, 176, 255);
        _doneTabButton.GetComponent<Button>().image.color = new Color32(176, 176, 176, 255);
    }

    public void ShowToDoTab()
    {
        HideAllTabs();
        _toDoTabButton.GetComponent<Button>().image.color = new Color32(255, 255, 255, 255);
        _toDoContent.SetActive(true);
    }

    public void ShowDoingTab()
    {
        HideAllTabs();
        _doingTabButton.GetComponent<Button>().image.color = new Color32(255, 255, 255, 255);
        _doingContent.SetActive(true);
    }

    public void ShowDoneTab()
    {
        HideAllTabs();
        _doneTabButton.GetComponent<Button>().image.color = new Color32(255, 255, 255, 255);
        _doneContent.SetActive(true);
    }

    public void ExitButton()
    {
        _missionMenu.SetActive(false);
    }
}
