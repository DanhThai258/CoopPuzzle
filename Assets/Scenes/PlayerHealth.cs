using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // CẦN THAY ĐỔI: Sử dụng float cho máu để tương thích tốt hơn với Slider (UI)
    public float maxHealth = 10f;
    public float currentHealth; // Đổi tên từ 'health' sang 'currentHealth' cho rõ ràng

    [Header("UI Connection")]
    // Kéo Player Health Bar (Slider có script HealthBar) vào đây từ Inspector
    public HealthBar healthBarUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;

        // Thiết lập giá trị max ban đầu cho UI
        if (healthBarUI != null)
        {
            healthBarUI.SetMaxHealth(maxHealth);
        }
    }

    // Hàm nhận sát thương
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0f); // Đảm bảo máu không âm

        // CẬP NHẬT THANH MÁU UI
        if (healthBarUI != null)
        {
            healthBarUI.SetHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Hàm mới: Xử lý khi nhân vật chết
    void Die()
    {
        Debug.Log(gameObject.name + " đã bị tiêu diệt!");
        // Thêm animation chết, hiệu ứng, v.v. ở đây
        Destroy(gameObject);
    }

    // Update giữ nguyên
    void Update()
    {
        // Có thể thêm logic Debug ở đây, ví dụ: 
        // if (Input.GetKeyDown(KeyCode.K)) TakeDamage(1);
    }
}