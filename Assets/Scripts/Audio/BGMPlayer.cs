using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip bgmClip; // �Ҵ��� BGM

    private void Awake()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgmClip;
        audioSource.loop = true; // ����
        audioSource.Play();
    }
}