using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float newX;
    float newIX;
    int jumptime = 0;
    float jumpspeed = 0;

    bool movingLeft;
    bool movingRight;
    bool jumpingLeft;
    bool jumpingRight;
    bool jumpingUp;
    bool immediateMoveLeft;
    bool immediateMoveRight;

    Animator animator;
    AudioManager aaudio;

    internal void JumpLeft()
    {
        jumpingLeft = true;
    }

    internal void JumpRight()
    {
        jumpingRight = true;
    }

    internal void MoveLeft(float x)
    {
        newX = x;
        movingLeft = true;
    }

    internal void MoveRight(float x)
    {
        newX = x;
        movingRight = true;
    }

    internal void JumpUp()
    {
        jumpingUp = true;
    }

    internal void ImmediateMoveLeft(float x)
    {
        newIX = x;
        immediateMoveLeft = true;
    }

    internal void ImmediateMoveRight(float x)
    {
        newIX = x;
        immediateMoveRight = true;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        aaudio = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
    }
    private void Update()
    {
        if (immediateMoveLeft)
        {
            if (animator.GetBool("moving") == false)
                animator.SetBool("moving", true);
            if (gameObject.transform.localScale.x > 0f)
                gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x,
                                                              gameObject.transform.localScale.y,
                                                              gameObject.transform.localScale.z);
            transform.Translate(new Vector3(-0.05f, 0, 0));
            if (Mathf.Abs(transform.position.x - newIX) <= 0.05f)
            {
                animator.SetBool("moving", false);
                immediateMoveLeft = false;
            }
        }
        else if (immediateMoveRight)
        {
            if (animator.GetBool("moving") == false)
                animator.SetBool("moving", true);
            if (gameObject.transform.localScale.x < 0f)
                gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x,
                                                              gameObject.transform.localScale.y,
                                                              gameObject.transform.localScale.z);
            transform.Translate(new Vector3(0.05f, 0, 0));
            if (Mathf.Abs(transform.position.x - newIX) <= 0.05f)
            {
                animator.SetBool("moving", false);
                immediateMoveRight = false;
            }
        }
        else if (jumpingUp)
        {
            if (jumptime == 0)
            {
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
                jumptime = 60;
                jumpspeed = 1f;
                GetComponent<Rigidbody2D>().simulated = false;
                animator.SetBool("jumping", true);
                aaudio.PlayJumpSFX();
            }
            else
            {
                jumptime--;
                gameObject.transform.Translate(0, 0.135f * jumpspeed, 0);
                jumpspeed = jumpspeed * 0.95f;

                if (jumptime == 30)
                {
                    gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
                    GetComponent<Rigidbody2D>().simulated = true;
                }
                if (jumptime == 1)
                {
                    jumpingUp = false;
                    jumptime = 0;
                    animator.SetBool("jumping", false );
                }
            }
        }
        else if (jumpingLeft)
        {
            if (gameObject.transform.localScale.x > 0f)
                gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x,
                                                              gameObject.transform.localScale.y,
                                                              gameObject.transform.localScale.z);
            if (jumptime == 0)
            {
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
                jumptime = 60;
                jumpspeed = 1f;
                GetComponent<Rigidbody2D>().simulated = false;
                animator.SetBool("jumping", true);
                aaudio.PlayJumpSFX();
            }
            else
            {
                jumptime--;
                gameObject.transform.Translate(-0.05f * (1f - jumpspeed), 0.12f * jumpspeed, 0);
                jumpspeed = jumpspeed * 0.95f;

                if (jumptime == 20)
                {
                    gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
                    GetComponent<Rigidbody2D>().simulated = true;
                    jumptime = 0;
                    jumpingLeft = false;
                    animator.SetBool("jumping", false);
                }
            }
        }
        else if (jumpingRight)
        {
            if (gameObject.transform.localScale.x < 0f)
                gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x,
                                                              gameObject.transform.localScale.y,
                                                              gameObject.transform.localScale.z);
            if (jumptime == 0)
            {
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
                jumptime = 60;
                jumpspeed = 1f;
                GetComponent<Rigidbody2D>().simulated = false;
                animator.SetBool("jumping", true);
                aaudio.PlayJumpSFX();
            }
            else
            {
                jumptime--;
                gameObject.transform.Translate(0.05f*(1f - jumpspeed), 0.12f * jumpspeed, 0);
                jumpspeed = jumpspeed * 0.95f;

                if (jumptime == 20)
                {
                    jumptime = 0;
                    jumpingRight = false;
                    animator.SetBool("jumping", false);
                    gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
                    GetComponent<Rigidbody2D>().simulated = true;
                }
            }
        }
        else if (movingLeft)
        {
            if (animator.GetBool("moving") == false)
                animator.SetBool("moving", true);
            if (gameObject.transform.localScale.x > 0f)
                gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, 
                                                              gameObject.transform.localScale.y, 
                                                              gameObject.transform.localScale.z);
            transform.Translate(new Vector3(-0.05f, 0, 0));
            if (Mathf.Abs(transform.position.x - newX) < 0.5f)
            {
                animator.SetBool("moving", false);
                movingLeft = false;
            }
        }
        else if (movingRight)
        {
            if (animator.GetBool("moving") == false)
                animator.SetBool("moving", true);
            if (gameObject.transform.localScale.x < 0f)
                gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x,
                                                              gameObject.transform.localScale.y,
                                                              gameObject.transform.localScale.z);
            transform.Translate(new Vector3(0.05f, 0, 0));
            if (Mathf.Abs(transform.position.x - newX) < 0.5f)
            {
                animator.SetBool("moving", false);
                movingRight = false;
            }
        }
    }
}
enum movement
{
    nil,
    up,
    down,
    left,
    right
}
