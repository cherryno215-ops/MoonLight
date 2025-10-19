using UnityEngine;

public class Enemy_ReaperDoubleAttackState : EnemyState
{
    private Enemy_Reaper enemyReaper;
    public Enemy_ReaperDoubleAttackState(Enemy enemy, StateMachine stateMachien, string animBoolName) : base(enemy, stateMachien, animBoolName)
    {
        enemyReaper = enemy as Enemy_Reaper;
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
