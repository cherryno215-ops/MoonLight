using UnityEngine;

public class Enemy : Entity
{
    

    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;
    public EnemyState attackState;
    public Enemy_BattleState battleState;
    public Enemy_DeadState deadState;

    [Header("Battle details")]
    public float battleMoveSpeed = 7;
    public float attackDistance = 2;
    public float battleTimeDuration = 5; // 최대 전투 시간
    public float minRetreatDistance = 2; // 후퇴 가능한 사거리
    public Vector2 retreatVelocity; // 후퇴 이동

    [Header("Movement datails")]
    public float idleTime = 1.5f;
    public float moveSpeed = 1.5f;

    [Header("Player detection")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance = 10; //플레이어 감지거리
    public Transform player { get; private set; }

    public virtual void SpecialAttack()
    {

    }


    public override void EntityDeath()
    {
        base.EntityDeath();

        stateMachine.ChangeState(deadState);
    }

    public void TryEnterBattleState(Transform player) // 
    {
        if (stateMachine.currentState == battleState) //배틀 상태 일 때
        {
            return;
        }
        if (stateMachine.currentState == attackState) // 공격 상태일 때
        {
            return;
        }
        this.player = player;
        stateMachine.ChangeState(battleState);
    }

    public Transform GetPlayerRefernce() // 플레이어가 null 일 때 플레이어 정보 받기
    {
        if (player == null)
        {
            player = PlayerDetected().transform;
        }
        return player;
    }

    public RaycastHit2D PlayerDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, playerCheckDistance, whatIsPlayer | whatIsGround); // 플레이어 감지 (플레이어의 모든 정보를 전부 감지함, true / false 포함)

        if (hit.collider == null || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player")) // 감지를 하지 못했거나 / 감지한 오브젝트중 플레이어가 아닌 오브젝트가 껴있다면
            return default;

        return hit; // 플레이어 정보 레이캐스트 반환
    }


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (facingDir * playerCheckDistance), playerCheck.position.y)); // 플레이어 기즈모 그리기 
        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (facingDir * minRetreatDistance), playerCheck.position.y)); // 공격 범위 기즈모 그리기
        Gizmos.color = Color.green;

    }
}
