using UnityEngine;

public class Enemy_BattleState : EnemyState
{

    private Transform player;
    private float lastTimeWasInBattle; //마지막으로 전투를 치른 시간
    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        UpdateBattleTimer();
        player ??= enemy.GetPlayerRefernce(); // 플레이어가 null일 때 GetPlayerRefernce 통해 플레이어 반환값 받기 

        

        if (ShouldRetreat()) // 회피 각이 나왔을 때
        {
            ShortRetreat();
        }
    }

    protected void ShortRetreat()
    {

        rb.linearVelocity = new Vector2(enemy.retreatVelocity.x * -DirectionToPlayer(), enemy.retreatVelocity.y); // 플레이어의 반대 방향으로 회피
        enemy.HandleFlip(DirectionToPlayer()); // 플레이어 쪽 바라보기
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

        if (WithinAttackRange() && enemy.PlayerDetected()) // 플레이어가 자신의 공격 범위 내에 들어왔을 때 && 플레이어가 감지됬을 때
            stateMachine.ChangeState(enemy.attackState);

        else
            enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), rb.linearVelocity.y); // DirectionToPlayer() int 값을 받아서 플레이어의 방향에 따라 추노함 

    }

    protected void UpdateBattleTimer() => lastTimeWasInBattle = Time.time; // 마지막으로 감지한 전투의 시간 경과

    protected bool battleTimeIsOver() => Time.time > lastTimeWasInBattle + enemy.battleTimeDuration; // 전투를 시작한지 5초가 지났는데 플레이어가 감지되지 않는다면


    protected bool WithinAttackRange() => DistanceToPlayer() < enemy.attackDistance; // 플레이어와 자신의 거리보다 공격 사거리가 더 크면 true를 반환
    protected bool ShouldRetreat() =>DistanceToPlayer() < enemy.minRetreatDistance; // 플레이어와의 거리보다 회피 사거리가 크면 true 반환



    private float DistanceToPlayer()
    {
        if (player == null)
        {
            return float.MaxValue;
        }

        return Mathf.Abs(player.position.x - enemy.transform.position.x); // 플레이어와 자신의 거리 절댓값 구하기
    }

    protected int DirectionToPlayer()
    {
        if (player == null)
        {
            return 0;
        }
        return player.position.x > enemy.transform.position.x ? 1 : -1; // 플레이어의 위치에 따라 추노의 여지를 남김
    }

}
