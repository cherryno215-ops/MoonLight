using UnityEngine;


public class Enemy_Health : Entity_Health
{
    private Enemy enemy => GetComponent<Enemy>();


    public override void TakeDamage(float damage, Transform damageDealer)
    {
        if (damageDealer.GetComponent<Player>() != null) // 자신을 공격한 대상이 플레이어라면
        {
            enemy.TryEnterBattleState(damageDealer);
        }

        base.TakeDamage(damage, damageDealer);
    }
}
