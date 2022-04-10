using UnityEngine;

public class EndLineBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.OnLevelEnd?.Invoke();
        }

    }
}
