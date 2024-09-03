using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandlerPane : MonoBehaviour, IPointerMoveHandler
{
    bool mouseClicked;

    public void OnPointerMove(PointerEventData eventData)
    {
        if (GameObject.FindGameObjectWithTag("MainCamera").layer == 3)
        {
            if (mouseClicked)
            {
                this.transform.GetComponent<PaneBehavior>().ChangeState();
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseClicked = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseClicked = false;
        }
    }
}
