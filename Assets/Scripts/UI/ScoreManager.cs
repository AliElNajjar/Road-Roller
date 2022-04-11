using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Text scoreText;

    private int score;

    private void Start()
    {
        EventManager.OnScoreUpdated += UpdateScore;
        EventManager.OnRestartRequested += () => UpdateScore(-score);
    }

    private void UpdateScore(int points)
    {
        score += points;
        scoreText.text = string.Format("{0:00}", score);
        if (score < 0)
        {
            EventManager.OnLevelEnd?.Invoke();
            AudioManager.Instance.PlaySound(AudioManager.Sounds.Lost);
        }
            
    }
}
