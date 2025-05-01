using UnityEngine;

public class PatrollingState : EnemyState
{
    private Transform[] waypoints;
    private int currentIndex = 0;
    private float speed;
    private Rigidbody2D rb;

    public PatrollingState(EnemyAI enemy, Transform[] waypoints, float speed)
    {
        this.enemy = enemy;
        this.waypoints = waypoints;
        this.speed = speed;
        this.rb = enemy.GetComponent<Rigidbody2D>();
    }

    public override void Update()
    {
        if (waypoints.Length == 0) return;

        Vector2 target = waypoints[currentIndex].position;
        Vector2 direction = (target - (Vector2)enemy.transform.position).normalized;
        rb.linearVelocity = direction * speed;
        enemy.transform.up = direction;

        if (Vector2.Distance(enemy.transform.position, target) < 0.1f)
        {
            currentIndex = (currentIndex + 1) % waypoints.Length;
        }
    }
}
