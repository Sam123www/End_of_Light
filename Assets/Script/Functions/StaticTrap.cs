using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTrap : MonoBehaviour
{
    public int damage;
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
