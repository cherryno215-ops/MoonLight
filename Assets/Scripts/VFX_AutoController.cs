using UnityEngine;

public class VFX_AutoController : MonoBehaviour
{
    [SerializeField] private bool autoDestroy = true;
    [SerializeField] private float destroyDelay = 1;

    private void Start()
    {
        if (autoDestroy) // 1�� ������ ���� ������Ʈ �ı�
        {
            Destroy(gameObject, destroyDelay);
        }
    }
}
