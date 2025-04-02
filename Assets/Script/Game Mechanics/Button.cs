using UnityEngine;

public class Button: MonoBehaviour
{
    public GameObject mouseSelect, lantern;
    public Sprite lightOnLantern, lightOffLantern;
    public void MouseEnter()
    {
        AudioManager.PlayButtonSelect();
        lantern.GetComponent<SpriteRenderer>().sprite = lightOnLantern;
        mouseSelect.SetActive(true);
    }
    public void MouseExit()
    {
        lantern.GetComponent<SpriteRenderer>().sprite = lightOffLantern;
        mouseSelect.SetActive(false);
    }
}
