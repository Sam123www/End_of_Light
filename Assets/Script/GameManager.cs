using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float player_hp, player_light;
    void Awake()
    {
        if(instance == null)
        {
            player_light = 1;
            player_hp = 1;
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        
    }
}
