using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class playerAttack : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    public GameObject hurt_effect;
    public Transform lightTrans, attackTrans;
    public float hurtSpeed_x, hurtSpeed_y;
    public float lightRange, attackRange;
    public float damage;
    bool immune = false;
    public float immuneFlashTime;
    public int immueFlashCount;
    public LayerMask ghostLayer, enemyLayer, tombLayer, trapLayer, groundLayer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Light();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(lightTrans.position, lightRange);
        Gizmos.DrawWireSphere(attackTrans.position, attackRange);
    }
    public void reduceHp(float[] harm)
    {
        if (immune) return;
        StartCoroutine(immuneFrame());
        hurt_effect.SetActive(false);
        hurt_effect.SetActive(true);
        if (harm[1] == 0)
        {
            rb.velocity = new Vector2(hurtSpeed_x, hurtSpeed_y);
        }
        else
        {
            rb.velocity = new Vector2(-hurtSpeed_x, hurtSpeed_y);
        }
        player_UI.playerUI.HP.value -= harm[0];
        if (player_UI.playerUI.HP.value <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    IEnumerator immuneFrame()
    {
        if (immune) yield return null;
        playerController.instance.canMove = false;
        immune = true;
        for(int i = 0; i < immueFlashCount; i++)
        {
            yield return new WaitForSeconds(immuneFlashTime);
            sr.enabled = !sr.enabled;
        }
        playerController.instance.canMove = true;
        for (int i = 0; i < immueFlashCount; i++)
        {
            yield return new WaitForSeconds(immuneFlashTime);
            sr.enabled = !sr.enabled;
        }
        immune = false;
        yield return null;
    }

    public void Attack()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackTrans.position, attackRange, enemyLayer);
        foreach(Collider2D collider in detectedObjects)
        {
            Debug.Log(collider.gameObject.name);
            if(collider.transform.position.x > transform.position.x)
            {
                float[] data = { damage, 0 };
                collider.gameObject.SendMessage("onDamage", data);
            }
            else
            {
                float[] data = { damage, 1 };
                collider.gameObject.SendMessage("onDamage", data);
            }
        }
    }
    void Light()
    {
        Collider2D[] lightCheckGhost = Physics2D.OverlapCircleAll(lightTrans.position, lightRange, ghostLayer);
        if (lightCheckGhost != null && playerController.instance.Light.activeInHierarchy)
        {
            foreach(Collider2D collider in lightCheckGhost)
            {
                if (!Physics2D.Linecast(transform.position, collider.transform.position, groundLayer)){
                    
                    collider.gameObject.SendMessage("Avoid");
                }
            }
        }
        Collider2D[] lightCheckTomb = Physics2D.OverlapCircleAll(lightTrans.position, lightRange, tombLayer);
        if (lightCheckGhost != null && playerController.instance.Light.activeInHierarchy)
        {
            foreach (Collider2D collider in lightCheckTomb)
            {
                if (!Physics2D.Linecast(transform.position, collider.transform.position, groundLayer))
                {
                    collider.gameObject.SendMessage("Enable");
                }
            }
        }
        Collider2D[] lightCheckTrap = Physics2D.OverlapCircleAll(lightTrans.position, lightRange, trapLayer);
        if (lightCheckTrap != null && playerController.instance.Light.activeInHierarchy)
        {
            foreach (Collider2D collider in lightCheckTrap)
            {
                if (!Physics2D.Linecast(transform.position, collider.transform.position, groundLayer))
                {
                    collider.gameObject.SendMessage("Enable");
                }
            }
        }
    }
}
