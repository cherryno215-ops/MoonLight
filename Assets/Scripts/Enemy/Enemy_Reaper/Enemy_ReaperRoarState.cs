using UnityEngine;
using System;
using System.Collections;

public class Enemy_ReaperRoarState : EnemyState
{
    private Enemy_Reaper enemyReaper;

    private float roarTime = 2f;

    public Enemy_ReaperRoarState(Enemy enemy, StateMachine stateMachien, string animBoolName) : base(enemy, stateMachien, animBoolName)
    {
        enemyReaper = enemy as Enemy_Reaper;
    }



    public override void Enter()
    {
        base.Enter();
        rb.linearVelocity = Vector2.zero;
        enemyReaper.StartCoroutine(RoarWait());
    }

    private IEnumerator RoarWait()
    {
        yield return new WaitForSeconds(roarTime);

        stateMachine.ChangeState(enemyReaper.reaperDubleAttackState);
    }

}
