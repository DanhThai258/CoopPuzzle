using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip idleSound;
    public AudioClip runSound;
    public AudioClip attackSound;

    // ============================
    // IDLE SOUND
    // ============================
    public void PlayIdleSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(idleSound);
    }

    // ============================
    // RUN SOUND (CHỈ PHÁT 2 GIÂY)
    // ============================
    public void PlayRunSound()
    {
        // Gán clip
        audioSource.clip = runSound;

        // Phát từ đầu
        audioSource.Stop();
        audioSource.Play();

        // Dừng lại sau 2 giây
        CancelInvoke(nameof(StopRunSound)); // tránh bị gọi đè
        Invoke(nameof(StopRunSound), 2f);
    }

    private void StopRunSound()
    {
        audioSource.Stop();
    }

    // ============================
    // ATTACK SOUND
    // ============================
    public void PlayAttackSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(attackSound);
    }
}
