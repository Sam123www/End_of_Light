using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip enableClip;
    Animator anim;
    public bool isEnable;
    public int id;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        if(GameManager.instance.checkPointId == id)
        {
            isEnable = true;
            anim.Play("Opened");
            GameManager.instance.playerPosition = transform.position;
            GameObject.Find("player").transform.position = transform.position;
        }
        else
        {
            anim.Play("Idle");
        }
    }
    public void Enable()
    {
        if (isEnable) return;
        isEnable = true;
        audioSource.clip = enableClip;
        audioSource.Play();
        anim.Play("Enable");
        GameManager.instance.playerPosition = transform.position;
        GameManager.instance.checkPointId = id;
    }
}
