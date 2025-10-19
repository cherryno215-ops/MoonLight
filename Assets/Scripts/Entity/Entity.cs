using UnityEngine;
using System;
public class Entity : MonoBehaviour
{
    public event Action OnFlipped;




    public Animator anim { get; private set; } // 애니메이션
    public Rigidbody2D rb { get; private set; } // rb 2D
    public Entity_SFX sfx { get; private set; }
    protected StateMachine stateMachine; // 상태머신


    [SerializeField] protected bool facingRight = true; // 오른쪽을 보는 얼굴

    public int facingDir { get; private set; } = 1; // 바라보는 방향


    [Header("Collision detection")]
    [SerializeField] protected LayerMask whatIsGround; // 바닥 레이어 감지
    [SerializeField] private float groundCheckDistance; // 바닥 감지 레이캐스트의 거리
    [SerializeField] private float wallCheckDistance; // 벽 감지 레이케스트의 거리
    [SerializeField] private Transform groundCheck; // CreatEmpty로 바닥 감지의 기준점 설정 가능
    public bool groundDetected { get; private set; } // 바닥 감지됨





    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>(); // 자식 객체의 컴포넌트 Animator 가져오기 (애를 먼저 가져와야지만 stateMachine이 오류가 나지 않고 정상적으로 작동함)
        rb = GetComponent<Rigidbody2D>(); // 리지드바디2D 인스턴스
        sfx = GetComponent<Entity_SFX>();


        stateMachine = new StateMachine(); // 상태머신 본체 하나 준비


    }

    protected virtual void Start()
    {

    }



    protected virtual void Update()
    {
        
        handleCollisionDetection(); // 바닥 감지
        stateMachine.UpdateActiveState(); // 현재 상태 반복
    }


    public void CurrentStateAnimationTrigger() // 애니메이션 트리거 호출
    {
        stateMachine.currentState.AnimationTrigger();
    }

    public virtual void EntityDeath() // 순수 백지 상태로 상속 시키기
    {

    }



    public void SetVelocity(float xVelocity, float yVelocity) // 엔티티 Velocity 제어 메서드
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }
    public void HandleFlip(float xVelocity) // 몸 뒤집기 메인 메서드
    {
        if (xVelocity > 0 && facingRight == false) // 오른쪽으로 움직일 때 && 왼쪽을 보고 있을 때
        {
            Flip();
        }
        else if (xVelocity < 0 && facingRight == true) // 왼쪽으로 움직일 때 && 오른족을 보고 있을 때
        {
            Flip();
        }
    }

    public void Flip() // 몸 뒤집기 서브 (호출용) 메서드
    {
        transform.Rotate(0, 180, 0);  // 180도로 몸 뒤집기
        facingRight = !facingRight; // facingRight 값을 false였다 true였다 해당 메서드가 호출 될 때마다 바꿈
        facingDir = facingDir * -1;

        OnFlipped?.Invoke(); //플립 이벤트에 등록해둔 함수들 전부 불러오기
    }

    private void handleCollisionDetection() // 충돌 감지 및 처리
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround); // 바닥 감지 후 바닥이라면 groundDetected = true, 바닥 외의 것이라면 groundDetected = false 

    }
    protected virtual void OnDrawGizmos() // 기즈모 그리기
    {
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + new Vector3(0, -groundCheckDistance, 0)); // 땅 감지 레이캐스트 표시용 기즈모
        
    }
}
