using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpisodeTitle : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        if (GameManager.instance.firstIntoScene)
        {
            Debug.Log("first");
            AudioManager.PlayEnterGame();
            anim.Play("FadeIn");
        }
    }
}
