using UnityEngine;
using UnityEngine.SceneManagement;

public class ChucNangMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;

    void PlayClick()
    {
        if (audioSource != null && clickSound != null)
            audioSource.PlayOneShot(clickSound);
    }

    public void ChoiMoi()
    {
        PlayClick();
        Invoke("LoadChooseCharacter", 0.2f);
    }

    void LoadChooseCharacter()
    {
        SceneManager.LoadScene(1);
    }

    public void CaiDat()
    {
        PlayClick();
        Invoke("LoadSetting", 0.2f);
    }

    void LoadSetting()
    {
        SceneManager.LoadScene(3);
    }

    public void Thoat()
    {
        PlayClick();
        Invoke("ExitGame", 0.2f);
    }

    void ExitGame()
    {
        Application.Quit();
    }

    public void TroVeMenu()
    {
        PlayClick();
        Invoke("LoadMenu", 0.2f);
    }

    void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
