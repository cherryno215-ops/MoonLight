using UnityEngine;
using UnityEngine.Rendering.Universal;

public abstract class EntityState /* 엔티티 스테이트는 모든 스테이트에 필요한
     정보를 담고 있는 템플릿일 뿐이며, 이를 추상화함으로써 실수로 단독으로 사용해버리는 실수를
     범하지 않도록 하기 위한 것일 뿐임. */
{
    public Entity_SFX sfx;

    protected StateMachine stateMachine; // 상태 머신
    protected string animBoolName; // 애니메이션 이름

    protected Animator anim; // 애니메이션 (플레이어 스크립트의 애니메이션 변수의 인스턴스)
    protected Rigidbody2D rb;

    protected float stateTimer; // 타이머 (쿨타임)
    protected bool triggerCalled; // 트리거 호출

    public EntityState(StateMachine stateMachien, string animBoolName)
    {
        this.stateMachine = stateMachien;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter() // 상태 시작
    {
        anim.SetBool(animBoolName, true);
        triggerCalled = false; // 트리거 재설정
    }

    public virtual void Update() // 업데이트
    {
        stateTimer -= Time.deltaTime;
        UpdateAnimationParameters();

    }

    public virtual void Exit() // 상태 나가기
    {
        anim.SetBool(animBoolName, false);
    }

    public void AnimationTrigger()
    {
        triggerCalled = true;
    }

    public virtual void UpdateAnimationParameters() // 재정의의 희생자 ㅠ
    {

    }
}
