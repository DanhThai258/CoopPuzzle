using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("=== THIẾT LẬP TẤN CÔNG ===")]
    // Điểm khởi phát đòn tấn công (Kéo một GameObject con vào đây)
    public Transform attackPoint;

    // Lực sát thương của Player
    public int attackDamage = 1;

    // Tầm đánh (bán kính phát hiện va chạm)
    public float attackRange = 1.5f;

    // Layer chứa Quái vật để tấn công (Nhớ gán Layer 'Monster' vào đây!)
    public LayerMask monsterLayer;

    // Tốc độ đánh
    private float timeToNextAttack = 0f;
    public float attackRate = 1f; // Ví dụ: 1 lần/giây

    // Thường được đặt trên đối tượng Player
    void Update()
    {
        // Xử lý Cooldown
        if (timeToNextAttack > 0)
        {
            timeToNextAttack -= Time.deltaTime;
        }

        // Xử lý Input
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            if (timeToNextAttack <= 0)
            {
                Attack();
                timeToNextAttack = 1f / attackRate; // Đặt lại thời gian hồi chiêu
            }
        }
    }

    void Attack()
    {
        // Kiểm tra xem đã thiết lập attackPoint chưa
        if (attackPoint == null)
        {
            Debug.LogError("Chưa gán Attack Point! Vui lòng kéo một GameObject con vào trường attackPoint.");
            return;
        }

        Debug.Log("Player tấn công!");

        // 1. Dùng Physics2D.OverlapCircle để phát hiện đối tượng trong tầm đánh
        // DÙNG VỊ TRÍ CỦA attackPoint thay vì transform.position
        Collider2D[] hitMonsters = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, monsterLayer);


        // 2. Lặp qua tất cả quái vật bị trúng đòn
        foreach (Collider2D monster in hitMonsters)
        {
            // Cố gắng lấy Component MonsterHealth từ đối tượng bị trúng
            MonsterHealth monsterHealth = monster.GetComponent<MonsterHealth>();

            if (monsterHealth != null)
            {
                // Gọi hàm TakeDamage trên quái vật
                monsterHealth.TakeDamage(attackDamage);
            }
        }
    }

    // Dùng để vẽ tầm đánh trong Scene View (chỉ hiển thị trong Editor)
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        // Vẽ một hình tròn đại diện cho tầm tấn công xung quanh attackPoint
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}