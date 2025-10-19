using UnityEngine;

public class Player_BasicAttackState : PlayerState
{

    private float attackVelocityTimer;
    private float lastTimeAttacked;

    private bool comboAttackQueued; // 콤보 공격 대기
    private int attackDir;
    private int comboIndex = 1; // 콤보 인덱스
    private int comboLimit = 3; // 최대 콤보 수
    private const int FirstComboIndex = 1; // 처음 콤보 인덱스



    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        comboAttackQueued = false; // 콤보 공격 대기 초기화
        ResetComboIndexIfNeeded();



        attackDir = player.moveInput.x != 0 ? ((int)player.moveInput.x) : player.facingDir;  // 움직이고 있으면 이동 방향, 아니면 바라보는 방향으로 공격 방향 결정


        anim.SetInteger("basicAttackIndex", comboIndex); // 해당 상태를 입력할 때마다 애니메이터의 정수 설정
        ApplyAttackVelocity();
    }
    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();


        if (input.Player.Attack.WasPressedThisFrame()) // 공격했을 때 활성화
        {
            QueueNextAttack(); // 공격 인덱스가 공격 리미트를 넘지 않았을 때에만 선입력 (comboAttackQueued) true
        }

        if (triggerCalled) // 트리거 호출
            HandleStateExit();


    }
    public override void Exit()
    {
        base.Exit();
        comboIndex++;   // 상태가 끝날 때 마다 콤보 Index 늘림
        lastTimeAttacked = Time.time;

    }

    private void HandleStateExit() // 상태 종료 (idle 상태 혹은 콤보 공격 잇기 (선입력))
    {
        if (comboAttackQueued) // 콤보 공격 대기 = true
        {
            anim.SetBool(animBoolName, false); // 현재 상태 초기화
            player.EnterAttackStateWithDelay(); // 1프레임 대기 이후 코루틴 실행
        }
        else
        {
            stateMachine.ChangeState(player.idleState); // idle 상태로 변경
        }
    }


    private void QueueNextAttack()
    {
        if (comboIndex < comboLimit)
        {
            comboAttackQueued = true;
        }
    }
    private void HandleAttackVelocity() // 공격 시 Velocity 설정
    {
        attackVelocityTimer -= Time.deltaTime; // 못 움직이는 시간

        if (attackVelocityTimer < 0) // 공격 모션이 지속되고 있을 때
        {
            player.SetVelocity(0, rb.linearVelocityY); // 못 움직임
        }

    }
    private void ApplyAttackVelocity() // 공격 속도 적용
    {
        attackVelocityTimer = player.attackVelocityDuration; // 인스턴스
        player.SetVelocity(player.attackVelocity.x * attackDir, player.attackVelocity.y); // 공격했을 때 Attack Velocity의 값 만큼 튀어나가는 로직
    }
    private void ResetComboIndexIfNeeded() // 콤보 인덱스 리셋
    {
        if (Time.time > lastTimeAttacked + player.comboResetTime) // 마지막으로 공격한지 comboResetTime 값 만큼 지났을 때
        {
            comboIndex = FirstComboIndex; // 콤보 인덱스 1로 초기화
        }

        if (comboIndex > comboLimit) // 콤보 리미트를 콤보 인덱스가 넘었을 때
        {
            comboIndex = FirstComboIndex; // 콤보 인덱스 1로 초기화
        }
    }
}
