using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyState : EntityState
{
    protected Enemy enemy; // Enemy 컴포넌트로부터 상속받음
    public EnemyState(Enemy enemy, StateMachine stateMachien, string animBoolName) : base(stateMachien, animBoolName)
    {
        this.enemy = enemy;

        rb = enemy.rb; // Entity로부터 상속받은 rb를 Enemy 컴포넌트에서 가져옴
        anim = enemy.anim; // Entity로부터 상속받은 anim를 Enemy 컴포넌트에서 가져옴
    }



    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();
        anim.SetFloat("xVelocity", rb.linearVelocity.x); // xVelocity값을 x 트랜스폼 값으로 정상적이게 입혀서 애니메이션 버그 정상화
    }
}



