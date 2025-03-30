using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAudio : MonoBehaviour
{
    public void _Run()
    {
        AudioManager.PlayRunAudio();
    }
    public void _Jump()
    {
        AudioManager.PlayJumpAudio();
    }
    public void _FallToGround()
    {
        AudioManager.PlayFallToGroundAudio();
    }
    public void _Attack()
    {
        AudioManager.PlayAttackAudio();
    }
    public void _HurtEnemy()
    {
        AudioManager.PlayHurtEnemyAudio();
    }
    public void _Hurt()
    {
        AudioManager.PlayHurtAudio();
    }
    public void _TurnOnLight()
    {
        AudioManager.PlayTurnOnLightAudio();
    }
    public void _TurnOffLight()
    {
        AudioManager.PlayTurnOffLightAudio();
    }
}
