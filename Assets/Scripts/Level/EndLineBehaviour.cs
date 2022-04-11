using UnityEngine;

public class EndLineBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySound(AudioManager.Sounds.Won);

            EventManager.OnLevelEnd?.Invoke();
        }

    }
}
