using UnityEngine;
using UnityEngine.Rendering.Universal;

public abstract class PlayerState : EntityState
{
    protected Player player; // 플레이어
    protected PlayerInputSet input;




    public PlayerState(Player player, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName) // 자식 스크립트에 상속시켜줄 기본 베이스 EntityState
    {
        this.player = player;

        anim = player.anim; // 플레이어 스크립트의 애니메이션 변수의 인스턴스
        rb = player.rb; // 플레이어 스크립트의 리지드바디 변수의 인스턴스
        input = player.input; // 플레이어 스크립트의 input 변수
    }

    public override void Update()
    {
        base.Update();


        if (input.Player.Dash.WasPressedThisFrame()) // 대쉬를 눌렀을 때
        {
            stateMachine.ChangeState(player.dashState); // # 어떠한 상태든 간에 무조건 씹고 대쉬 상태로 돌입 #
        }
    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();
        anim.SetFloat("yVelocity", rb.linearVelocity.y); // 캐릭터의 y 값이 바뀔 때 현재 수직 속도 정보를 Animator에 넘겨 애니메이션 상태 전환에 반영하는 코드
    }




}
