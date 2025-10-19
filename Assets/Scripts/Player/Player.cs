using System.Collections;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Player : Entity
{
    public PlayerInputSet input { get; private set; } // ��ǲ
    public Player_IdleState idleState { get; private set; } // ���� ����
    public Player_MoveState moveState { get; private set; } // �̵� ����
    public Player_JumpState jumpState { get; private set; } // ���� ����
    public Player_FallState fallState { get; private set; } // ���� ����
    public Player_DashState dashState { get; private set; } // �뽬 ����
    public Player_BasicAttackState basicAttackState { get; private set; } // �⺻ ���� ����
    public Player_DeadState deadState { get; private set; } // ���� ����



    [Header("Attack details")]
    public Vector2 attackVelocity; // ���� Velocity
    public float attackVelocityDuration = 0.1f; // ���� Velocity ��Ÿ��
    public float comboResetTime = 1.5f; // ������ �޺� �ε��� �ʱ�ȭ���� �ɸ��� �ð�
    private Coroutine queudAttackCo; // ���� ��� �ڷ�ƾ



    [Header("Movement details")]
    public float moveSpeed; // �̵� �ӵ�
    public float jumpForce = 5; // ���� �ӵ�

    [Range(0, 1)]
    public float inAirMoveMultiplier = 0.7f; // ���� ���¿����� �ӵ�
    [Space]
    public float dashDuration = .25f; // �뽬 �ð�
    public float dashSpeed = 20; // �뽬 �ӵ�


    public Vector2 moveInput { get; private set; } //���� ��ǲ (��: 1,1   1,0   0,1 ���)

    protected override void Awake()
    {
        base.Awake();

        input = new PlayerInputSet(); // ��ǲ �ν��Ͻ� �Ҵ�



        idleState = new Player_IdleState(this, stateMachine, "idle"); // idle ���� ����
        moveState = new Player_MoveState(this, stateMachine, "move"); // move ���� ����
        jumpState = new Player_JumpState(this, stateMachine, "jumpfall"); // jumpfall ��Ʈ�� ���� ���� (jump)
        fallState = new Player_FallState(this, stateMachine, "jumpfall"); // jumpfall ��Ʈ�� ���� ���� (fall)
        dashState = new Player_DashState(this, stateMachine, "dash"); // dash ���� ����
        basicAttackState = new Player_BasicAttackState(this, stateMachine, "basicAttack"); // ���� ���� ����
        deadState = new Player_DeadState(this, stateMachine, "dead");
  
    }


    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);// ���۽� idle state
    }

    public override void EntityDeath()
    {
        base.EntityDeath();
        stateMachine.ChangeState(deadState);
    }


    public void EnterAttackStateWithDelay() // ���� ���� ������ ���� ������
    {
        if (queudAttackCo != null) // �ڷ�ƾ�� ����ǰ� �ִٸ�
        {
            StopCoroutine(queudAttackCo); // �ڷ�ƾ ����
        }

        queudAttackCo = StartCoroutine(EnterAttackStateWithDelayCo()); // �׸��� �ڷ�ƾ �ٽ� ����
    }
    private IEnumerator EnterAttackStateWithDelayCo() // ���� ���� ������ �ڷ�ƾ
    {
        yield return new WaitForEndOfFrame(); // �������� ���� �� ���� ��ٷȴٰ�
        stateMachine.ChangeState(basicAttackState);
    }



    private void OnEnable() // gameObject.SetActive(true); �� �� ȣ��
    {
        input.Enable(); // ��ǲ �ý��� ��ü Ȱ��ȭ

        input.Player.Movement.performed += context => moveInput = context.ReadValue<Vector2>(); // �÷��̾ wasd�� �ϳ��� ������ moveInput ������ Vector2 ���� ���� (�̵�)
        input.Player.Movement.canceled += context => moveInput = Vector2.zero; // wasd�� �Է��� ��ư�� ���� 0���� ���� (����)



    }

    private void OnDisable() 
    {
        input.Disable(); // ��ǲ �ý��� ��ü ��Ȱ��ȭ
    }


}