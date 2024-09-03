using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnLevel1 : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject storyScene;
    void Start()
    {
        if (LevelData.LevelNumber == 1)
        {
            try
            {
                tutorial.SetActive(true);
            }
            catch { }
        }
        if (LevelData.story == true)
        {
            storyScene.SetActive(true);
        }
    }
}
