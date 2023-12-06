using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class playerAttack : MonoBehaviour
{
    public float hp;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    void reduceHp(float harm)
    {
        hp -= harm;
        if (hp <= 0)
        {
            print("1");
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
