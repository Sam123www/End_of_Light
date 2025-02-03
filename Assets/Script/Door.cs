using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Door : MonoBehaviour
{
    public GameObject[] door;
    public GameObject openKey;
    public Sprite open;
    public int nextScene;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            openKey.SetActive(true);
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
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            openKey.SetActive(false);
        }
    }

    IEnumerator Transmit()
    {
        player_UI.playerUI.Transmit();
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1;
        SceneManager.LoadScene(nextScene);
    }
}
