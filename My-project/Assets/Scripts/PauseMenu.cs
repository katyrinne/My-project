using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool PauseGame;
    public GameObject pauseGameMenu;
    public AudioMixer audioMixer;
    public AudioMixerSnapshot pausedSnapshot;
    public AudioMixerSnapshot unpausedSnapshot;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (PauseGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseGameMenu.SetActive(false); 
        Time.timeScale = 1f;
        unpausedSnapshot.TransitionTo(0.1f); // Восстанавливаем обычный снэпшот
        PauseGame = false;
    }

    public void Pause()
    {
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
        pausedSnapshot.TransitionTo(0.1f); // Активируем снэпшот для паузы
        PauseGame = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
