using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float visionAngle = 60f;
    [SerializeField] private float visionDistance = 5f;
    [SerializeField] private LayerMask visionMask;
    [SerializeField] private Transform[] waypoints;

    private Rigidbody2D rb;
    private EnemyState currentState;
    private GameObject playerInRange;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentState = new PatrollingState(this, waypoints, speed);
        EventBus.GameStateChanged += OnGameStateChanged;

        CircleCollider2D visionTrigger = GetComponentInChildren<CircleCollider2D>();
        if (visionTrigger != null && visionTrigger.isTrigger)
        {
            visionTrigger.radius = visionDistance;
        }
    }

    private void OnDestroy()
    {
        EventBus.GameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.Won || state == GameState.Lost)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void Update()
    {
        if (GameManager.Instance.currentState == GameState.Playing)
        {
            currentState.Update();
            CheckPlayerDetection();
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = null;
        }
    }

    private void CheckPlayerDetection()
    {
        if (playerInRange == null) return;

        Vector2 directionToPlayer = (playerInRange.transform.position - transform.position).normalized;
        float angle = Vector2.Angle(transform.up, directionToPlayer);

        if (angle < visionAngle / 2)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, visionDistance, visionMask);
            if (hit.collider != null && hit.collider.gameObject == playerInRange)
            {
                EventBus.RaisePlayerDetected();
            }
        }
    }
}
