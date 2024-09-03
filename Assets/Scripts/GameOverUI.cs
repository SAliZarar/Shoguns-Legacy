using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    bool fadeIn = false;
    public GameObject youDiedImg;
    public Button restart;
    public Button mainmenu;
    public void SetUp()
    {
        gameObject.SetActive(true);
        fadeIn = true;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        int level = LevelData.LevelNumber;
        string scene = "menu";
        switch (level)
        {
            case 0: scene = "PlayScene"; LevelData.opening = false; LevelData.story = false; break;
            case 1: scene = "PlayScene"; LevelData.opening = false; LevelData.story = false; break;
            case 2: scene = "PlayScene"; LevelData.opening = false; LevelData.story = false; break;
            case 3: scene = "PlayScene"; LevelData.opening = false; LevelData.story = false; break;
            case 4: scene = "Playscene v2"; LevelData.opening = false; LevelData.story = false; break;
            case 5: scene = "Playscene v2"; LevelData.opening = false; LevelData.story = false; break;
            case 6: scene = "Playscene v2"; LevelData.opening = false; LevelData.story = false; break;
            case 7: scene = "Playscene v3"; LevelData.opening = false; LevelData.story = false; break;
            case 8: scene = "Playscene v3"; LevelData.opening = false; LevelData.story = false; break;
            case 9: scene = "Playscene v3"; LevelData.opening = false; LevelData.story = false; break;
            case 10: scene = "Playscene v3"; LevelData.opening = false; LevelData.story = false; break;
        }
        SceneManager.LoadScene(scene);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        if (fadeIn && youDiedImg.GetComponent<SpriteRenderer>().color.a != 1)
        {
            youDiedImg.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, youDiedImg.GetComponent<SpriteRenderer>().color.a + 0.02f);
        }
        if (youDiedImg.GetComponent<SpriteRenderer>().color.a >= 1)
        {
            restart.gameObject.SetActive(true);
            mainmenu.gameObject.SetActive(true);
        }
    }
}
