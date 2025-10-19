using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    [SerializeField] private Entity_VFX vfx;
    public float damage = 10;
    private Entity_SFX sfx;

    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask whatIsTarget;

    private void Awake()
    {
        sfx = GetComponent<Entity_SFX>();
    }

    public void PerformAttack() // ���� ����
    {

        foreach (var target in GetDetectedcolliders())
        {
            Entity_Health targetHealth = target.GetComponent<Entity_Health>(); // Ÿ�� hp ����

            targetHealth?.TakeDamage(damage, transform); // ������ ������

            sfx?.AttackHit();
            vfx.CreateOnHitVFX(target.transform);


        }


    }


    private Collider2D[] GetDetectedcolliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget); // �������� ���� Ÿ�� �˻�
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }
}
