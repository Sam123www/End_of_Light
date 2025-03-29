using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject UIPrefab, AudioManagerPrefab;
    public float player_fullHP, player_fullLight;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            UIPrefab = Instantiate(UIPrefab);
            DontDestroyOnLoad(UIPrefab);
            AudioManagerPrefab = Instantiate(AudioManagerPrefab);
            DontDestroyOnLoad(AudioManagerPrefab);
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
