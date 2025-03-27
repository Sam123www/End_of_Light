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
    public GameObject lightMouse;
    public SceneAsset playScene, settingScene, aboutAsScene;
    private void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        lightMouse.transform.position = pos;
    }
    public void playBotton()
    {
        GameManager.instance.Loading(playScene.name);
    }
    public void settingBotton()
    {
        SceneManager.LoadScene(settingScene.name);
    }
    public void aboutAsBotton()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
    }
    public void quitBotton()
    {
        Application.Quit();
    }
}
 