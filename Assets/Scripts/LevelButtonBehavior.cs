using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtonBehavior : MonoBehaviour
{
    int Level = 1;
    string scene = "PlayScene";
    public TMP_Text levelNum;

    public void StartLevel()
    {
        Level = Convert.ToInt32(levelNum.text);
        LevelData.LevelNumber = Level;
        switch (Level)
        {
            case 0: scene = "PlayScene"; LevelData.opening = false; LevelData.story = false ; break;
            case 1: scene = "PlayScene"; LevelData.opening = true; LevelData.story = true; break;
            case 2: scene = "PlayScene"; LevelData.opening = false; LevelData.story = false; break;
            case 3: scene = "PlayScene"; LevelData.opening = false; LevelData.story = false ; break;
            case 4: scene = "Playscene v2"; LevelData.opening = true; LevelData.story = true; break;
            case 5: scene = "Playscene v2"; LevelData.opening = false; LevelData.story = false; break;
            case 6: scene = "Playscene v2"; LevelData.opening = false; LevelData.story = false; break;
            case 7: scene = "Playscene v3"; LevelData.opening = true; LevelData.story = true; break;
            case 8: scene = "Playscene v3"; LevelData.opening = false; LevelData.story = false; break;
            case 9: scene = "Playscene v3"; LevelData.opening = false; LevelData.story = false; break;
            case 10: scene = "Playscene v3"; LevelData.opening = false; LevelData.story = true; break;
        }
        SceneManager.LoadScene(scene);
    }
    public void decreaseLevel()
    {
        int num = Mathf.Max(Convert.ToInt32(levelNum.text) - 1, 1);
        levelNum.text = num.ToString();
    }

    public void increaseLevel()
    {
        int num = Mathf.Min(Convert.ToInt32(levelNum.text) + 1, 10);
        levelNum.text = num.ToString();
    }

    public void skipStory()
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void StartEndless()
    {
        LevelData.LevelNumber = 0;
        scene = "PlayScene";
        LevelData.opening = false;
        LevelData.story = false;
        SceneManager.LoadScene(scene);
    }
}
