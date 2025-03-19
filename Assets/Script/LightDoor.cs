using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDoor : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Enable()
    {
        anim.Play("enable");
    }
}
