using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_UI : MonoBehaviour
{
    public static player_UI playerUI;
    [Header("hp, light")]
    public Text HPnum, lightnum;
    public float fullHP, fullLight, LightIncreaseSpeed, LightDecreaseSpeed;
    public Slider HP, Light;
    bool isIncreasing;
    [Header("textTrigger")]
    public string[] text;
    void Start()
    {
        if (playerUI == null) { playerUI = this; }
        HP.value = GameManager.instance.player_hp;
        HPnum.text = fullHP.ToString();
        Light.value = GameManager.instance.player_light;
        lightnum.text = fullLight.ToString();
        StartCoroutine(decreaseLight());
        StartCoroutine(increaseLight());
    }
    IEnumerator decreaseLight()
    {
        yield return new WaitForSeconds(0.3f);
        if (playerController.player_controller.Light.activeInHierarchy == true)
        {
            isIncreasing = false;
            Light.value -= LightDecreaseSpeed / fullLight;
            lightnum.text = (float.Parse(lightnum.text) - LightDecreaseSpeed).ToString();
        }
        if (Light.value <= 0)
        {
            playerController.player_controller.turnOffLight = true;
            playerController.player_controller.isLighting = false;
            Light.value = 0;
            lightnum.text = 0.ToString();
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
        if(Light.value < 1 && playerController.player_controller.Light.activeInHierarchy == false)
        {
            isIncreasing = true;
            Light.value += LightIncreaseSpeed / fullLight;
            lightnum.text = (float.Parse(lightnum.text) + LightIncreaseSpeed).ToString();
        }
        if(Light.value >= 1)
        {
            lightnum.text = fullLight.ToString();
            Light.value = 1;
        }
        StartCoroutine(increaseLight());
        yield return null;
    }
    public void Transmit()
    {
        GetComponent<Animator>().Play("transmit");
    }
}
