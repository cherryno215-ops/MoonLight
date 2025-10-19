using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Camera mainCamera;
    private float lastCameraPositionX; // ���� �����ӿ����� ī�޶� x��ǥ
 



    [SerializeField] private ParallaxLayer[] background_Layers;

    private void Awake()
    {
        mainCamera = Camera.main;
        
    }

    private void Update()
    {
        float currentCameraPositionX = mainCamera.transform.position.x; // ī�޶� ���� x��ǥ
        float distanceToMove = currentCameraPositionX - lastCameraPositionX; // ���� ��ġ���� ���� (�󸶳� ��������)
        lastCameraPositionX = currentCameraPositionX; // ������ ��ġ ����


        foreach (ParallaxLayer layer in background_Layers) // ParallaxLayer ��ũ��Ʈ���� ��������
        {
            layer.Move(distanceToMove);
        }
    }
}
