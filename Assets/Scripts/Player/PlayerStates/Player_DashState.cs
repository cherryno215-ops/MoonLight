using JetBrains.Annotations;
using UnityEngine;

public class Player_DashState : PlayerState
{

    private float origianlGravityScale;
    private float dashDir; // facingDir ��� ���� ���� ������ġ
    public Player_DashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        dashDir = player.moveInput.x != 0 ? ((int)player.moveInput.x) : player.facingDir; // �����̰� ������ �̵� ����, �ƴϸ� �ٶ󺸴� �������� �뽬 ���� ����



        stateTimer = player.dashDuration;

        origianlGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
    }


    public override void Update()
    {
        base.Update();
        

        player.SetVelocity(player.dashSpeed * dashDir, 0); // Velocity ����

        if (stateTimer < 0) // Ÿ�̸Ӱ� 0�� ���� ��
        {
            if (player.groundDetected) // ���� ����� ��
            {
                stateMachine.ChangeState(player.idleState); // ���� ���·� ����
            }
            else // �� ��
            {
                stateMachine.ChangeState(player.fallState); // ���� ���·� ����
            }
        }
    }

    public override void Exit() // ĳ���� ƨ�ܳ��� ������ �޼���
    {
        base.Exit(); // ���°� ��ȯ���� ��

        player.SetVelocity(0, 0); // velocity�� 0, 0���� �ٲ� (�뽬 �� �������� ƨ�ܳ����� ����)
        rb.gravityScale = origianlGravityScale; 
    }
}
