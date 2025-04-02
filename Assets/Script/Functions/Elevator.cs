using Cinemachine;
using System.Collections;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    AudioSource audioSource;
    public CinemachineImpulseDefinition cid;
    public AudioClip duringClip, endClip;
    public float speed, shakeForce, shakeTime;
    public Transform upPoint, downPoint;
    bool toUp = true;
    public GameObject lightObj;
    Rigidbody2D rb;
    public void Enable()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        if (toUp)
        {
            rb.velocity = Vector2.up * speed;
        }
        else
        {
            rb.velocity = Vector2.down * speed;
        }
        StartCoroutine(Shaking());
    }
    private void FixedUpdate()
    {
        if (toUp && transform.position.y >= upPoint.position.y)
        {
            toUp = false;
            rb.velocity = Vector2.zero;
            lightObj.SendMessage("Disable");
        }
        if(!toUp && transform.position.y <= downPoint.position.y)
        {
            toUp = true;
            rb.velocity = Vector2.zero;
            lightObj.SendMessage("Disable");
        }
    }
    IEnumerator Shaking()
    {
        audioSource.clip = duringClip;
        audioSource.loop = true;
        audioSource.Play();
        while (Mathf.Abs(rb.velocity.y) > 0.1f)
        {
            cid.CreateEvent(transform.position, new Vector3(shakeForce, shakeForce, 0));
            yield return new WaitForSeconds(shakeTime);
        }
        audioSource.loop = false;
        audioSource.clip = endClip;
        audioSource.Play();
    }
}
