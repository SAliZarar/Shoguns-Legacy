using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPlayer : MonoBehaviour
{
    GameObject Player;
    public BackgroundMovement bgmvt;
    bool movebg;

    private void Start()
    {
        if (LevelData.LevelNumber == 0)
        {
            movebg = false;
        }
        else
        {
            movebg = true;
        }
    }
    public void setPlayer()
    {
        Player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        if (Player != null)
        if (Player.transform.position.y > transform.position.y + 2.2)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
            if (movebg)
            {
                bgmvt.Move(0.05f);
            }
        }
    }
}
