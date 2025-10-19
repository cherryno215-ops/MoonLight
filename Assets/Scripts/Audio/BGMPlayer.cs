using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip bgmClip; // 할당할 BGM

    private void Awake()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgmClip;
        audioSource.loop = true; // 루프
        audioSource.Play();
    }
}