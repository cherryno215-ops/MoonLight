using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyState : EntityState
{
    protected Enemy enemy; // Enemy ������Ʈ�κ��� ��ӹ���
    public EnemyState(Enemy enemy, StateMachine stateMachien, string animBoolName) : base(stateMachien, animBoolName)
    {
        this.enemy = enemy;

        rb = enemy.rb; // Entity�κ��� ��ӹ��� rb�� Enemy ������Ʈ���� ������
        anim = enemy.anim; // Entity�κ��� ��ӹ��� anim�� Enemy ������Ʈ���� ������
    }



    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();
        anim.SetFloat("xVelocity", rb.linearVelocity.x); // xVelocity���� x Ʈ������ ������ �������̰� ������ �ִϸ��̼� ���� ����ȭ
    }
}



