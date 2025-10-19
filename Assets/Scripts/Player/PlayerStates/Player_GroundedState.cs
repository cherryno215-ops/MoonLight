using UnityEngine;

public class Player_GroundedState : PlayerState // 슈퍼 상태
{
    public Player_GroundedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocity.y < 0) // 떨어지고 있을 때
        {
            stateMachine.ChangeState(player.fallState); // fall 상태로 번경
        }

        if (input.Player.Jump.WasPerformedThisFrame())  // 점프 뛸 때
        {
            stateMachine.ChangeState(player.jumpState); // 점프 상태로 변경
        }

        if (input.Player.Attack.WasPerformedThisFrame()) // 공격 했을 때
        {
            stateMachine.ChangeState(player.basicAttackState); // 공격 상태로 변경
        }
    }
}
