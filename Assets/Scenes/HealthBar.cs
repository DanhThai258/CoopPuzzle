using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;

    void Awake()
    {
        // Lấy component Slider
        slider = GetComponent<Slider>();
    }

    // Cập nhật giá trị thanh máu (từ 0 đến 1)
    public void SetHealth(float health)
    {
        slider.value = health;
    }

    // Thiết lập giá trị Max ban đầu (thường là 1)
    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth; // Đảm bảo thanh máu đầy ban đầu
    }
}