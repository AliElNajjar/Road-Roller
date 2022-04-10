using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Collectible : MonoBehaviour
{
    [SerializeField] private int scoreWorth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UpdateScore();
            gameObject.SetActive(false);
        }

    }

    private void UpdateScore()
    {
        //scoremanager....
    }
}