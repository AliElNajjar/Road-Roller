using UnityEngine;
using static AudioManager;

[RequireComponent(typeof(Collider))]
public class Collectible : MonoBehaviour
{
    [SerializeField] private int scoreWorth;

    private Sounds sound;

    private void Start()
    {
        sound = scoreWorth > 0 ? Sounds.Positive : Sounds.Negative;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySound(sound);
            UpdateScore();
            gameObject.SetActive(false);
        }

    }

    private void UpdateScore()
    {
        EventManager.OnScoreUpdated?.Invoke(scoreWorth);
    }
}