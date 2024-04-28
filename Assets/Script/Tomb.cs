using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject skeleton;
    public int max_count;
    public float cd_low, cd_high;
    int count = 0;
    float timer, cd;
    void Start()
    {
        cd = Random.Range(cd_low, cd_high);
    }
    void Update()
    {

        if(count <= max_count && timer + cd < Time.time)
        {
            cd = Random.Range(cd_low, cd_high);
            timer = Time.time;
            count++;
            Instantiate(skeleton, transform.position, Quaternion.identity);
        }
    }
}
