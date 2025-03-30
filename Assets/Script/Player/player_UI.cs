using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_UI : MonoBehaviour
{
    public static player_UI playerUI;
    [Header("hp, light")]
    public Text HPnum, lightnum;
    public float LightIncreaseSpeed, LightDecreaseSpeed;
    public Slider HP, Light;
    bool isIncreasing;
    [Header("textTrigger")]
    public string[] text;
    void Start()
    {
        if (playerUI == null) { playerUI = this; }
        HP.maxValue = GameManager.instance.player_fullHP;
        HP.value = GameManager.instance.player_fullHP;
        Light.maxValue = GameManager.instance.player_fullLight;
        Light.value = GameManager.instance.player_fullLight;
        StartCoroutine(decreaseLight());
        StartCoroutine(increaseLight());
    }
    void Update()
    {
        lightnum.text = Light.value.ToString();
        HPnum.text = HP.value.ToString();
    }
    IEnumerator decreaseLight()
    {
        yield return new WaitForSeconds(0.3f);
        if (playerController.instance.Light.activeInHierarchy == true)
        {
            isIncreasing = false;
            Light.value -= LightDecreaseSpeed;
        }
        if (Light.value <= 0 && playerController.instance.isLighting)
        {
            playerController.instance.isLighting = false;
            playerController.instance.turnOffLight = true;
            Light.value = 0;
        }
        StartCoroutine(decreaseLight());
        yield return null;
    }
    IEnumerator increaseLight()
    {
        if (isIncreasing)
        {
            yield return new WaitForSeconds(0.3f);
        }
        else
        {
            yield return new WaitForSeconds(2);
        }
        if(Light.value < GameManager.instance.player_fullLight && playerController.instance.Light.activeInHierarchy == false)
        {
            isIncreasing = true;
            Light.value += LightIncreaseSpeed;
        }
        if(Light.value >= GameManager.instance.player_fullLight)
        {
            Light.value = GameManager.instance.player_fullLight;
        }
        StartCoroutine(increaseLight());
        yield return null;
    }
    public void Transmit()
    {
        GetComponent<Animator>().Play("transmit");
    }
}
