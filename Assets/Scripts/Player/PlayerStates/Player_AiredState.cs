using UnityEngine;

public class Player_AiredState : PlayerState
{
    public Player_AiredState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }


    public override void Update()
    {
        base.Update();


        if (player.moveInput.x != 0) // 움직일 때
        {
            player.SetVelocity(player.moveInput.x * (player.moveSpeed * player.inAirMoveMultiplier), rb.linearVelocity.y); // 공중에서도 움직일 수 있음 (inAirMoveMultiplier = 만큼 속도 감소 혹은 증가)
        }
    }
}
