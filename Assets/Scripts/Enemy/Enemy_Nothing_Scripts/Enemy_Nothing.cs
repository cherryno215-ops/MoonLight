using System.Collections;
using System.Xml;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Enemy_Nothing : Enemy
{
    public Enemy_NothingBattleState nothingBattleState { get; private set; }
    public Enemy_NothingRetreatState nothingRetreatState { get; private set; }
 

    [Space]
    public float retreatCooldown = 4.5f; // Enemy Nothing 전용
    public float retreatMaxDistance = 8; // 회피 능력의 최대 거리
    public float retreatSpeed = 15; // 회피 능력 속도 

    protected override void Awake()
    {
        base.Awake();

        //Enemy에서 필요한 상태들만 이곳에서 상태들의 값 할당
        idleState = new Enemy_IdleState(this, stateMachine, "idle");
        moveState = new Enemy_MoveState(this, stateMachine, "move");
        attackState = new Enemy_AttackState(this, stateMachine, "attack");
        deadState = new Enemy_DeadState(this, stateMachine, "dead");


    
        nothingRetreatState = new Enemy_NothingRetreatState(this, stateMachine, "battle");
        nothingBattleState = new Enemy_NothingBattleState(this, stateMachine, "battle");
        battleState = nothingBattleState;



    }


    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }


 }

