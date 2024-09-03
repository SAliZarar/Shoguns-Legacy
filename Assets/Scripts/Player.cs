using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlaySceneController controller;

    int healthTotal = 3;
    int health;
    int attacktime = 0;

    GameObject HealthBar;
    GameObject HeartRed;
    GameObject HeartWhite;

    List<GameObject> heartList = new List<GameObject>();
    Rigidbody2D rb;
    PlayerMovement movt;
    AudioManager aaudio;

    void Start()
    {
        health = 3;

        HealthBar = new GameObject("HealthBar");
        HealthBar.transform.parent = Camera.main.transform;
        HeartRed = Resources.Load("HealthHeartRed") as GameObject;
        HeartWhite = Resources.Load("HealthHeartWhite") as GameObject;

        GenerateHealth();

        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        movt = GetComponent<PlayerMovement>();

        aaudio = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void TakeDamage(int dam)
    {
        aaudio.PlayTakeDmgSFX();
        heartList[health - 1].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("HeartWhite");
        health -= dam;
        if (health<=0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(transform.GetComponent<BoxCollider2D>());
        GameObject obj = GameObject.FindGameObjectWithTag("Controller");
        controller = obj.GetComponent<PlaySceneController>();
        Lose();
    }

    public void Lose()
    {
        controller.Lose();
    }

    private void Update()
    {
        if (transform.position.y < 0)
        {
            controller.DestroyGameObject(gameObject);
        }

        if (attacktime > 25)
        {
            GetComponent<Animator>().SetBool("attacking", false);
            attacktime = 0;
        }
        else if (GetComponent<Animator>().GetBool("attacking") == true)
        {
            attacktime++;
        }
    }

    private void GenerateHealth()
    {
        int i = 0;
        while (i < health)
        {
            heartList.Add(GameObject.Instantiate(HeartRed, new Vector2(0.5f + (i * 0.5f), -0.5f), Quaternion.identity, HealthBar.transform));
            i++;
        }
        while (i < healthTotal)
        {
            heartList.Add(GameObject.Instantiate(HeartRed, new Vector2(0.5f + (i * 0.5f), -0.5f), Quaternion.identity, HealthBar.transform));
            i++;
        }
    }

    public void MoveToEnemy(GameObject enemy)
    {
        if (Mathf.Abs(enemy.gameObject.transform.position.y - transform.position.y) < 0.5)
        {
            //only movement in x 
            if (enemy.transform.position.x < transform.position.x)
            {
                movt.MoveLeft(enemy.transform.position.x);
            }
            else
            {
                movt.MoveRight(enemy.transform.position.x);
            }
        }
        else if (Mathf.Abs(enemy.transform.position.x - transform.position.x) < 2)
        {
            //movement in only y
            if (transform.position.x < 1.8)
            {
                movt.ImmediateMoveRight(1.8f);
            }
            else if (transform.position.x > 3.5)
            {
                movt.ImmediateMoveLeft(3.5f);
            }

            movt.JumpUp();

            if (enemy.transform.position.x < 1.8f)
            {
                movt.MoveLeft(enemy.transform.position.x);
            }
            else if (enemy.transform.position.x > 3.5)
            {
                movt.MoveRight(enemy.transform.position.x);
            }
        }
        else
        {
            //movement in x and y
            if (enemy.transform.position.x < transform.position.x)
            {
                if (transform.position.x < 1.8)
                {
                    movt.ImmediateMoveRight(1.8f);
                }
                else if (transform.position.x > 3.5)
                {
                    movt.ImmediateMoveLeft(3.5f);
                }
                movt.JumpLeft();
                movt.MoveLeft(enemy.transform.position.x);
            }
            else
            {
                if (transform.position.x < 1.8)
                {
                    movt.ImmediateMoveRight(1.8f);
                }
                else if (transform.position.x > 3.5)
                {
                    movt.ImmediateMoveLeft(3.5f);
                }
                movt.JumpRight();
                movt.MoveRight(enemy.transform.position.x);
            }
        }
    }

    internal void Attack()
    {
        GetComponent<Animator>().SetBool("attacking", true);
        aaudio.PlayAttackSFX();
    }
}
