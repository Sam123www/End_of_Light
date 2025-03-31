using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Button: MonoBehaviour
{
    public GameObject mouseSelect, lantern;
    public MainMenu menu;
    public Sprite lightOnLantern, lightOffLantern;
    private void OnMouseEnter()
    {
        AudioManager.PlayButtonSelect();
        lantern.GetComponent<SpriteRenderer>().sprite = lightOnLantern;
        mouseSelect.SetActive(true);
        menu.lightEnable = true;
    }
    private void OnMouseExit()
    {
        lantern.GetComponent<SpriteRenderer>().sprite = lightOffLantern;
        mouseSelect.SetActive(false);
        menu.lightEnable = false;
    }
}
