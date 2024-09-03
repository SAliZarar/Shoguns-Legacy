using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonBehavior : MonoBehaviour
{
    public GameObject menu;
    public void PauseGame()
    {
        menu.SetActive(true);
    }
}
