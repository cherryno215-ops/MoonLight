using System;
using UnityEngine;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour
{
    private Slider healthBar;
    private Entity_VFX entityVfx;
    private Entity entity;


    [SerializeField] public float currentHealth;
    [SerializeField] public float maxHp = 100;
    [SerializeField] protected bool isDead;


    protected virtual void Awake()
    {
        entityVfx = GetComponent<Entity_VFX>();
        entity = GetComponent<Entity>();
       healthBar = GetComponentInChildren<Slider>();

        currentHealth = maxHp;
        UpdateHealthBar();
    }


    protected virtual void Start()
    {
        UpdateHealthBar(); // ���� �ʱ�ȭ �ڵ�
    }



    public virtual void TakeDamage(float damage, Transform damageDealer) // ������ ���� 
    {
        if (isDead) // �׾�����
        {
            return;
        }

        entityVfx?.PlayOnDamageVfx();
        ReduceHp(damage);
    }




    protected void ReduceHp(float damage) // HP ����
    {
        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public float GetCurrentHealth() => currentHealth;

    private void Die()
    {
        isDead = true;
        entity.EntityDeath();
    }

    


    private void UpdateHealthBar()
    {
       if (healthBar == null)
        {
            return;
        }

        
        healthBar.value = currentHealth / maxHp;
    } 
}
