using UnityEngine;

public class Player_GroundedState : PlayerState // ���� ����
{
    public Player_GroundedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocity.y < 0) // �������� ���� ��
        {
            stateMachine.ChangeState(player.fallState); // fall ���·� ����
        }

        if (input.Player.Jump.WasPerformedThisFrame())  // ���� �� ��
        {
            stateMachine.ChangeState(player.jumpState); // ���� ���·� ����
        }

        if (input.Player.Attack.WasPerformedThisFrame()) // ���� ���� ��
        {
            stateMachine.ChangeState(player.basicAttackState); // ���� ���·� ����
        }
    }
}
