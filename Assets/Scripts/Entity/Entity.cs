using UnityEngine;
using System;
public class Entity : MonoBehaviour
{
    public event Action OnFlipped;




    public Animator anim { get; private set; } // �ִϸ��̼�
    public Rigidbody2D rb { get; private set; } // rb 2D
    public Entity_SFX sfx { get; private set; }
    protected StateMachine stateMachine; // ���¸ӽ�


    [SerializeField] protected bool facingRight = true; // �������� ���� ��

    public int facingDir { get; private set; } = 1; // �ٶ󺸴� ����


    [Header("Collision detection")]
    [SerializeField] protected LayerMask whatIsGround; // �ٴ� ���̾� ����
    [SerializeField] private float groundCheckDistance; // �ٴ� ���� ����ĳ��Ʈ�� �Ÿ�
    [SerializeField] private float wallCheckDistance; // �� ���� �����ɽ�Ʈ�� �Ÿ�
    [SerializeField] private Transform groundCheck; // CreatEmpty�� �ٴ� ������ ������ ���� ����
    public bool groundDetected { get; private set; } // �ٴ� ������





    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>(); // �ڽ� ��ü�� ������Ʈ Animator �������� (�ָ� ���� �����;����� stateMachine�� ������ ���� �ʰ� ���������� �۵���)
        rb = GetComponent<Rigidbody2D>(); // ������ٵ�2D �ν��Ͻ�
        sfx = GetComponent<Entity_SFX>();


        stateMachine = new StateMachine(); // ���¸ӽ� ��ü �ϳ� �غ�


    }

    protected virtual void Start()
    {

    }



    protected virtual void Update()
    {
        
        handleCollisionDetection(); // �ٴ� ����
        stateMachine.UpdateActiveState(); // ���� ���� �ݺ�
    }


    public void CurrentStateAnimationTrigger() // �ִϸ��̼� Ʈ���� ȣ��
    {
        stateMachine.currentState.AnimationTrigger();
    }

    public virtual void EntityDeath() // ���� ���� ���·� ��� ��Ű��
    {

    }



    public void SetVelocity(float xVelocity, float yVelocity) // ��ƼƼ Velocity ���� �޼���
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }
    public void HandleFlip(float xVelocity) // �� ������ ���� �޼���
    {
        if (xVelocity > 0 && facingRight == false) // ���������� ������ �� && ������ ���� ���� ��
        {
            Flip();
        }
        else if (xVelocity < 0 && facingRight == true) // �������� ������ �� && �������� ���� ���� ��
        {
            Flip();
        }
    }

    public void Flip() // �� ������ ���� (ȣ���) �޼���
    {
        transform.Rotate(0, 180, 0);  // 180���� �� ������
        facingRight = !facingRight; // facingRight ���� false���� true���� �ش� �޼��尡 ȣ�� �� ������ �ٲ�
        facingDir = facingDir * -1;

        OnFlipped?.Invoke(); //�ø� �̺�Ʈ�� ����ص� �Լ��� ���� �ҷ�����
    }

    private void handleCollisionDetection() // �浹 ���� �� ó��
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround); // �ٴ� ���� �� �ٴ��̶�� groundDetected = true, �ٴ� ���� ���̶�� groundDetected = false 

    }
    protected virtual void OnDrawGizmos() // ����� �׸���
    {
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + new Vector3(0, -groundCheckDistance, 0)); // �� ���� ����ĳ��Ʈ ǥ�ÿ� �����
        
    }
}
