using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements.Experimental;

public class TransferLight : MonoBehaviour
{
    public GameObject[] lightLine;
    Animator anim;
    Collider2D[] lightcol;
    public Light2D myLight;
    public float radius, delayTime;
    bool isEnable;
    public LayerMask lightLayer;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    public void Enable()
    {
        if (isEnable) return;
        isEnable = true;
        StartCoroutine(toEnable());
    }
    IEnumerator toEnable()
    {
        lightcol = Physics2D.OverlapCircleAll(transform.position, radius, lightLayer);
        anim.Play("enable");
        if (lightLine.Length > 0)
        {
            foreach (GameObject obj in lightLine)
            {
                obj.gameObject.SendMessage("Enable");
            }
        }
        yield return new WaitForSeconds(delayTime);
        foreach (Collider2D col in lightcol)
        {
            col.gameObject.SendMessage("Enable");
        }
    }
}
