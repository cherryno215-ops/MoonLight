using System.Collections;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Player : Entity
{
    public PlayerInputSet input { get; private set; } // 인풋
    public Player_IdleState idleState { get; private set; } // 유후 상태
    public Player_MoveState moveState { get; private set; } // 이동 상태
    public Player_JumpState jumpState { get; private set; } // 점프 상태
    public Player_FallState fallState { get; private set; } // 낙하 상태
    public Player_DashState dashState { get; private set; } // 대쉬 상태
    public Player_BasicAttackState basicAttackState { get; private set; } // 기본 공격 상태
    public Player_DeadState deadState { get; private set; } // 죽음 상태



    [Header("Attack details")]
    public Vector2 attackVelocity; // 공격 Velocity
    public float attackVelocityDuration = 0.1f; // 공격 Velocity 쿨타임
    public float comboResetTime = 1.5f; // 축적한 콤보 인덱스 초기화까지 걸리는 시간
    private Coroutine queudAttackCo; // 공격 대기 코루틴



    [Header("Movement details")]
    public float moveSpeed; // 이동 속도
    public float jumpForce = 5; // 점프 속도

    [Range(0, 1)]
    public float inAirMoveMultiplier = 0.7f; // 공중 상태에서의 속도
    [Space]
    public float dashDuration = .25f; // 대쉬 시간
    public float dashSpeed = 20; // 대쉬 속도


    public Vector2 moveInput { get; private set; } //무브 인풋 (예: 1,1   1,0   0,1 등등)

    protected override void Awake()
    {
        base.Awake();

        input = new PlayerInputSet(); // 인풋 인스턴스 할당



        idleState = new Player_IdleState(this, stateMachine, "idle"); // idle 상태 선언
        moveState = new Player_MoveState(this, stateMachine, "move"); // move 상태 선언
        jumpState = new Player_JumpState(this, stateMachine, "jumpfall"); // jumpfall 블랜트리 상태 선언 (jump)
        fallState = new Player_FallState(this, stateMachine, "jumpfall"); // jumpfall 블랜트리 상태 선언 (fall)
        dashState = new Player_DashState(this, stateMachine, "dash"); // dash 상태 선언
        basicAttackState = new Player_BasicAttackState(this, stateMachine, "basicAttack"); // 공격 상태 선언
        deadState = new Player_DeadState(this, stateMachine, "dead");
  
    }


    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);// 시작시 idle state
    }

    public override void EntityDeath()
    {
        base.EntityDeath();
        stateMachine.ChangeState(deadState);
    }


    public void EnterAttackStateWithDelay() // 어택 상태 프레임 단위 딜레이
    {
        if (queudAttackCo != null) // 코루틴이 실행되고 있다면
        {
            StopCoroutine(queudAttackCo); // 코루틴 멈춰
        }

        queudAttackCo = StartCoroutine(EnterAttackStateWithDelayCo()); // 그리고 코루틴 다시 시작
    }
    private IEnumerator EnterAttackStateWithDelayCo() // 공격 상태 딜레이 코루틴
    {
        yield return new WaitForEndOfFrame(); // 프레임이 끝날 때 까지 기다렸다가
        stateMachine.ChangeState(basicAttackState);
    }



    private void OnEnable() // gameObject.SetActive(true); 일 때 호출
    {
        input.Enable(); // 인풋 시스템 전체 활성화

        input.Player.Movement.performed += context => moveInput = context.ReadValue<Vector2>(); // 플레이어가 wasd중 하나를 누르면 moveInput 변수에 Vector2 값이 저장 (이동)
        input.Player.Movement.canceled += context => moveInput = Vector2.zero; // wasd중 입력한 버튼을 때면 0으로 복구 (유후)



    }

    private void OnDisable() 
    {
        input.Disable(); // 인풋 시스템 전체 비활성화
    }


}