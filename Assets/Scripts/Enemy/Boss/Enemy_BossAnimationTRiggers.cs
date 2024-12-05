using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BossAnimationTRiggers : MonoBehaviour
{
private Enemy_Boss enemy => GetComponentInParent<Enemy_Boss>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
        void Start()
    {
        
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                PlayerStats target = hit.GetComponent<PlayerStats>();
                enemy.stats.DoDamage(target);
            }
        }
    }

    private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
    private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();


}
