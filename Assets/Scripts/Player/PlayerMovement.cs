using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private bool isMoving = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        EventBus.GameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        EventBus.GameStateChanged -= OnGameStateChanged;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && rb.linearVelocity == Vector2.zero)
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMoving = true;
        }

        if (isMoving)
        {
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            rb.linearVelocity = direction * speed;

            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                rb.linearVelocity = Vector2.zero;
                isMoving = false;
            }
        }
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.Won || state == GameState.Lost)
        {
            isMoving = false;
            rb.linearVelocity = Vector2.zero;
        }
    }
}
