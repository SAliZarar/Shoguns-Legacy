using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandlerEnemy : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (GameObject.FindGameObjectWithTag("MainCamera").layer == 0)
        {
            /*Enemy enemy = this.gameObject.GetComponent<Enemy>();
            if (enemy != null && enemy.clickable)
            {
                transform.parent.GetComponentInChildren<PlaySceneController>().SetDrawState(true);
                enemy.Clicked();
            }*/
        }
    }

    public void click()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera").layer == 0)
        {
            Enemy enemy = gameObject.GetComponent<Enemy>();
            if (enemy != null && enemy.clickable)
            {
                transform.parent.GetComponentInChildren<PlaySceneController>().SetDrawState(true);
                enemy.Clicked();
            }
        }
    }
}
