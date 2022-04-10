using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup mainPanel;
    [Space]
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject endPanel;
    [Space]
    [SerializeField] private Button startBtn;
    [SerializeField] private Button pauseBtn;
    [SerializeField] private Button quitBtn;
    [SerializeField] private Button restartBtn;
    [SerializeField] private float fadeDuration = 1f;


    private Sprite playSprite, pauseSprite;


    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);

        AddButtonListeners();

        LoadSprites();
    }

    private void HandleGameStateChanged(GameManager.GameState currentState)
    {
        mainPanel.gameObject.SetActive(currentState == GameManager.GameState.Pregame);
        inGamePanel.SetActive(currentState != GameManager.GameState.Pregame);
        pausePanel.SetActive(currentState == GameManager.GameState.Paused);
        endPanel.SetActive(currentState == GameManager.GameState.Ended);
        pauseBtn.gameObject.SetActive(currentState != GameManager.GameState.Ended);
            
        pauseBtn.image.sprite = currentState == GameManager.GameState.Paused ? playSprite : pauseSprite;
    }


    private void AddButtonListeners()
    {
        startBtn.onClick.AddListener(InitGame);
        pauseBtn.onClick.AddListener(() => EventManager.OnPauseToggle?.Invoke());
        quitBtn.onClick.AddListener(() => EventManager.OnQuitRequested?.Invoke());
        restartBtn.onClick.AddListener(HandleRestartRequested);
    }


    private void LoadSprites()
    {
        pauseSprite = Resources.Load<Sprite>("Pause Button Sprite");
        playSprite = Resources.Load<Sprite>("Play Button Sprite");
    }


    private void InitGame()
    {
        StartCoroutine(FadeOut(StartGame));
    }

    private IEnumerator FadeOut(Action onComplete = null)
    {
        float elaspedTime = 0f;
        while (elaspedTime <= fadeDuration)
        {
            elaspedTime += Time.deltaTime;
            mainPanel.alpha = Mathf.Lerp(1f, 0f, elaspedTime / fadeDuration);
            yield return null;
        }
        mainPanel.alpha = 0f;
        mainPanel.gameObject.SetActive(false);
        mainPanel.alpha = 1f;

        onComplete?.Invoke();
    }

    private void StartGame()
    {
        EventManager.OnStartRequested.Invoke();
    }

    public void HandleRestartRequested()
    {
        EventManager.OnRestartRequested?.Invoke();
    }
}
