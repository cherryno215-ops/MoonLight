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
        entity.OnFlipped += HandleFlip; // OnFlipped를 구독해 Flip 메서드가 호출 될 때마다 같이 호출
    }


    private void OnDisable()
    {
        entity.OnFlipped -= HandleFlip; // OnFlipped 구독을 취소해서 신호를 받지 않음
    }

    private void HandleFlip()
    {
        transform.rotation = Quaternion.identity; // 로테이션 0,0,0 고정
    }
}
