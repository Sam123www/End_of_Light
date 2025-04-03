using UnityEditor;
using UnityEngine;
public class MainMenu : MonoBehaviour
{
    public GameObject lightMouse, Credits, OptionsPanel;
    public string playScene;
    private void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        lightMouse.transform.position = pos;
    }
    public void PlayBotton()
    {
        AudioManager.PlayEnterGame();
        GameManager.instance.Loading(playScene);
    }
    public void OptionsBotton()
    {
        AudioManager.PlayButtonClick();
        OptionsPanel.SetActive(true);
    }
    public void CreditsBotton()
    {
        AudioManager.PlayButtonClick();
        Credits.SetActive(true);
    }
    public void QuitBotton()
    {
        AudioManager.PlayButtonClick();
        Application.Quit();
    }
}
 