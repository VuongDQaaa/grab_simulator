using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
public class UIController : MonoBehaviour
{

    public GameObject LoadScene;
    public Slider sldLoadScene;
    public TextMeshProUGUI txtLoad;

    private void Awake() {
        LoadScene.SetActive(false);
    }

// Change Load Game Play from mission to main game play
    public void _LoadGamePlay() {
        SceneManager.LoadScene("Game Play");
    }

    public void _LoadMenu(){
        StartCoroutine(_LoadScene(0));
    }

    IEnumerator _LoadScene(int indexScene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(indexScene);
        float progress = 0;
        LoadScene.SetActive(true);
        while (!operation.isDone)
        {
            progress = Mathf.Clamp01(operation.progress / 0.9f);
            sldLoadScene.value = progress;
            txtLoad.SetText(progress + "%");

            yield return null;
        }
    }

    public void _BtnQuit(){
        Application.Quit();
    }
    
}
