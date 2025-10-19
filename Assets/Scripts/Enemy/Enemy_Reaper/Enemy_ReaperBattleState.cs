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
                stateMachine.ChangeState(enemyReaper.reaperRoarState); // ��ȿ ���·� ��ȯ
                return; // �� �����ӿ� �ٸ� �ൿ ���� ����
            }
        }


        if (WithinAttackRange() && enemy.PlayerDetected()) // �÷��̾ �ڽ��� ���� ���� ���� ������ �� && �÷��̾ �������� ��
                stateMachine.ChangeState(enemyReaper.reaperAttackState);

            else
                enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), rb.linearVelocity.y); // DirectionToPlayer() int ���� �޾Ƽ� �÷��̾��� ���⿡ ���� �߳��� 

        
    }
}
