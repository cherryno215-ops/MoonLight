using UnityEngine;

public class Player_MoveState : Player_GroundedState
{
    public Player_MoveState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();



        if (player.moveInput.x == 0) // moveInput x의 값이 0일 때, 즉 움직이지 않을 때 
        {
            stateMachine.ChangeState(player.idleState); // 유후 상태로 전환
        }

        player.SetVelocity(player.moveInput.x * player.moveSpeed, rb.linearVelocity.y); // player.SetVelocity에서 new Vector를 참조해서 "이동 로직 구현"
    }
}
