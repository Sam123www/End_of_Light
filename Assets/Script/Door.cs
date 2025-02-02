using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Door : MonoBehaviour
{
    public GameObject door_L, door_R;
    public Sprite open;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetAxis("Vertical") > 0.1f){
            door_L.GetComponent<SpriteRenderer>().sprite = open;
            door_R.GetComponent<SpriteRenderer>().sprite = open;
        } 
    }
}
