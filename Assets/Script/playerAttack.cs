using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class playerAttack : MonoBehaviour
{
    public Transform lightTrans, attackTrans;
    public bool lightCheck;
    public float lightRange, attackRange;
    public float hp;
    public LayerMask ghostLayer, enemyLayer;
    void Start()
    {
        
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
    void reduceHp(float harm)
    {
        hp -= harm;
        if (hp <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            reduceHp(1f);
        }
    }
    public void Attack()
    {
        
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackTrans.position, attackRange, enemyLayer);
        foreach(Collider2D collider in detectedObjects)
        {
            Debug.Log(collider.gameObject.name);
        }
    }
    public void AttackEnd()
    {
        playerController.PlayerController.isAttacking = false;
    }
}
