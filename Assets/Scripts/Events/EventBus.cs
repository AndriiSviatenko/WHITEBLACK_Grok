using System;
using UnityEngine;

public static class EventBus
{
    public static event Action PlayerDetected;
    public static event Action PlayerEnteredLight;
    public static event Action PlayerReachedEnd;
    public static event Action<GameState> GameStateChanged;

    public static void RaisePlayerDetected() => PlayerDetected?.Invoke();
    public static void RaisePlayerEnteredLight() => PlayerEnteredLight?.Invoke();
    public static void RaisePlayerReachedEnd() => PlayerReachedEnd?.Invoke();
    public static void RaiseGameStateChanged(GameState state) => GameStateChanged?.Invoke(state);
}