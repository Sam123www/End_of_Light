using System.Collections;
using UnityEngine;

public class LightToLight : MonoBehaviour
{
    Animator anim;
    public GameObject light1, light2;
    public float delayTime;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Enable()
    {
        StartCoroutine(toEnable());
    }
    IEnumerator toEnable()
    {
        anim.Play("lighting");
        yield return new WaitForSeconds(delayTime);
        light1.gameObject.SendMessage("Enable");
        light2.gameObject.SendMessage("Enable");
    }
}
