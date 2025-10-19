using System.Xml;
using UnityEngine;

public class Enemy_Reaper : Enemy
{
    public Enemy_ReaperAttackState reaperAttackState { get; private set; }

    public Enemy_ReaperBattleState reaperBattleState { get; private set; }
    public Enemy_ReaperDoubleAttackState reaperDubleAttackState { get; private set; }
    public Enemy_ReaperRoarState reaperRoarState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        //Enemy에서 필요한 상태들만 이곳에서 상태들의 값 할당
        idleState = new Enemy_IdleState(this, stateMachine, "idle");
        moveState = new Enemy_MoveState(this, stateMachine, "move");
        deadState = new Enemy_DeadState(this, stateMachine, "dead");

        reaperBattleState = new Enemy_ReaperBattleState(this, stateMachine, "battle");
        reaperAttackState = new Enemy_ReaperAttackState(this, stateMachine, "attack");
        reaperRoarState = new Enemy_ReaperRoarState(this, stateMachine, "roar");
        reaperDubleAttackState = new Enemy_ReaperDoubleAttackState(this, stateMachine, "doubleAttack");

        battleState = reaperBattleState;


    }


    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

}
