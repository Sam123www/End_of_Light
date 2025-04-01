using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public SceneAsset nextScene;
    public void Enable()
    {
        GameManager.instance.Loading(nextScene.name);
    }
}
