using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    Animator anim;
    public int id;
    private void Start()
    {
        anim = GetComponent<Animator>();
        if(GameManager.instance.checkPointId == id)
        {
            anim.Play("Opened");
        }
        else
        {
            anim.Play("Idle");
        }
    }
    public void Enable()
    {
        anim.Play("Enable");
        GameManager.instance.playerPosition = transform.position;
        GameManager.instance.checkPointId = id;
    }
}
