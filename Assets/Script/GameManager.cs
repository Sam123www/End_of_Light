using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float player_hp, player_light;
    public int area_id;
    public GameObject[] area;
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
    public void ChangeArea(int nowId)
    {
        for(int i=0; i<area.Length; i++)
        {
            if (nowId == i)
            {
                foreach (GameObject child in area[i].GetComponentsInChildren<GameObject>())
                {
                    child.SetActive(true);
                }
            }
            else
            {
                foreach (GameObject child in area[i].GetComponentsInChildren<GameObject>())
                {
                    child.SetActive(false);
                }
            }
        }
    }
}
