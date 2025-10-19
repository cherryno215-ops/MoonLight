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

    public void PerformAttack() // 공격 수행
    {

        foreach (var target in GetDetectedcolliders())
        {
            Entity_Health targetHealth = target.GetComponent<Entity_Health>(); // 타겟 hp 참조

            targetHealth?.TakeDamage(damage, transform); // 데미지 입히기

            sfx?.AttackHit();
            vfx.CreateOnHitVFX(target.transform);


        }


    }


    private Collider2D[] GetDetectedcolliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget); // 구형태의 물리 타겟 검사
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }
}
