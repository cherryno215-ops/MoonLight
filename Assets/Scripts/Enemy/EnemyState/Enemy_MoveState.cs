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

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.linearVelocityY); // 바라보는 방향으로 움직이기

        if (enemy.groundDetected == false) // 이후 갈 곳에 땅이 없다면?
        {
            stateMachine.ChangeState(enemy.idleState); // 유후 상태로 전환
            
        }
            
    }
}
