using JetBrains.Annotations;
using UnityEngine;

public class Player_JumpState : Player_AiredState
{
    public Player_JumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();


        player.SetVelocity(rb.linearVelocity.x, player.jumpForce); // 점프할 때 튀어오름
        player.SetVelocity(player.moveInput.x * player.moveSpeed, rb.linearVelocity.y); // 점프 도중에서도 움직일 수 있음
    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocity.y < 0) // 낙하하고 있을 때
        {
            stateMachine.ChangeState(player.fallState); // fall 상태로 변경
        }
    }
}

