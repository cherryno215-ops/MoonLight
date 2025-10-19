using UnityEngine;

public class Enemy_ReaperAttackState : EnemyState
{
    private Enemy_Reaper enemyReaper;
    public Enemy_ReaperAttackState(Enemy enemy, StateMachine stateMachien, string animBoolName) : base(enemy, stateMachien, animBoolName)
    {
        enemyReaper = enemy as Enemy_Reaper;
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            stateMachine.ChangeState(enemyReaper.reaperBattleState);
        }
    }
}
