using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject skeleton;
    public int max_count;
    public float cd_low, cd_high;
    bool isEnable = false;
    int count = 0;
    float timer, cd;
    void Start()
    {
        cd = Random.Range(cd_low, cd_high);
    }
    void Update()
    {
        if (isEnable == true && count <= max_count && timer + cd < Time.time)
        {
            cd = Random.Range(cd_low, cd_high);
            timer = Time.time;
            count++;
            Instantiate(skeleton, transform.position, Quaternion.identity).transform.SetParent(transform);
        }
    }
    
    public void Enable()
    {
        GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 1, 1);
        Invoke("Begin", 1);
    }
    void Begin()
    {
        isEnable = true;
    }
}
