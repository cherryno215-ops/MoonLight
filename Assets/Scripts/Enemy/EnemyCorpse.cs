using UnityEngine;

public class EnemyCorpse : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        //Enemy에서 필요한 상태들만 이곳에서 상태들의 값 할당
        idleState = new Enemy_IdleState(this, stateMachine, "idle");
        moveState = new Enemy_MoveState(this, stateMachine, "move");
        attackState = new Enemy_AttackState(this, stateMachine, "attack");
        battleState = new Enemy_BattleState(this, stateMachine, "battle");
        deadState = new Enemy_DeadState(this, stateMachine, "dead");

    }


    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }
}
