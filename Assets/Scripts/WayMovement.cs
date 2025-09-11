using UnityEngine;

public class WayMovement : MonoBehaviour
{
    public float speed = 3f;
    public float chaseRadius = 10f;
    public float attackRadius = 2f;
    public Transform player;

    private Transform[] waypoints;
    private int currentIndex = 0;
    private Animator animator;
    private float idleTimer = 0f;
    private bool isIdle = true;

    private Vector3 spawnPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        spawnPosition = transform.position; // remember where spider was placed in scene

        // Find all waypoints
        GameObject parent = GameObject.FindGameObjectWithTag("WayPoints");
        Transform[] allChildren = parent.GetComponentsInChildren<Transform>();
        waypoints = System.Array.FindAll(allChildren, t => t != parent.transform);

        // Pick the closest waypoint to spawn position
        if (waypoints.Length > 0)
        {
            float closestDist = Mathf.Infinity;
            for (int i = 0; i < waypoints.Length; i++)
            {
                float dist = Vector3.Distance(spawnPosition, waypoints[i].position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    currentIndex = i;
                }
            }
        }

        EnterIdle();
    }

    void Update()
    {
        if (player == null || waypoints.Length == 0) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // ▶ ATTACK
        if (distanceToPlayer <= attackRadius)
        {
            EnterAttack();
            return;
        }

        // ▶ CHASE
        if (distanceToPlayer <= chaseRadius)
        {
            EnterChase();
            return;
        }

        // ▶ If player is out of range but was chasing or attacking
        if (animator.GetBool("isChasing") || animator.GetBool("isAttacking"))
        {
            EnterIdle();  // Reset to idle and start wait
            return;
        }

        // ▶ Idle waiting logic
        if (isIdle)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= 5f)
            {
                EnterPatrol();
            }
        }

        // ▶ Patrol logic
        if (animator.GetBool("isPatrolling"))
        {
            Transform target = waypoints[currentIndex];
            MoveTowards(target.position);

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                currentIndex = (currentIndex + 1) % waypoints.Length;
                EnterIdle();
            }
        }
    }

    void MoveTowards(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        FaceTarget(target);
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            lookRotation *= Quaternion.Euler(0, 180, 0); // Flip if needed
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    // ▶ State Functions

    void EnterIdle()
    {
        ResetBools();
        isIdle = true;
        idleTimer = 0f;
    }

    void EnterPatrol()
    {
        ResetBools();
        animator.SetBool("isPatrolling", true);
        isIdle = false;
    }

    void EnterChase()
    {
        ResetBools();
        animator.SetBool("isChasing", true);
        MoveTowards(player.position);
    }

    void EnterAttack()
    {
        ResetBools();
        animator.SetBool("isAttacking", true);
        FaceTarget(player.position);
    }

    void ResetBools()
    {
        animator.SetBool("isPatrolling", false);
        animator.SetBool("isChasing", false);
        animator.SetBool("isAttacking", false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}
