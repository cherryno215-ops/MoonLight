using UnityEngine;

public class Enemy_NothingRetreatState : EnemyState
{
    private Enemy_Nothing enemyNothing;
    private Vector3 startPosition; // 해당 능력의 시작 포지션
    private Transform player; // 플레이어 감지용 
    public Enemy_NothingRetreatState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        enemyNothing = enemy as Enemy_Nothing;
    }

    public override void Enter()
    {
        base.Enter();

        if (player == null)
        {
            player = enemy.GetPlayerRefernce(); // 플레이어가 감지되지 않았을 때 플레이어의 감지 하기

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
        return player.position.x > enemy.transform.position.x ? 1 : -1; // 플레이어의 위치가 자신 기준 좌, 우에 있는지 확인하는 플레이어 방향 감지 로직
    }
}
