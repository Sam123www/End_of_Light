using UnityEditor;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public string nextScene;
    public void Enable()
    {
        GameManager.instance.Loading(nextScene);
    }
}
