using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDoor : MonoBehaviour
{
    public SceneAsset nextScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.Loading(nextScene.name);
        }
    }
}
