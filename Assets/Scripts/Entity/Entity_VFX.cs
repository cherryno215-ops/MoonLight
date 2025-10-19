using System.Collections;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{
    private SpriteRenderer sr;
    public Vector3 vfxSpawnPos;


    [Header("On Damage VFX")]
    [SerializeField] private Material onDamageMaterial; // �ջ�� ���׸���
    [SerializeField] private float onDamageVfxDuration = 0.2f;
    private Material originalMaterial;
    private Coroutine onDamageVfxCoroutine;

    [Header("On Doing Damage VFX")]
    [SerializeField] private GameObject hitVfx;




    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;

    }

    public void CreateOnHitVFX(Transform target)
    {
        vfxSpawnPos = target.position + new Vector3(-2f, 0f, 0f); // target.position���� �������� 0.3��ŭ �̵�
        Instantiate(hitVfx, vfxSpawnPos, Quaternion.identity);
    }

    public void PlayOnDamageVfx() // �ջ�� VFX ����
    {
        if (onDamageVfxCoroutine != null)
            StopCoroutine(onDamageVfxCoroutine); 
        

        StartCoroutine(OnDamageVfxCo());
    }

    private IEnumerator OnDamageVfxCo() // �������� �Ծ��� �� �ٲ�� ���׸���(�� ����) ��ٸ���
    {
        sr.material = onDamageMaterial;

        yield return new WaitForSeconds(onDamageVfxDuration);
        sr.material = originalMaterial;
    }


}
