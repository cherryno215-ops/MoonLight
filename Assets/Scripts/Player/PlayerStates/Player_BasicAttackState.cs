using UnityEngine;

public class Player_BasicAttackState : PlayerState
{

    private float attackVelocityTimer;
    private float lastTimeAttacked;

    private bool comboAttackQueued; // �޺� ���� ���
    private int attackDir;
    private int comboIndex = 1; // �޺� �ε���
    private int comboLimit = 3; // �ִ� �޺� ��
    private const int FirstComboIndex = 1; // ó�� �޺� �ε���



    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        comboAttackQueued = false; // �޺� ���� ��� �ʱ�ȭ
        ResetComboIndexIfNeeded();



        attackDir = player.moveInput.x != 0 ? ((int)player.moveInput.x) : player.facingDir;  // �����̰� ������ �̵� ����, �ƴϸ� �ٶ󺸴� �������� ���� ���� ����


        anim.SetInteger("basicAttackIndex", comboIndex); // �ش� ���¸� �Է��� ������ �ִϸ������� ���� ����
        ApplyAttackVelocity();
    }
    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();


        if (input.Player.Attack.WasPressedThisFrame()) // �������� �� Ȱ��ȭ
        {
            QueueNextAttack(); // ���� �ε����� ���� ����Ʈ�� ���� �ʾ��� ������ ���Է� (comboAttackQueued) true
        }

        if (triggerCalled) // Ʈ���� ȣ��
            HandleStateExit();


    }
    public override void Exit()
    {
        base.Exit();
        comboIndex++;   // ���°� ���� �� ���� �޺� Index �ø�
        lastTimeAttacked = Time.time;

    }

    private void HandleStateExit() // ���� ���� (idle ���� Ȥ�� �޺� ���� �ձ� (���Է�))
    {
        if (comboAttackQueued) // �޺� ���� ��� = true
        {
            anim.SetBool(animBoolName, false); // ���� ���� �ʱ�ȭ
            player.EnterAttackStateWithDelay(); // 1������ ��� ���� �ڷ�ƾ ����
        }
        else
        {
            stateMachine.ChangeState(player.idleState); // idle ���·� ����
        }
    }


    private void QueueNextAttack()
    {
        if (comboIndex < comboLimit)
        {
            comboAttackQueued = true;
        }
    }
    private void HandleAttackVelocity() // ���� �� Velocity ����
    {
        attackVelocityTimer -= Time.deltaTime; // �� �����̴� �ð�

        if (attackVelocityTimer < 0) // ���� ����� ���ӵǰ� ���� ��
        {
            player.SetVelocity(0, rb.linearVelocityY); // �� ������
        }

    }
    private void ApplyAttackVelocity() // ���� �ӵ� ����
    {
        attackVelocityTimer = player.attackVelocityDuration; // �ν��Ͻ�
        player.SetVelocity(player.attackVelocity.x * attackDir, player.attackVelocity.y); // �������� �� Attack Velocity�� �� ��ŭ Ƣ����� ����
    }
    private void ResetComboIndexIfNeeded() // �޺� �ε��� ����
    {
        if (Time.time > lastTimeAttacked + player.comboResetTime) // ���������� �������� comboResetTime �� ��ŭ ������ ��
        {
            comboIndex = FirstComboIndex; // �޺� �ε��� 1�� �ʱ�ȭ
        }

        if (comboIndex > comboLimit) // �޺� ����Ʈ�� �޺� �ε����� �Ѿ��� ��
        {
            comboIndex = FirstComboIndex; // �޺� �ε��� 1�� �ʱ�ȭ
        }
    }
}
