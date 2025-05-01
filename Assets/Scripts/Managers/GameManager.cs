using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState currentState = GameState.Playing;
    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        EventBus.PlayerDetected += OnPlayerDetected;
        EventBus.PlayerEnteredLight += OnPlayerEnteredLight;
        EventBus.PlayerReachedEnd += OnPlayerReachedEnd;
    }

    private void OnDestroy()
    {
        EventBus.PlayerDetected -= OnPlayerDetected;
        EventBus.PlayerEnteredLight -= OnPlayerEnteredLight;
        EventBus.PlayerReachedEnd -= OnPlayerReachedEnd;
    }

    private void OnPlayerDetected()
    {
        if (currentState == GameState.Playing)
        {
            SetState(GameState.Lost);
        }
    }

    private void OnPlayerEnteredLight()
    {
        if (currentState == GameState.Playing)
        {
            SetState(GameState.Lost);
        }
    }

    private void OnPlayerReachedEnd()
    {
        if (currentState == GameState.Playing)
        {
            SetState(GameState.Won);
        }
    }

    private void SetState(GameState newState)
    {
        currentState = newState;
        EventBus.RaiseGameStateChanged(currentState);
    }
}
