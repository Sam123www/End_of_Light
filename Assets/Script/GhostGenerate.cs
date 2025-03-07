using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class GhostGenerate : MonoBehaviour
{
    public GameObject ghost;
    public float cycleTime;
    float Timer;
    bool inRange;
    public int num_max;
    int num = 0;

    void Start()
    {
        Timer = cycleTime;
    }
    void Update()
    {
        if(Time.time >= Timer && num <= num_max && inRange)
        {
            Instantiate(ghost, transform.position, Quaternion.Euler(0, 0, 0));
            Timer += cycleTime;
            num++;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        inRange = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
    }
}
