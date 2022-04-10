using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        enabled = false;
    }

    protected virtual void HandleGameStateChanged(GameManager.GameState currentState)
    {
        enabled = currentState == GameManager.GameState.Running;
    }
}
