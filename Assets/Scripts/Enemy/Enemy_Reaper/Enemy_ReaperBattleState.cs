using UnityEngine;

public class Enemy_ReaperBattleState : Enemy_BattleState
{
    private Enemy_Reaper enemyReaper;
    private float roarCooldown = 4f;
    private float specialTimer = 0f;

    public Enemy_ReaperBattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        enemyReaper = enemy as Enemy_Reaper;
    }

    public override void Update()
    {
        stateTimer -= Time.deltaTime;
        UpdateAnimationParameters();


        if (enemy.PlayerDetected())
        {
            specialTimer += Time.deltaTime;

            if (specialTimer >= roarCooldown)
            {
                specialTimer = 0f;
                stateMachine.ChangeState(enemyReaper.reaperRoarState); // 포효 상태로 전환
                return; // 이 프레임에 다른 행동 하지 않음
            }
        }


        if (WithinAttackRange() && enemy.PlayerDetected()) // 플레이어가 자신의 공격 범위 내에 들어왔을 때 && 플레이어가 감지됬을 때
                stateMachine.ChangeState(enemyReaper.reaperAttackState);

            else
                enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), rb.linearVelocity.y); // DirectionToPlayer() int 값을 받아서 플레이어의 방향에 따라 추노함 

        
    }
}
