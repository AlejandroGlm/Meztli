using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBattleState : EnemyState
{
    private Transform player;
    private Enemy_Wolf enemy;
    private int moveDir;

    // Distancia mínima para atacar
    private float attackRange = 1.5f;
    // Distancia máxima para perseguir al jugador
    private float chaseRange = 50f;

    public WolfBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Wolf _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
    }

    public override void Update()
    {
        base.Update();

        // Calcular la distancia entre el lobo y el jugador
        float distanceToPlayer = Vector2.Distance(player.position, enemy.transform.position);

        // Si el jugador está dentro del rango de ataque, atacamos
        if (distanceToPlayer <= attackRange)
        {
            if (CanAttack())
            {
                stateMachine.ChangeState(enemy.attack1State);
                Debug.Log("Atacando al jugador");
                return; // Salir del Update, porque ya estamos atacando
            }
        }
        // Si el jugador está dentro del rango de persecución (y fuera del rango de ataque), perseguimos
        else if (distanceToPlayer <= chaseRange)
        {
            // Determinamos si el jugador está a la izquierda o derecha del lobo
            if (player.position.x > enemy.transform.position.x)
                moveDir = 1; // Mover a la derecha
            else if (player.position.x < enemy.transform.position.x)
                moveDir = -1; // Mover a la izquierda

            // Movemos al lobo hacia el jugador
            enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
        }
        else
        {
            // Si el jugador está fuera de rango de persecución, regresamos al estado idle
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemy.SetZeroVelocity();  // Detenemos el movimiento al salir del estado de batalla
    }

    private bool CanAttack()
    {
        // Verificamos si ha pasado suficiente tiempo desde el último ataque
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }
}
