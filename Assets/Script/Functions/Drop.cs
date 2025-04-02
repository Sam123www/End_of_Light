using Cinemachine;
using UnityEngine;

public class Drop : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip hitGroundClip;
    bool isEnable, isStop;
    public float stop, offset_stop, shakeForce;
    public Vector2 old_pos;
    Rigidbody2D rb;
    CinemachineImpulseSource cis;
    void Awake()
    {
        old_pos = transform.position;
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        cis = GetComponent<CinemachineImpulseSource>();
        rb.bodyType = RigidbodyType2D.Static;
    }

    void Update()
    {
        if(!isStop && transform.position.y <= old_pos.y + stop)
        {
            audioSource.clip = hitGroundClip;
            audioSource.Play();
            isStop = true;
            cis.GenerateImpulse(shakeForce);
            rb.bodyType = RigidbodyType2D.Static;
            transform.position = new Vector2(transform.position.x, old_pos.y + stop);
        }
    }
    public void Enable()
    {
        if (isEnable) return;
        isEnable = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y + stop + offset_stop), new Vector2(1, 0.1f));
    }
}
