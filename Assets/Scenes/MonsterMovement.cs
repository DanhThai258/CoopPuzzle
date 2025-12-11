using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [Header("=== PATROL POINTS (phải có đúng 2 điểm) ===")]
    public Transform[] patrolPoints;

    [Header("=== SETTINGS ===")]
    public float moveSpeed = 3f;
    public float chaseDistance = 6f;
    public float stopDistance = 1.2f;   // quái dừng lại khi gần Player
    public float zoneBuffer = 2f;

    [Header("=== PLAYER (tự động tìm) ===")]
    [SerializeField] private Transform playerTransform;

    private bool isChasing = false;
    private int patrolDestination = 0;

    private void Awake() => FindPlayer();
    private void Reset() => FindPlayer();
    private void OnValidate() => FindPlayer();

    void FindPlayer()
    {
        if (playerTransform == null)
        {
            var go = GameObject.FindGameObjectWithTag("Player");
            if (go != null) playerTransform = go.transform;
        }
    }

    void Update()
    {
        if (playerTransform == null)
        {
            FindPlayer();
            if (playerTransform == null) return;
        }

        if (patrolPoints == null || patrolPoints.Length < 2 ||
            patrolPoints[0] == null || patrolPoints[1] == null)
        {
            Debug.LogError($"[Monster {name}] Thiếu Patrol Points!");
            return;
        }

        float left = Mathf.Min(patrolPoints[0].position.x, patrolPoints[1].position.x);
        float right = Mathf.Max(patrolPoints[0].position.x, patrolPoints[1].position.x);

        bool playerInPatrolZone =
            playerTransform.position.x >= left - zoneBuffer &&
            playerTransform.position.x <= right + zoneBuffer;

        float distToPlayer = Mathf.Abs(playerTransform.position.x - transform.position.x);

        // =====================================================
        // ============= LOGIC QUYẾT ĐỊNH CHASE =================
        // =====================================================
        if (playerInPatrolZone && distToPlayer <= chaseDistance)
            isChasing = true;
        else
            isChasing = false;

        // =====================================================
        //                   CHASE LOGIC
        // =====================================================
        if (isChasing)
        {
            // Nếu còn xa → chạy đến Player
            if (distToPlayer > stopDistance)
            {
                Vector2 target = new Vector2(playerTransform.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            }
            else
            {
                // Đứng yên khi gần Player
                transform.position = transform.position;

                // Nơi bạn sẽ gọi animation Attack
                // animator.SetTrigger("Attack");
            }

            FlipFacing(playerTransform.position.x > transform.position.x);
        }
        else
        {
            Patrol();
        }
    }

    // ============================================================
    //                       PATROL LOGIC
    // ============================================================
    void Patrol()
    {
        Vector2 targetPos = new Vector2(patrolPoints[patrolDestination].position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPos) < 0.2f)
            patrolDestination = (patrolDestination == 0) ? 1 : 0;

        FlipFacing(patrolPoints[patrolDestination].position.x > transform.position.x);
    }

    // ============================================================
    //                   LẬT HƯỚNG QUÁI
    // ============================================================
    private void FlipFacing(bool faceRight)
    {
        Vector3 scale = transform.localScale;
        scale.x = faceRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        if (patrolPoints != null && patrolPoints.Length >= 2 && patrolPoints[0] && patrolPoints[1])
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(patrolPoints[0].position, patrolPoints[1].position);

            Gizmos.color = new Color(0, 1, 1, 0.2f);
            float w = Mathf.Abs(patrolPoints[0].position.x - patrolPoints[1].position.x) + zoneBuffer * 2f;
            Vector3 center = (patrolPoints[0].position + patrolPoints[1].position) / 2f;
            Gizmos.DrawCube(center, new Vector3(w, 6f, 1f));

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
