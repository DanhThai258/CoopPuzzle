using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] skins;
    public int selectedCharacter = 0;

    // Âm thanh
    public AudioSource audioSource;
    public AudioClip switchSound;   // âm thanh cho next/previous
    public AudioClip playSound;     // âm thanh cho nút Play

    private void Awake()
    {
        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);

        if (skins == null || skins.Length == 0) return;

        selectedCharacter = Mathf.Clamp(selectedCharacter, 0, skins.Length - 1);

        foreach (GameObject g in skins)
            g.SetActive(false);

        skins[selectedCharacter].SetActive(true);
    }

    public void ChangeNext()
    {
        skins[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % skins.Length;
        skins[selectedCharacter].SetActive(true);

        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
        PlayerPrefs.Save();

        PlaySwitchSound();
    }

    public void ChangePrevious()
    {
        skins[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter - 1 + skins.Length) % skins.Length;
        skins[selectedCharacter].SetActive(true);

        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
        PlayerPrefs.Save();

        PlaySwitchSound();
    }

    public void PlayGame()
    {
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
        PlayerPrefs.Save();

        PlayPlaySound(); // phát âm thanh play

        Invoke("LoadGameScene", 0.25f); // đợi 0.25s rồi load
    }

    // HÀM NÀY BẠN BỊ THIẾU — PHẢI CÓ!
    void LoadGameScene()
    {
        SceneManager.LoadScene(2);
    }

    // Âm thanh Next/Previous
    void PlaySwitchSound()
    {
        if (audioSource != null && switchSound != null)
            audioSource.PlayOneShot(switchSound);
    }

    // Âm thanh Play
    void PlayPlaySound()
    {
        if (audioSource != null && playSound != null)
            audioSource.PlayOneShot(playSound);
    }
}
