using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomb : MonoBehaviour
{
    Animator anim;
    public GameObject skeleton, blueLight;
    public int max_count;
    public float cd_low, cd_high, offset;
    bool isEnable = false;
    int count = 0;
    float timer, cd;
    void Start()
    {
        anim = GetComponent<Animator>();
        cd = Random.Range(cd_low, cd_high);
    }
    void Update()
    {
        if (isEnable == true && count <= max_count && timer + cd < Time.time)
        {
            cd = Random.Range(cd_low, cd_high);
            timer = Time.time;
            count++;
            Instantiate(skeleton, transform.position + Vector3.up * offset, Quaternion.identity).transform.SetParent(transform);
        }
    }
    public void Enable()
    {
        if (isEnable) return;
        isEnable = true;
        anim.Play("Enable");
        Invoke("Begin", 1);
    }
    void Begin()
    {
        isEnable = true;
    }
}
