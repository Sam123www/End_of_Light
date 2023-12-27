using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class playerAttack : MonoBehaviour
{
    public GameObject Light;
    public bool lightCheck;
    public float lightRange;
    public float hp;
    public LayerMask GhostMask;
    void Start()
    {
        
    }
    void Update()
    {
        lightCheck = Physics2D.OverlapCircle(Light.transform.position, lightRange, GhostMask);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(Light.transform.position, lightRange);
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
}
