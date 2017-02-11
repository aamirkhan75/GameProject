﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Player : MonoBehaviour
{

    public float health = 100f;
    private float healthStart;
    private float healthRounded;

    public bool isDead = false;

    public float moveSpeed = 5f;
    public float jumpHeight = 15f;
    public float jumpSpeed = 15f;
    public bool isJumping = false;
    private float newJumpY = 0f;

    public float offsetY = 0.5f;
    private float newY = 0f;

    private Vector3 newJumpPosition;

    //public Text healthText;
    public Image healthbar;

    public PlayerRotation playerRotation = new PlayerRotation();
    public enum PlayerRotation { up, down, left, right };

    public GameObject graphicChild;
    private Animator anim;

    private PlayerStats playerStats;

    public float fistDamage = 15f;

    [HideInInspector]
    public Enemy enemy;

    [HideInInspector]
    public Boss boss;

    private Ray jumpRay;
    private RaycastHit hitInfo;

    public Rigidbody[] ragdollParts;

    public GameObject deathCanvas;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        anim = graphicChild.GetComponent<Animator>();

        healthStart = health;

        //RagdollKinematic(true);

    }

    private void Update()
    {

        ChangeGraphicRotation();

        //healthRounded = Mathf.Round(health);
        //healthText.text = ("Health: " + healthRounded);

        healthbar.fillAmount = health / healthStart;
        //healthbar.fillAmount = Mathf.Lerp(healthStart / healthStart, health / healthStart, Time.deltaTime * 50f);

        // Jump Raycast
        jumpRay = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(jumpRay, out hitInfo))
        {
            newY = hitInfo.transform.position.y + offsetY;
            //Debug.Log(hitInfo.collider.name);
        }

        //Debug.DrawRay(jumpRay.origin, jumpRay.direction * 100f, Color.green);

        if (isDead == false)
        {
            if (Input.GetKey(KeyCode.W)) // If Holding 'W'
            {
                MovePlayer("forward");
            }
            else if (Input.GetKey(KeyCode.S)) // If Holding 'S'
            {
                MovePlayer("back");
            }
            else if (Input.GetKey(KeyCode.A)) // If Holding 'A'
            {
                MovePlayer("left");
            }
            else if (Input.GetKey(KeyCode.D)) // If Holding 'D'
            {
                MovePlayer("right");
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                newJumpY = transform.position.y + jumpHeight;
                isJumping = true;
                //Debug.Log("Jump Functionality Here...");
            }
            else
            {
                anim.SetBool("isWalking", false);
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                // Set Attack
                anim.SetTrigger("punch");

                if (enemy != null)
                {
                    enemy.TakeDamage(fistDamage);
                }

                if (boss != null)
                {
                    boss.TakeDamage(fistDamage);
                }
            }
        }
    }

    void MovePlayer(string direction)
    {
        Vector3 dirvector = new Vector3(0, 0, 0);
        if (direction == "forward")
        {
            dirvector = Vector3.forward;
            playerRotation = PlayerRotation.up;
        }
        else if (direction == "back")
        {
            dirvector = Vector3.back;
            playerRotation = PlayerRotation.down;
        }
        else if (direction == "left")
        {
            dirvector = Vector3.left;
            playerRotation = PlayerRotation.left;
        }
        else if (direction == "right")
        {
            dirvector = Vector3.right;
            playerRotation = PlayerRotation.right;
        }

        int layerMask = 1 << 8;
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, dirvector, out hit, 1, layerMask))
        {
            transform.Translate(dirvector * Time.deltaTime * moveSpeed);
        }
        anim.SetBool("isWalking", true);

    }

    private void FixedUpdate()
    {
        if (isJumping == true)
        {
            newJumpPosition = new Vector3(transform.position.x, newJumpY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newJumpPosition, Time.deltaTime * jumpSpeed);
        }
        else if (isJumping == false)
        {
            Vector3 newPos = new Vector3(transform.position.x, newY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * jumpSpeed);
        }

        double newDouble = newJumpPosition.y;
        double roundedNewY = Math.Round(newDouble, 0);

        if (Math.Round(transform.position.y, 0) >= roundedNewY)
        {
            isJumping = false;
        }
    }

    private void ChangeGraphicRotation()
    {
        if (playerRotation == PlayerRotation.up)
        {
            graphicChild.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (playerRotation == PlayerRotation.down)
        {
            graphicChild.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (playerRotation == PlayerRotation.left)
        {
            graphicChild.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (playerRotation == PlayerRotation.right)
        {
            graphicChild.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        anim.SetTrigger("getHit");

        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        RagdollKinematic(false);
        deathCanvas.SetActive(true);
    }

    private void RagdollKinematic(bool enabled)
    {
        foreach (Rigidbody part in ragdollParts)
        {
            anim.enabled = false;
            part.isKinematic = enabled;
        }
    }

}
