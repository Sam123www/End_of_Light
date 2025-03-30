using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject UIPrefab;
    public int checkPointId;
    public float player_fullHP, player_fullLight;
    public bool isNewLevel;
    public Vector3 playerPosition;
    void Awake()
    {
        Time.timeScale = 1; 
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            UIPrefab = Instantiate(UIPrefab);
            DontDestroyOnLoad(UIPrefab);
        }
        else if (isNewLevel)
        {
            instance = this;
            checkPointId = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Loading(string nextScene)
    {
        UIPrefab.SendMessage("Enable", nextScene);
    }
}
