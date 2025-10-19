using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : Entity_Health
{
    [SerializeField] private Slider hudHealthBar;


    protected override void Start()
    {
        base.Start();

        UpdateHudHealthBar();

        
    }

    public override void TakeDamage(float damage, Transform damageDealer)
    {
        base.TakeDamage(damage, damageDealer);
        UpdateHudHealthBar();
    }


    private void UpdateHudHealthBar()
    {
        if (hudHealthBar == null)
        {
            return;
        }
        
        hudHealthBar.value = currentHealth / maxHp;
    }
}
