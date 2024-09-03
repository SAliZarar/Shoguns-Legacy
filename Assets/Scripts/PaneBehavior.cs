using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PaneBehavior : MonoBehaviour
{
    public bool visible = false;
    public float visibleTime = 0f;
    public float visibleTimeReq = 1.5f;
    public bool fadeOut = false;
    public int ID = 0;
    public bool required = false;

    public bool clicked = false;
    public Tuple<int, int> index;
    public void ChangeState()
    {
        if (this.clicked == false && this.required)
        {
            Vibration.Vibrate(100);
        }
        this.clicked = true;
        this.transform.parent.GetComponent<DrawingPanel>().PaneStateChange(index);
    }

    internal void Reveal()
    {
        visible = true;
    }

    private void Update()
    {
        if (visible)
        {
            Color color = gameObject.GetComponent<SpriteRenderer>().color;
            if (color.a <= 1)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, color.a + 0.01f);
                if (gameObject.GetComponent<SpriteRenderer>().color.a >= 0.4)
                {
                    visible = false;
                }
            }
        }
        /*else if (fadeOut)
        {
            Color color = gameObject.GetComponent<SpriteRenderer>().color;
            if (color.a >= 0)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, color.a - 0.01f);
            }
        }*/
        else
        {
            visibleTime += Time.deltaTime;
            if (visibleTime > visibleTimeReq)
            {
                fadeOut = true;
            }
        }
    }
}
