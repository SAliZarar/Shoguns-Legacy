using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StoryPaneBehavior : MonoBehaviour
{
    public GameObject textbox;
    public int frames = 500;
    private int elapsedframes = 0;
    private void Update()
    {
        if (elapsedframes > frames)
        {
            gameObject.SetActive(false);
        }
        else
        {
            textbox.GetComponent<RectTransform>().Translate(new Vector3 (0, 2, 0));
            elapsedframes++;
        }
    }
}
