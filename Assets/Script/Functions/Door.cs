using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public GameObject[] door;
    public Sprite open;
    public SceneAsset nextScene;
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
                StartCoroutine(Transmit());
                Time.timeScale = 0;
            }
        } 
    }

    IEnumerator Transmit()
    {
        player_UI.playerUI.Transmit();
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1;
        SceneManager.LoadScene(nextScene.name);
    }
}
