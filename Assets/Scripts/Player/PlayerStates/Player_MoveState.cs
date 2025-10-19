using UnityEngine;

public class Player_MoveState : Player_GroundedState
{
    public Player_MoveState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();



        if (player.moveInput.x == 0) // moveInput x�� ���� 0�� ��, �� �������� ���� �� 
        {
            stateMachine.ChangeState(player.idleState); // ���� ���·� ��ȯ
        }

        player.SetVelocity(player.moveInput.x * player.moveSpeed, rb.linearVelocity.y); // player.SetVelocity���� new Vector�� �����ؼ� "�̵� ���� ����"
    }
}
