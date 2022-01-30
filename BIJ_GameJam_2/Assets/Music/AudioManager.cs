using Assets.GameProgression;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource bowSound;
    [SerializeField] private AudioSource hurtSound;

    void Start()
    {
        GameManager.Instance.OnGameEvent += Instance_OnGameEvent;
    }

    private void Instance_OnGameEvent(GameEvents gameEvent)
    {
        switch (gameEvent)
        {
            case GameEvents.LoverHitByArrow:
                PlayAudioSource(hurtSound);
                break;
            case GameEvents.ArrowFired:
                PlayAudioSource(bowSound);
                break;
            default:
                break;
        }
    }

    private void PlayAudioSource(AudioSource audioSource)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.time = 0;
        }
        audioSource.Play();
    }
}
