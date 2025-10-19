using UnityEngine;

public class Enemy_IdleState : Enemy_GroundedState
{
    public Enemy_IdleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();


        stateTimer = enemy.idleTime;
    }

    public override void Update()
    {
        base.Update(); // 지금 참조한 EntityState의 Update에는 stateTimer를 초당으로 깎아내는 로직이 존재함

        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.moveState);

    }


}
