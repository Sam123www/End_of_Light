using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_UI : MonoBehaviour
{
    public static player_UI playerUI;
    public Text HPnum;
    public float fullHP;
    public Slider HP;
    void Awake()
    {
        if (playerUI == null) { playerUI = this; }
        HP.value = 1;
        HPnum.text = fullHP.ToString();
    }
}
