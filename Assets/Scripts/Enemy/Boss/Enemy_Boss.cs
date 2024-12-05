using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para manejar escenas


public class Enemy_Boss : Enemy
{

    #region States
    public BossIdleState idleState { get; private set; }
    public BossMoveState moveState { get; private set; }
    public BossBattleState battleState { get; private set; }
    public BossAttack1State attack1State { get; private set; }
    public BossStunnedState stunnedState { get; private set; }

    public BossDeadState deadState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new BossIdleState(this, stateMachine, "Idle", this);
        moveState = new BossMoveState(this, stateMachine, "Move", this);
        attack1State = new BossAttack1State(this, stateMachine, "Attack1",this);
        battleState = new BossBattleState(this, stateMachine, "Move", this);
        stunnedState = new BossStunnedState(this, stateMachine, "Stunned", this);
        deadState = new BossDeadState(this, stateMachine, "Idle", this);


    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState); // Comienza con el estado Idle
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.U))
        {
            stateMachine.ChangeState(stunnedState);
        }
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }
        return false;
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);

        StartCoroutine(LoadSceneAfterDelay("Level2", 2f)); 
    }

    private IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay); // Espera antes de cambiar de escena
        SceneManager.LoadScene(sceneName);
    }
}
