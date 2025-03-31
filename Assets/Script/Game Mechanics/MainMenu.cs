using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public GameObject lightMouse, Credits;
    public bool lightEnable;
    public SceneAsset playScene, OptionsScene, aboutAsScene;
    private void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        lightMouse.transform.position = pos;
    }
    public void PlayBotton()
    {
        if(lightEnable)
        {
            AudioManager.PlayEnterGame();
            GameManager.instance.Loading(playScene.name);
        }
    }
    public void OptionsBotton()
    {
        if (lightEnable)
        {
            AudioManager.PlayButtonClick();
            SceneManager.LoadScene(OptionsScene.name);
        }
    }
    public void CreditsBotton()
    {
        if (lightEnable)
        {
            AudioManager.PlayButtonClick();
            
            Application.OpenURL("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
        }
    }
    public void QuitBotton()
    {
        if (lightEnable)
        {
            AudioManager.PlayButtonClick();
            Application.Quit();
        }
    }
}
 