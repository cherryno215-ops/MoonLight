using UnityEngine;

public class Enemy_NothingRetreatState : EnemyState
{
    private Enemy_Nothing enemyNothing;
    private Vector3 startPosition; // �ش� �ɷ��� ���� ������
    private Transform player; // �÷��̾� ������ 
    public Enemy_NothingRetreatState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        enemyNothing = enemy as Enemy_Nothing;
    }

    public override void Enter()
    {
        base.Enter();

        if (player == null)
        {
            player = enemy.GetPlayerRefernce(); // �÷��̾ �������� �ʾ��� �� �÷��̾��� ���� �ϱ�

        }

        startPosition = enemy.transform.position;

        rb.linearVelocity = new Vector2(enemyNothing.retreatSpeed * -DirectionToPlayer(), 0);
        enemy.HandleFlip(DirectionToPlayer());
    } 


    public override void Update()
    {
        base.Update();

        bool rechedMaxDistance = Vector2.Distance(enemy.transform.position, startPosition) > enemyNothing.retreatMaxDistance;

        if (rechedMaxDistance)
        {
            rb.linearVelocity = Vector2.zero;
            stateMachine.ChangeState(enemy.battleState);
        }
        stateMachine.ChangeState(enemy.battleState);
    }

    private int DirectionToPlayer()
    {
        if (player == null)
        {
            return 0;
        }
        return player.position.x > enemy.transform.position.x ? 1 : -1; // �÷��̾��� ��ġ�� �ڽ� ���� ��, �쿡 �ִ��� Ȯ���ϴ� �÷��̾� ���� ���� ����
    }
}
