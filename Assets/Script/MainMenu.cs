using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public SceneAsset playScene, settingScene, aboutAsScene;
    public UnityEvent onSelected;

    private void Start()
    {
        onSelected.Invoke();
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
 