using System;
using System.Collections.Generic;
using UnityEngine;

public class DrawingPanel : MonoBehaviour
{
    float timer = 0;
    int index = 0;

    static int counter = 0;
    GameObject pane;

    int PanelW = 5;
    int PanelH = 5;
    float scale = 1.5f;

    GameObject overlay;

    public Dictionary<Tuple<int, int>, bool> PaneStates = new Dictionary<Tuple<int, int>, bool>();
    public Dictionary<Tuple<int, int>, GameObject> Panes = new Dictionary<Tuple<int, int>, GameObject>();
    public List<Tuple<int, int>> RequiredPanes = new List<Tuple<int, int>>();
    public List<Tuple<int, int>> VisiblePanes = new List<Tuple<int, int>>();
    public static GameObject overl;

    private void Awake()
    {
        pane = Resources.Load("PatternPlane") as UnityEngine.GameObject;
        overlay = Resources.Load("PanelOverlay") as UnityEngine.GameObject;
        pane.transform.localScale = new Vector3(scale, scale, scale);
        SetActive();
    }

    public void SetActive()
    {
        transform.parent = Camera.main.transform;
        if (overl == null)
        {
            overl = GameObject.Instantiate(overlay, new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y - 3), Quaternion.identity);
            overl.transform.parent = transform;
        }

        PaneStates.Clear();
        Panes.Clear();
        RequiredPanes.Clear();

        float offsetX = Camera.main.transform.position.x - 1.8f;
        float offsetY = Camera.main.transform.position.y + -1.2f;
        float distance = 0.15f;
        for (int i = 0; i < PanelW; i++)
        {
            bool skip = false;
            for (int j = 0; j < PanelH; j++)
            {
                GameObject obj = GameObject.Instantiate(pane, new Vector2(offsetX + i * scale/2 + i * distance, offsetY - j * scale/2 - j * distance), Quaternion.identity);
                PaneBehavior behavior = obj.AddComponent<PaneBehavior>();

                behavior.ID = counter++;

                obj.AddComponent<BoxCollider2D>();
                obj.AddComponent<InputHandlerPane>();
                obj.transform.parent = this.transform;

                Tuple<int, int> index = new Tuple<int, int>(i, j);

                behavior.index = index;
                Panes.Add(index, obj);
                PaneStates.Add(index, false);

                if (UnityEngine.Random.Range(0, 10) >= 7 && !skip)
                {
                    RequiredPanes.Add(index);
                    obj.GetComponent<PaneBehavior>().required = true;
                    if (UnityEngine.Random.Range(0, 10) >= 5) skip = true;
                }
                /*if (j == PanelH - 1 && !atleast1)
                {
                    Tuple<int, int> t = new Tuple<int, int>(i, UnityEngine.Random.Range(0, PanelH));
                    RequiredPanes.Add(t);
                    Panes[t].GetComponent<PaneBehavior>().required = true;
                }*/
            }
        }
    }

    private void FixedUpdate()
    {
        if(index < RequiredPanes.Count)
        {
            timer = timer + Time.fixedDeltaTime;
            if (timer > 0.1)
            {
                Panes[RequiredPanes[index]].GetComponent<PaneBehavior>().Reveal();
                timer = 0;
                index++;
            }
        }
    }

    public void PaneStateChange(Tuple<int, int> index)
    {
        foreach (Tuple<int, int> req in RequiredPanes)
        {
            if (req == index)
            {
                PaneStates[index] = true;
                Panes[index].GetComponent<SpriteRenderer>().color = new Color(17, 76, 82, 140);
            }
        }
    }

    public void CheckStates()
    {
        bool correct = true;
        foreach (Tuple<int, int> pane in RequiredPanes)
        {
            if (PaneStates[pane] != true)
            {
                correct = false; break;
            }
        }
        if (correct)
        {
            Success();
        }
        else
        {
            Failure();
        }
    }

    private void Failure()
    {
        transform.GetComponent<PlaySceneController>().TakeDamage(1);
        transform.GetComponent<PlaySceneController>().EnemyAttack();
        ResetPanel();
    }

    private void Success()
    {
        Camera.main.gameObject.layer = LayerMask.NameToLayer("Default");
        transform.GetComponent<PlaySceneController>().KillEnemy(1);
        transform.GetComponent<PlaySceneController>().SetDrawState(false);
        DestroyPanel();
    }

    public void DestroyPanel()
    {
        foreach (var pair in Panes)
        {
            Destroy(pair.Value);
        }
        Destroy(overl);
        Camera.main.gameObject.layer = 0;
        Destroy(GetComponent<DrawingPanel>());
    }

    private void ResetPanel()
    {
        foreach (var pair in Panes)
        {
            Destroy(pair.Value);
        }
        transform.GetComponent<PlaySceneController>().CreateDrawingPanel();
    }
}
