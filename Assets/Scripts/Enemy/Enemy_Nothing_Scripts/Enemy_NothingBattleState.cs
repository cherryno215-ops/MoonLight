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

        if (ShouldRetreat()) // 플레이어가 회피 사거리 내로 들어왔을 때
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
    private bool CanUseRetreatAbility() => Time.time > lastTimeUsedRetreat + enemyNothing.retreatCooldown; // 능력 회피 쿨타임이 전부 돌았을 때
}
