
using UnityEngine;

public class Entity_SFX : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("SFX Names")]
    [SerializeField] private string attackHit;



    private void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();

    }
    public void AttackHit()
    {
        SFX_audioManager.instance.PlaySFX(attackHit, audioSource);
    }
}
