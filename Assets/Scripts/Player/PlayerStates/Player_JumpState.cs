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


        player.SetVelocity(rb.linearVelocity.x, player.jumpForce); // ������ �� Ƣ�����
        player.SetVelocity(player.moveInput.x * player.moveSpeed, rb.linearVelocity.y); // ���� ���߿����� ������ �� ����
    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocity.y < 0) // �����ϰ� ���� ��
        {
            stateMachine.ChangeState(player.fallState); // fall ���·� ����
        }
    }
}

