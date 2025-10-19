using UnityEngine;

public class VFX_AutoController : MonoBehaviour
{
    [SerializeField] private bool autoDestroy = true;
    [SerializeField] private float destroyDelay = 1;

    private void Start()
    {
        if (autoDestroy) // 1초 지나면 게임 오브젝트 파괴
        {
            Destroy(gameObject, destroyDelay);
        }
    }
}
