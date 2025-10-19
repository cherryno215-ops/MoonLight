using UnityEngine;
using UnityEngine.Rendering.Universal;

public abstract class PlayerState : EntityState
{
    protected Player player; // �÷��̾�
    protected PlayerInputSet input;




    public PlayerState(Player player, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName) // �ڽ� ��ũ��Ʈ�� ��ӽ����� �⺻ ���̽� EntityState
    {
        this.player = player;

        anim = player.anim; // �÷��̾� ��ũ��Ʈ�� �ִϸ��̼� ������ �ν��Ͻ�
        rb = player.rb; // �÷��̾� ��ũ��Ʈ�� ������ٵ� ������ �ν��Ͻ�
        input = player.input; // �÷��̾� ��ũ��Ʈ�� input ����
    }

    public override void Update()
    {
        base.Update();


        if (input.Player.Dash.WasPressedThisFrame()) // �뽬�� ������ ��
        {
            stateMachine.ChangeState(player.dashState); // # ��� ���µ� ���� ������ �ð� �뽬 ���·� ���� #
        }
    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();
        anim.SetFloat("yVelocity", rb.linearVelocity.y); // ĳ������ y ���� �ٲ� �� ���� ���� �ӵ� ������ Animator�� �Ѱ� �ִϸ��̼� ���� ��ȯ�� �ݿ��ϴ� �ڵ�
    }




}
