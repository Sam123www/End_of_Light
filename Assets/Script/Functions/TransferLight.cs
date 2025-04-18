using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TransferLight : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip enableClip;
    public GameObject[] PriorityObj, LowPriorityObj;
    Animator anim;
    Collider2D[] lightcol;
    public Light2D myLight;
    public float radius, delayTime;
    bool isEnable;
    public LayerMask lightLayer, groundLayer, trapLayer;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
    public void Disable()
    {
        isEnable = false;
        anim.Play("idle");
    }
    void SendMes(LayerMask layer, string message)
    {
        lightcol = Physics2D.OverlapCircleAll(transform.position, radius, layer);
        if (lightcol != null)
        {
            foreach (Collider2D collider in lightcol)
            {
                if (!Physics2D.Linecast(transform.position, collider.transform.position, groundLayer))
                {
                    collider.gameObject.SendMessage(message);
                }
            }
        }
    }
    private void Update()
    {
        if (isEnable)
        {
            SendMes(trapLayer, "Enable");
        }
    }
    IEnumerator toEnable()
    {
        audioSource.clip = enableClip;
        audioSource.Play();
        anim.Play("enable");
        if (PriorityObj.Length > 0)
        {
            foreach (GameObject obj in PriorityObj)
            {
                obj.gameObject.SendMessage("Enable");
            }
        }
        yield return new WaitForSeconds(delayTime);
        if (LowPriorityObj.Length > 0)
        {
            foreach (GameObject obj in LowPriorityObj)
            {
                obj.gameObject.SendMessage("Enable");
            }
        }
        SendMes(lightLayer, "Enable");
    }
}
