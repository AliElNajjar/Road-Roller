using System;
using UnityEngine.Events;

public static class EventManager
{
    [Serializable] public class EventGameState : UnityEvent<GameManager.GameState> { }

    #region Actions
    public static Action<float> OnInputDetected;
    public static Action OnPauseToggle;
    public static Action OnStartRequested;
    public static Action OnRestartRequested;
    public static Action OnQuitRequested;
    public static Action OnLevelEnd;
    public static Action<int> OnScoreUpdated;
    #endregion
}
