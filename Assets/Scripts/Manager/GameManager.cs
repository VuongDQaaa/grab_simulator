using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private const string _saveGold = "0";
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void IsFirstTimeStart()
    {
        if (!PlayerPrefs.HasKey("IsFirstTimeStart"))
        {
            PlayerPrefs.SetInt(_saveGold, 0);
            PlayerPrefs.SetInt("IsFirstTimeStart", 0);
        }
    }

    public void SetGold(int gold)
    {
        PlayerPrefs.SetInt(_saveGold, gold);
    }

    public int GetGold()
    {
        return PlayerPrefs.GetInt(_saveGold);
    }
}
