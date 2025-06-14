using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Clips")]
    public AudioClip diamondCollectedClip;
    public AudioClip playerWinClip;
    public AudioClip playerLoseClip;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDiamondSound()
    {
        PlaySound(diamondCollectedClip);
    }

    public void PlayWinSound()
    {
        PlaySound(playerWinClip);
    }

    public void PlayLoseSound()
    {
        PlaySound(playerLoseClip);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
