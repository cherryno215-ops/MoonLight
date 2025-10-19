using System.Collections;
using System.Xml;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Enemy_Nothing : Enemy
{
    public Enemy_NothingBattleState nothingBattleState { get; private set; }
    public Enemy_NothingRetreatState nothingRetreatState { get; private set; }
 

    [Space]
    public float retreatCooldown = 4.5f; // Enemy Nothing ����
    public float retreatMaxDistance = 8; // ȸ�� �ɷ��� �ִ� �Ÿ�
    public float retreatSpeed = 15; // ȸ�� �ɷ� �ӵ� 

    protected override void Awake()
    {
        base.Awake();

        //Enemy���� �ʿ��� ���µ鸸 �̰����� ���µ��� �� �Ҵ�
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

