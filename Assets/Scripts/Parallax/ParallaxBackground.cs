using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Camera mainCamera;
    private float lastCameraPositionX; // 지난 프레임에서의 카메라 x좌표
 



    [SerializeField] private ParallaxLayer[] background_Layers;

    private void Awake()
    {
        mainCamera = Camera.main;
        
    }

    private void Update()
    {
        float currentCameraPositionX = mainCamera.transform.position.x; // 카메라 현재 x좌표
        float distanceToMove = currentCameraPositionX - lastCameraPositionX; // 직전 위치와의 차이 (얼마나 움직였나)
        lastCameraPositionX = currentCameraPositionX; // 마지막 위치 갱신


        foreach (ParallaxLayer layer in background_Layers) // ParallaxLayer 스크립트에서 가져오기
        {
            layer.Move(distanceToMove);
        }
    }
}
