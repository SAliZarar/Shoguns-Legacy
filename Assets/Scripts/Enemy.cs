using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    PlaySceneController controller;
    public bool clickable;
    float attackTimer = 0;
    int deathframes = 40;

    Animator animator;
    public void Die()
    {
        GetComponent<Animator>().SetBool("dying", true);
        GameObject obj = GameObject.FindGameObjectWithTag("Controller");
        controller = obj.GetComponent<PlaySceneController>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (transform.position.y < 0)
        {
            controller.DestroyGameObject(gameObject);
        }
        else if (GetComponent<Animator>().GetBool("dying"))
        {
            if (deathframes == 0)
            {
                controller.DestroyGameObject(gameObject);
            }
            else
            {
                deathframes--;
            }
        }
        if (animator.GetBool("Attack") == true && attackTimer < 0.5)
        {
            attackTimer += Time.deltaTime;
        }
        else if (animator.GetBool("Attack") == true)
        {
            animator.SetBool("Attack", false);
            attackTimer = 0;
        }
        
        try
        {
            if (GameObject.FindWithTag("Player").transform.position.x < gameObject.transform.position.x && gameObject.transform.localScale.x > 0)
            {
                gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
            else if (GameObject.FindWithTag("Player").transform.position.x > gameObject.transform.position.x && gameObject.transform.localScale.x < 0)
            {
                gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
        }
        catch
        {
            //player is dead
        }
    }

    public void Clicked()
    {
        GameObject.FindWithTag("Player").GetComponent<Player>().MoveToEnemy(gameObject);
        transform.parent.GetComponent<PlaySceneController>().CreateDrawingPanel();
    }

    public void Attack()
    {
        animator.SetBool("Attack", true);
    }
}
