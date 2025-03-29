using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class playerAttack : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    CinemachineImpulseSource cis;
    public GameObject hurt_effect;
    public Transform lightTrans, attackTrans;
    public float hurtSpeed_x, hurtSpeed_y;
    public float lightRange, attackRange;
    public float damage;
    bool immune = false;
    public float immuneFlashTime;
    public int immueFlashCount;
    public LayerMask ghostLayer, enemyLayer, tombLayer, trapLayer, groundLayer, transferLightLayer;
    [Header("CameraShake")]
    public float hurtShakeForce, attackShakeForce;
    void Start()
    {
        cis = GetComponent<CinemachineImpulseSource>();
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
        AudioManager.PlayHurtAudio();
        cis.GenerateImpulse(new Vector3(hurtShakeForce, 1, 0).normalized);
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
            StartCoroutine(Dying());
        }
    }
    IEnumerator Dying()
    {
        Time.timeScale = 0;
        AudioManager.MuteAll();
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1;
        GameManager.instance.SendMessage("Loading", SceneManager.GetActiveScene().name);
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
        if(detectedObjects.Length > 0)
        {
            AudioManager.PlayHurtEnemyAudio();
            if (transform.rotation.y == 0)
            {
                cis.GenerateImpulse(Vector3.left * attackShakeForce);
            }
            else
            {
                cis.GenerateImpulse(Vector3.right * attackShakeForce);
            }
            foreach (Collider2D collider in detectedObjects)
            {
                if (collider.transform.position.x > transform.position.x)
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
    void SendMes(LayerMask layer, string message)
    {
        Collider2D[] lightCheck = Physics2D.OverlapCircleAll(lightTrans.position, lightRange, layer);
        if (lightCheck != null && playerController.instance.Light.activeInHierarchy)
        {
            foreach (Collider2D collider in lightCheck)
            {
                if (!Physics2D.Linecast(transform.position, collider.transform.position, groundLayer))
                {
                    collider.gameObject.SendMessage(message);
                }
            }
        }
    }
    void Light()
    {
        SendMes(ghostLayer, "Avoid");
        SendMes(tombLayer, "Enable");
        SendMes(trapLayer, "Enable");
        SendMes(transferLightLayer, "Enable");
    }
}
