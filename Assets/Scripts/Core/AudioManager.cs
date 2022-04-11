using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public enum Sounds
    {
        Positive,
        Negative,
        Lost,
        Won
    }

    [SerializeField] private AudioClip positiveClip;
    [SerializeField] private AudioClip negativeClip;
    [SerializeField] private AudioClip gameOverClip;
    [SerializeField] private AudioClip winClip;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(Sounds sound)
    {
        switch (sound)
        {
            case Sounds.Positive:
                source.PlayOneShot(positiveClip);
                break;
            case Sounds.Negative:
                source.PlayOneShot(negativeClip);
                break;
            case Sounds.Lost:
                source.PlayOneShot(gameOverClip);
                break;
            case Sounds.Won:
                source.PlayOneShot(winClip);
                break;
            default:
                break;
        }
    }
}
