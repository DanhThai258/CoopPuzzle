using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    [Header("Health Settings")]
    // Dùng float cho máu để tương thích với Slider UI
    public float maxHealth = 5f;
    public float currentHealth; // Đổi tên từ 'health'

    [Header("UI Connection")]
    // Kéo Prefab Health Bar (World Space) vào đây từ Inspector
    public GameObject healthBarPrefab;
    private HealthBar healthBar; // Tham chiếu đến script HealthBar

    void Start()
    {
        currentHealth = maxHealth;

        // 1. TẠO VÀ GÁN THANH MÁU KHI QUÁI VẬT XUẤT HIỆN
        if (healthBarPrefab != null)
        {
            // Khởi tạo Health Bar và đặt nó làm con của Quái vật
            GameObject healthBarObject = Instantiate(healthBarPrefab, transform);

            // Điều chỉnh vị trí (ví dụ: trên đầu quái vật)
            // Giá trị (0, 1.5f, 0) là vị trí tương đối so với Baby Dog
            healthBarObject.transform.localPosition = new Vector3(0, 1.5f, 0);

            // Lấy script HealthBar từ Prefab 
            // Dùng GetComponentInChildren vì HealthBar nằm trên Slider, là con của Canvas
            healthBar = healthBarObject.GetComponentInChildren<HealthBar>();

            if (healthBar != null)
            {
                healthBar.SetMaxHealth(maxHealth);
            }
        }
    }

    // Hàm công khai để nhận sát thương từ Player
    public void TakeDamage(int damage)
    {
        // Giảm máu
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0f); // Đảm bảo máu không âm

        // 2. CẬP NHẬT THANH MÁU UI
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        Debug.Log($"Quái vật {gameObject.name} nhận {damage} sát thương. Máu còn lại: {currentHealth}");

        // Kiểm tra nếu quái vật chết
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"Quái vật {gameObject.name} đã bị tiêu diệt!");
        // Hủy đối tượng quái vật (Thanh máu là con nên sẽ bị hủy theo)
        Destroy(gameObject);
    }
}