using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonBehavior : MonoBehaviour
{
    public GameObject levelSelectionMenu;
    public GameObject settingsMenu;
    public void LoadMainMenu()
    {
        GameObject.Destroy(GameObject.FindWithTag("AudioManager"));
        SceneManager.LoadScene("MainMenu");
    }
    
    public void BackToMainMenu()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void SelectLevel()
    {
        levelSelectionMenu.SetActive(true);
    }

    public void nextLevel()
    {
        LevelData.LevelNumber = LevelData.LevelNumber + 1;
        int level = LevelData.LevelNumber;
        switch (level)
        {
            case 0: LevelData.opening = false; SceneManager.LoadScene("PlayScene Endless"); LevelData.story = false; break;
            case 1: LevelData.opening = true; SceneManager.LoadScene("PlayScene"); LevelData.story = true; break;
            case 2: LevelData.opening = false; SceneManager.LoadScene("PlayScene"); LevelData.story = false; break;
            case 3: LevelData.opening = false; SceneManager.LoadScene("PlayScene"); LevelData.story = false; break;
            case 4: LevelData.opening = true; SceneManager.LoadScene("PlayScene v2"); LevelData.story = true; break;
            case 5: LevelData.opening = false; SceneManager.LoadScene("PlayScene v2"); LevelData.story = false; break;
            case 6: LevelData.opening = false; SceneManager.LoadScene("PlayScene v2"); LevelData.story = false; break;
            case 7: LevelData.opening = true; SceneManager.LoadScene("PlayScene v3"); LevelData.story = true; break;
            case 8: LevelData.opening = false; SceneManager.LoadScene("PlayScene v3"); LevelData.story = false; break;
            case 9: LevelData.opening = false; SceneManager.LoadScene("PlayScene v3"); LevelData.story = false; break;
            case 10: LevelData.opening = false; SceneManager.LoadScene("PlayScene v4"); LevelData.story = true; break;
            case 11: LoadMainMenu(); break;
        }
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
    }

    public void ToggleAudio()
    {
        if (LevelData.audio)
        {
            LevelData.audio = false;
            GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().MuteBGM();
        }
        else
        {
            LevelData.audio = true;
            GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>().UnMuteBGM();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
