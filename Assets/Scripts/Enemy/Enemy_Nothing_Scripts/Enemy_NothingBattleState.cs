using UnityEngine;

public class Enemy_NothingBattleState : Enemy_BattleState
{
    private Enemy_Nothing enemyNothing;
    private float lastTimeUsedRetreat = float.NegativeInfinity;


    public Enemy_NothingBattleState(Enemy enemy, StateMachine stateMachien, string animBoolName) : base(enemy, stateMachien, animBoolName)
    {
        enemyNothing = enemy as Enemy_Nothing;
    }

    public override void Enter()
    {
        base.Enter();

        if (ShouldRetreat()) // �÷��̾ ȸ�� ��Ÿ� ���� ������ ��
        {
            if (CanUseRetreatAbility())
            {
                Retreat();
            }
            else
            {
                ShortRetreat();
            }
        }
    }

    private void Retreat()
    {
        lastTimeUsedRetreat = Time.time;
        stateMachine.ChangeState(enemyNothing.nothingRetreatState);
    }
    private bool CanUseRetreatAbility() => Time.time > lastTimeUsedRetreat + enemyNothing.retreatCooldown; // �ɷ� ȸ�� ��Ÿ���� ���� ������ ��
}
