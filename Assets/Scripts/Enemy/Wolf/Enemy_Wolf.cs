using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Wolf : Enemy
{
    #region States
    public WolfIdleState idleState { get; private set; }
    public WolfMoveState moveState { get; private set; }
    public WolfBattleState battleState { get; private set; }
    public WolfAttack1State attack1State { get; private set; }
    //public WolfAttack2State attack2State { get; private set; }
    //public WolfAttack3State attack3State { get; private set; }

    public WolfStunnedState stunnedState { get; private set; }
    
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // Inicialización de los estados
        idleState = new WolfIdleState(this, stateMachine, "Idle", this);
        moveState = new WolfMoveState(this, stateMachine, "Move", this);
        attack1State = new WolfAttack1State(this, stateMachine, "Attack1", this);
        //attack2State = new WolfAttack2State(this, stateMachine, "Attack2", this);
        //attack3State = new WolfAttack3State(this, stateMachine, "Attack3", this);
        battleState = new WolfBattleState(this, stateMachine, "Move", this);
        stunnedState = new WolfStunnedState(this, stateMachine, "Stunned", this);

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
}

