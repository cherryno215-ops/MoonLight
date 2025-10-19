using UnityEngine;

public class Player_FallState : Player_AiredState
{
    public Player_FallState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }


    public override void Update()
    {
        base.Update();

        if (player.groundDetected) // groundDetected�� true���
        {
            stateMachine.ChangeState(player.idleState); // ���� ���·� ��ȯ
        }
    }
}
