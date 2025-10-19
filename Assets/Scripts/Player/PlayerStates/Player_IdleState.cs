using UnityEngine;


public class Player_IdleState : Player_GroundedState
{
    public Player_IdleState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {

    }

    public override void Enter()
    {
        
        base.Enter();

        player.SetVelocity(0, rb.linearVelocity.y); // 안 미끄러지게 해줌
    }

    public override void Update()
    {
        base.Update();

        
      

        if (player.moveInput.x != 0) // moveInput x의 값이 0이 아닐 때 즉 움직이고 있을 때
        {
            stateMachine.ChangeState(player.moveState); // 무브 상태로 상태 전환
        }

    }
}
