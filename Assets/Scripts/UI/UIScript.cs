using UnityEngine;

public class UIScript : MonoBehaviour
{
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject loseText;

    private void Awake()
    {
        EventBus.GameStateChanged += OnGameStateChanged;
        winText.SetActive(false);
        loseText.SetActive(false);
    }

    private void OnDestroy()
    {
        EventBus.GameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.Won)
        {
            winText.SetActive(true);
        }
        else if (state == GameState.Lost)
        {
            loseText.SetActive(true);
        }
    }
}