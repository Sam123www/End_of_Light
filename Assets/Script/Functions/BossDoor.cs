using UnityEditor;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    public string nextScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.Loading(nextScene);
        }
    }
}
