using System.Collections;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{
    private SpriteRenderer sr;
    public Vector3 vfxSpawnPos;


    [Header("On Damage VFX")]
    [SerializeField] private Material onDamageMaterial; // 손상시 마테리얼
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
        vfxSpawnPos = target.position + new Vector3(-2f, 0f, 0f); // target.position에서 왼쪽으로 0.3만큼 이동
        Instantiate(hitVfx, vfxSpawnPos, Quaternion.identity);
    }

    public void PlayOnDamageVfx() // 손상시 VFX 동작
    {
        if (onDamageVfxCoroutine != null)
            StopCoroutine(onDamageVfxCoroutine); 
        

        StartCoroutine(OnDamageVfxCo());
    }

    private IEnumerator OnDamageVfxCo() // 데미지를 입었을 때 바뀌는 마테리얼(몸 색상) 기다리기
    {
        sr.material = onDamageMaterial;

        yield return new WaitForSeconds(onDamageVfxDuration);
        sr.material = originalMaterial;
    }


}
