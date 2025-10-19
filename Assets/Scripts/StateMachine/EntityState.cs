using UnityEngine;
using UnityEngine.Rendering.Universal;

public abstract class EntityState /* ��ƼƼ ������Ʈ�� ��� ������Ʈ�� �ʿ���
     ������ ��� �ִ� ���ø��� ���̸�, �̸� �߻�ȭ�����ν� �Ǽ��� �ܵ����� ����ع����� �Ǽ���
     ������ �ʵ��� �ϱ� ���� ���� ����. */
{
    public Entity_SFX sfx;

    protected StateMachine stateMachine; // ���� �ӽ�
    protected string animBoolName; // �ִϸ��̼� �̸�

    protected Animator anim; // �ִϸ��̼� (�÷��̾� ��ũ��Ʈ�� �ִϸ��̼� ������ �ν��Ͻ�)
    protected Rigidbody2D rb;

    protected float stateTimer; // Ÿ�̸� (��Ÿ��)
    protected bool triggerCalled; // Ʈ���� ȣ��

    public EntityState(StateMachine stateMachien, string animBoolName)
    {
        this.stateMachine = stateMachien;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter() // ���� ����
    {
        anim.SetBool(animBoolName, true);
        triggerCalled = false; // Ʈ���� �缳��
    }

    public virtual void Update() // ������Ʈ
    {
        stateTimer -= Time.deltaTime;
        UpdateAnimationParameters();

    }

    public virtual void Exit() // ���� ������
    {
        anim.SetBool(animBoolName, false);
    }

    public void AnimationTrigger()
    {
        triggerCalled = true;
    }

    public virtual void UpdateAnimationParameters() // �������� ����� ��
    {

    }
}
