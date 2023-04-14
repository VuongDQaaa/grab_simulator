using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject loseUI;
    [SerializeField] GameObject winUI;
    private void Awake() {
        if(instance == null){
            instance = this;
        }
    }
    public void GameOver(){
        loseUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Winning(){
        winUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
