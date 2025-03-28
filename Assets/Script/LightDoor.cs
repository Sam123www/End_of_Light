using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDoor : MonoBehaviour
{
    Animator anim;
    CinemachineImpulseSource cis;
    public CinemachineImpulseDefinition cid;
    bool isEnable;
    public float firstShakeForce, shakeForce, shakeTime, shakeNum, waitTime;
    public void Enable()
    {
        if (isEnable) return;
        isEnable = true;
        cis = GetComponent<CinemachineImpulseSource>();
        cis.GenerateImpulse(firstShakeForce);
        StartCoroutine(Shaking());
        anim = GetComponent<Animator>();
        anim.Play("enable");
    }
    IEnumerator Shaking()
    {
        yield return new WaitForSeconds(waitTime);
        for(int i=0; i<shakeNum; i++)
        {
            cid.CreateEvent(transform.position, new Vector3(shakeForce, shakeForce, 0));
            yield return new WaitForSeconds(shakeTime);
        }
    }
}
