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
    public float battleTimeDuration = 5; // �ִ� ���� �ð�
    public float minRetreatDistance = 2; // ���� ������ ��Ÿ�
    public Vector2 retreatVelocity; // ���� �̵�

    [Header("Movement datails")]
    public float idleTime = 1.5f;
    public float moveSpeed = 1.5f;

    [Header("Player detection")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance = 10; //�÷��̾� �����Ÿ�
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
        if (stateMachine.currentState == battleState) //��Ʋ ���� �� ��
        {
            return;
        }
        if (stateMachine.currentState == attackState) // ���� ������ ��
        {
            return;
        }
        this.player = player;
        stateMachine.ChangeState(battleState);
    }

    public Transform GetPlayerRefernce() // �÷��̾ null �� �� �÷��̾� ���� �ޱ�
    {
        if (player == null)
        {
            player = PlayerDetected().transform;
        }
        return player;
    }

    public RaycastHit2D PlayerDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, playerCheckDistance, whatIsPlayer | whatIsGround); // �÷��̾� ���� (�÷��̾��� ��� ������ ���� ������, true / false ����)

        if (hit.collider == null || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player")) // ������ ���� ���߰ų� / ������ ������Ʈ�� �÷��̾ �ƴ� ������Ʈ�� ���ִٸ�
            return default;

        return hit; // �÷��̾� ���� ����ĳ��Ʈ ��ȯ
    }


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (facingDir * playerCheckDistance), playerCheck.position.y)); // �÷��̾� ����� �׸��� 
        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (facingDir * minRetreatDistance), playerCheck.position.y)); // ���� ���� ����� �׸���
        Gizmos.color = Color.green;

    }
}
