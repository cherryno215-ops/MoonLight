using UnityEngine;

public class Enemy_BattleState : EnemyState
{

    private Transform player;
    private float lastTimeWasInBattle; //���������� ������ ġ�� �ð�
    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        UpdateBattleTimer();
        player ??= enemy.GetPlayerRefernce(); // �÷��̾ null�� �� GetPlayerRefernce ���� �÷��̾� ��ȯ�� �ޱ� 

        

        if (ShouldRetreat()) // ȸ�� ���� ������ ��
        {
            ShortRetreat();
        }
    }

    protected void ShortRetreat()
    {

        rb.linearVelocity = new Vector2(enemy.retreatVelocity.x * -DirectionToPlayer(), enemy.retreatVelocity.y); // �÷��̾��� �ݴ� �������� ȸ��
        enemy.HandleFlip(DirectionToPlayer()); // �÷��̾� �� �ٶ󺸱�
    }

    public override void Update()
    {
        base.Update();

        if (enemy.PlayerDetected() == true) 
            UpdateBattleTimer();

        if (battleTimeIsOver() == true) 
        {
            stateMachine.ChangeState(enemy.idleState);
        }

        if (WithinAttackRange() && enemy.PlayerDetected()) // �÷��̾ �ڽ��� ���� ���� ���� ������ �� && �÷��̾ �������� ��
            stateMachine.ChangeState(enemy.attackState);

        else
            enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), rb.linearVelocity.y); // DirectionToPlayer() int ���� �޾Ƽ� �÷��̾��� ���⿡ ���� �߳��� 

    }

    protected void UpdateBattleTimer() => lastTimeWasInBattle = Time.time; // ���������� ������ ������ �ð� ���

    protected bool battleTimeIsOver() => Time.time > lastTimeWasInBattle + enemy.battleTimeDuration; // ������ �������� 5�ʰ� �����µ� �÷��̾ �������� �ʴ´ٸ�


    protected bool WithinAttackRange() => DistanceToPlayer() < enemy.attackDistance; // �÷��̾�� �ڽ��� �Ÿ����� ���� ��Ÿ��� �� ũ�� true�� ��ȯ
    protected bool ShouldRetreat() =>DistanceToPlayer() < enemy.minRetreatDistance; // �÷��̾���� �Ÿ����� ȸ�� ��Ÿ��� ũ�� true ��ȯ



    private float DistanceToPlayer()
    {
        if (player == null)
        {
            return float.MaxValue;
        }

        return Mathf.Abs(player.position.x - enemy.transform.position.x); // �÷��̾�� �ڽ��� �Ÿ� ���� ���ϱ�
    }

    protected int DirectionToPlayer()
    {
        if (player == null)
        {
            return 0;
        }
        return player.position.x > enemy.transform.position.x ? 1 : -1; // �÷��̾��� ��ġ�� ���� �߳��� ������ ����
    }

}
