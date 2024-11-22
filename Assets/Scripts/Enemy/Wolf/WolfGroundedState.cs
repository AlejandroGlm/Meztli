using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfGroundedState : EnemyState
{
    protected Enemy_Wolf enemy;
    protected Transform player;
    public WolfGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Wolf _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
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
        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.position) < 2)
            stateMachine.ChangeState(enemy.battleState);

        //// Si el lobo está en el suelo y el jugador está cerca, puede entrar en un estado de batalla o moverse
        //if (wolf.IsGroundDetected())
        //{
        //    // Si el jugador está cerca, cambiar al estado de batalla
        //    if (Vector2.Distance(wolf.transform.position, player.position) < 5)
        //    {
        //        stateMachine.ChangeState(wolf.battleState);
        //    }
        //}
        //else
        //{
        //    // Si no está en el suelo, cambiar a un estado aéreo o hacer otro comportamiento
        //    stateMachine.ChangeState(wolf.moveState);  // O cualquier estado que maneje lo que pasa cuando no está en el suelo
        //}
    }
}
