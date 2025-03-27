using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button: MonoBehaviour
{
    public GameObject mouseSelect, lantern;
    public Sprite lightOnLantern, lightOffLantern;
    private void OnMouseEnter()
    {
        lantern.GetComponent<SpriteRenderer>().sprite = lightOnLantern;
        mouseSelect.SetActive(true);
    }
    private void OnMouseExit()
    {
        lantern.GetComponent<SpriteRenderer>().sprite = lightOffLantern;
        mouseSelect.SetActive(false);
    }
}
