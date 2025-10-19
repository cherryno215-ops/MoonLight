using UnityEngine;

public class EnemyCorpse : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        //Enemy���� �ʿ��� ���µ鸸 �̰����� ���µ��� �� �Ҵ�
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
