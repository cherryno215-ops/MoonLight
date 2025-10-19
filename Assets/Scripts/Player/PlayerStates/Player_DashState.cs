using JetBrains.Annotations;
using UnityEngine;

public class Player_DashState : PlayerState
{

    private float origianlGravityScale;
    private float dashDir; // facingDir 대용 버그 방지 안전장치
    public Player_DashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        dashDir = player.moveInput.x != 0 ? ((int)player.moveInput.x) : player.facingDir; // 움직이고 있으면 이동 방향, 아니면 바라보는 방향으로 대쉬 방향 결정



        stateTimer = player.dashDuration;

        origianlGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
    }


    public override void Update()
    {
        base.Update();
        

        player.SetVelocity(player.dashSpeed * dashDir, 0); // Velocity 설정

        if (stateTimer < 0) // 타이머가 0이 됬을 때
        {
            if (player.groundDetected) // 땅에 닿았을 때
            {
                stateMachine.ChangeState(player.idleState); // 유후 상태로 변경
            }
            else // 그 외
            {
                stateMachine.ChangeState(player.fallState); // 낙하 상태로 변경
            }
        }
    }

    public override void Exit() // 캐릭터 튕겨나감 방지용 메서드
    {
        base.Exit(); // 상태가 전환됬을 때

        player.SetVelocity(0, 0); // velocity를 0, 0으로 바꿈 (대쉬 후 관성으로 튕겨나가기 방지)
        rb.gravityScale = origianlGravityScale; 
    }
}
