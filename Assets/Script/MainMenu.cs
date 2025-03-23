using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public SceneAsset playScene, settingScene, aboutAsScene;

    public void playBotton()
    {
        SceneManager.LoadScene(playScene.name);
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
 