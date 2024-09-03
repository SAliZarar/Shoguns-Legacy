using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButtonBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public int index = 0;
    public GameObject[] images;
    public void OnButtonClicked()
    {
        if (index < images.Length - 1)
        {
            images[index].SetActive(false);
            index++;
            try
            {
                images[index].SetActive(true);
            }
            catch { }
        }
        else
        {
            gameObject.transform.parent.parent.gameObject.SetActive(false);
        }
    }
}
