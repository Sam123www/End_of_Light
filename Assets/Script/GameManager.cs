using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject UIPrefab;
    public float player_fullHP, player_fullLight;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            UIPrefab = Instantiate(UIPrefab);
            DontDestroyOnLoad(UIPrefab);
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
