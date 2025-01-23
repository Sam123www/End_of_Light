using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class playerAttack : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform lightTrans, attackTrans;
    public bool lightCheck;
    public float hurtSpeed_x, hurtSpeed_y;
    public float lightRange, attackRange;
    public float damage;
    public LayerMask ghostLayer, enemyLayer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        lightCheck = Physics2D.OverlapCircle(lightTrans.position, lightRange, ghostLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(lightTrans.position, lightRange);
        Gizmos.DrawWireSphere(attackTrans.position, attackRange);
    }
    public void reduceHp(float[] harm)
    {
        StartCoroutine(immuneFrame());
        if (harm[1] == 0)
        {
            rb.velocity = new Vector2(hurtSpeed_x, hurtSpeed_y);
        }
        else
        {
            rb.velocity = new Vector2(-hurtSpeed_x, hurtSpeed_y);
        }
        player_UI.playerUI.HP.value -= harm[0] / player_UI.playerUI.fullHP;
        float now = float.Parse(player_UI.playerUI.HPnum.text);
        player_UI.playerUI.HPnum.text = (now - harm[0]).ToString();
        if (player_UI.playerUI.HP.value <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    IEnumerator immuneFrame()
    {
        playerController.player_controller.canMove = false;
        yield return new WaitForSeconds(0.5f);
        playerController.player_controller.canMove = true;
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
}
