using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    Coroutine Drawing;
    GameObject LineToKill;

    GameObject handler;

    bool MouseButtonDowned = false;
    private void Start()
    {
        handler = GameObject.FindGameObjectWithTag("Controller");
    }
    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera").layer == 3 && handler.GetComponent<PlaySceneController>().CurrentState == States.Drawing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartLine();
                MouseButtonDowned = true;
            }
            if (Input.GetMouseButtonUp(0) && MouseButtonDowned)
            {
                FinishLine();
                handler.GetComponent<DrawingPanel>().CheckStates();
            }
        }
    }

    private void StartLine()
    {
        if (Drawing != null)
        {
            StopCoroutine(Drawing);
        }
        Drawing = StartCoroutine(DrawLine());
    }

    private void FinishLine()
    {
        if (Drawing != null)
            StopCoroutine(Drawing);
        UnityEngine.Object.Destroy(LineToKill);
    }

    IEnumerator DrawLine()
    {
        GameObject newGameObject = Instantiate(Resources.Load("Line") as GameObject, new Vector3(0,0,0), Quaternion.identity);
        LineToKill = newGameObject;
        LineRenderer line = newGameObject.GetComponent<LineRenderer>();
        line.positionCount = 0;
        line.sortingLayerName = "NoLighting";

        while(true)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            line.positionCount++;
            line.SetPosition(line.positionCount-1, position);
            yield return null;
        }
    }
}
