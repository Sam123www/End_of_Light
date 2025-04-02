using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{
    Animator anim;
    public float damage, hold;
    int count = 0;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
     
    public IEnumerator Enable()
    {
        count++;
        anim.Play("enable");
        yield return new WaitForSeconds(hold);
        if(--count == 0)
        {
            anim.Play("disable");
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && damage > 0)
        {
            if (collision.transform.position.x > transform.position.x)
            {
                float[] data = { damage, 0 };
                collision.gameObject.SendMessage("reduceHp", data);
            }
            else
            {
                float[] data = { damage, 1 };
                collision.gameObject.SendMessage("reduceHp", data);
            }
            Debug.Log("hurt");
        }
    }
}
