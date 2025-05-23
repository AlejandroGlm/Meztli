using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGroundedState : EnemyState
{
    protected Enemy_Boss enemy;
    protected Transform player;
    public BossGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Boss enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.position) < 2)
            stateMachine.ChangeState(enemy.battleState);
    }
}
