using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public GameObject[] door;
    public Sprite open;
    public string nextScene;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetAxis("Vertical") > 0.1f)
            {
                foreach (var item in door)
                {
                    item.gameObject.GetComponent<SpriteRenderer>().sprite = open;
                }
                Time.timeScale = 0;
                GameManager.instance.Loading(nextScene);
            }
        } 
    }
}
