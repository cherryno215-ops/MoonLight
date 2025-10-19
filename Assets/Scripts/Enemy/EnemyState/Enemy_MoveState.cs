using UnityEngine;

public class Enemy_MoveState : Enemy_GroundedState
{
    public Enemy_MoveState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();


        if (enemy.groundDetected == false)
            enemy.Flip();
    }


    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.linearVelocityY); // �ٶ󺸴� �������� �����̱�

        if (enemy.groundDetected == false) // ���� �� ���� ���� ���ٸ�?
        {
            stateMachine.ChangeState(enemy.idleState); // ���� ���·� ��ȯ
            
        }
            
    }
}
