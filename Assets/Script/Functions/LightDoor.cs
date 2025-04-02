using Cinemachine;
using System.Collections;
using UnityEngine;

public class LightDoor : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip enableClip, duringClip;
    Animator anim;
    CinemachineImpulseSource cis;
    public CinemachineImpulseDefinition cid;
    bool isEnable;
    public float firstShakeForce, shakeForce, shakeTime, shakeNum, waitTime;
    public void Enable()
    {
        if (isEnable) return;
        isEnable = true;
        audioSource = GetComponent<AudioSource>();
        cis = GetComponent<CinemachineImpulseSource>();
        cis.GenerateImpulse(firstShakeForce);
        StartCoroutine(Shaking());
        anim = GetComponent<Animator>();
        anim.Play("enable");
    }
    IEnumerator Shaking()
    {
        audioSource.clip = enableClip;
        audioSource.Play();
        yield return new WaitForSeconds(waitTime);
        audioSource.clip = duringClip;
        audioSource.loop = true;
        audioSource.Play();
        for(int i=0; i<shakeNum; i++)
        {
            cid.CreateEvent(transform.position, new Vector3(shakeForce, shakeForce, 0));
            yield return new WaitForSeconds(shakeTime);
        }
        audioSource.Stop();
    }
}
