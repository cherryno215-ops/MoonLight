using UnityEngine;
using UnityEngine.SceneManagement;

public class Entity_AnimationTriggers : MonoBehaviour
{
    private Entity entity;
    private Entity_Combat entityCombat;


    [SerializeField] private AudioClip clip;
    private AudioSource audioSource;

    [SerializeField] private string nextSceneName;





    protected virtual void Awake()
    {
        entity = GetComponentInParent<Entity>();
        entityCombat = GetComponentInParent<Entity_Combat>();
        audioSource = GetComponent<AudioSource>();
    }
    protected virtual void CurrentStateTrigger()
    {
        entity.CurrentStateAnimationTrigger();
    }

    protected virtual void AttackTrigger()
    {
        entityCombat.PerformAttack();
    }

    protected virtual void AudioTrigger()
    {
        audioSource.PlayOneShot(clip);
    }

    protected virtual void NextSceenTrigger()
    {
        SceneManager.LoadScene(nextSceneName);
    }


}
