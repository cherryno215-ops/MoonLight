using UnityEngine;

public class SFX_audioManager : MonoBehaviour
{

    public static SFX_audioManager instance;
    [SerializeField] private SFX_audioDatabase SFXaudioDatabase;
    [SerializeField] private AudioSource sfxSource;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(string soundName, AudioSource sfxSource)
    {
        var data = SFXaudioDatabase.Get(soundName);
        if (data == null) return;


        var clip = data.GetRandomClip();
        if (clip == null)
            return;




        sfxSource.clip = clip;
        sfxSource.PlayOneShot(clip);

    }


}
