using UnityEngine;

public class UI_MiniHealthBar : MonoBehaviour
{
    private Entity entity;


    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    private void OnEnable()
    {
        entity.OnFlipped += HandleFlip; // OnFlipped�� ������ Flip �޼��尡 ȣ�� �� ������ ���� ȣ��
    }


    private void OnDisable()
    {
        entity.OnFlipped -= HandleFlip; // OnFlipped ������ ����ؼ� ��ȣ�� ���� ����
    }

    private void HandleFlip()
    {
        transform.rotation = Quaternion.identity; // �����̼� 0,0,0 ����
    }
}
