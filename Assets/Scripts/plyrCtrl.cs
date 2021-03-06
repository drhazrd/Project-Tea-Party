using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plyrCtrl : MonoBehaviour {

    public float moveSpeed;
    public float distFromGround;
    public float jumpForce;
    public CharacterController controller;
    //public plyrCmbtCtrl combatControl;
    //public PlayerAttack fightControl;
    private Vector3 moveDirection;
    public float gravityScale;

    public Animator anim;
    //public CharacterAnimator playeranim;
    public Transform pivey;
    public float rotateSpeed;
    public GameObject model;
    public bool player1, player2, player3, player4;
    public SkinnedMeshRenderer playerSkin;
    public Material[] playerSkinMat;
    public Material[] playerCoronaMat;
    public bool useController;
    public SpriteRenderer playerCorona;
    public float knockBackForce;
    public float knockBackTime;
    private float knockBackCounter;
    public int skinSwitch = 1;
    //public float colorA, colorB, colorC;


    // Use this for initialization
    void Start () {
        //Access the Player RigidBody component.
        //combatControl = GetComponent<plyrCmbtCtrl>();
        //fightControl = GetComponent<PlayerAttack>();
        anim = GetComponentInChildren<Animator>();
        playerSkin = GetComponentInChildren<SkinnedMeshRenderer>();
        //Create a reference for CharacterController.
        controller = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update () {


        switch (skinSwitch)
        {
            case 1:
                playerSkin.material = playerSkinMat[0];
                playerCorona.color = Color.red;  
                break;
            case 2:
                playerSkin.material = playerSkinMat[1];
                playerCorona.color = Color.blue;
                break;
            case 3:
                playerSkin.material = playerSkinMat[2];
                playerCorona.color = new Color(.37f,.76f,.74f);
                break;
            case 4:
                playerSkin.material = playerSkinMat[3];
                playerCorona.color = Color.green;
                break;
            default:
                break;
        }

        if (player1)
        {
            skinSwitch = 1;
        }
        else if (player2)
        {
            skinSwitch = 2;
        }
        else if (player3)
        {
            skinSwitch = 3;
        }
        else if (player4)
        {
            skinSwitch = 4;
        }
        
        if (knockBackCounter <= 0)
        {
            float yStore = moveDirection.y;
            //if (fightControl.canMove == true)
            //{
                moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
                moveDirection = moveDirection.normalized * moveSpeed;
            //}
            //else { return; }
            moveDirection.y = yStore;
            if (controller.isGrounded)
            {
                moveDirection.y = 0f;
                //Improved jump script.
                if (Input.GetButtonDown("Jump"))
                {
                    Jump();
                }

            }
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
        }

		//Apply gravity.
		moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);

		//Apply vector.
		controller.Move(moveDirection * Time.deltaTime);

        distFromGround = moveDirection.y;
        anim.SetFloat("distanceFromGround", distFromGround);


        //move the player in different directions based on the camera

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivey.rotation.eulerAngles.y, 0);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            model.transform.rotation = Quaternion.Slerp(model.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            anim.SetBool("Move", true);
        }
        else if (Input.GetAxis("Horizontal") == 0f || Input.GetAxis("Vertical") == 0f)
        {
            anim.SetBool("Move", false);
        }

		anim.SetBool ("isGrounded", controller.isGrounded);
		anim.SetFloat ("runSpeed", (Mathf.Abs (Input.GetAxis ("Vertical"))+ Mathf.Abs(Input.GetAxis("Horizontal"))));

	}
    public void AnimateWalk()
    {
        //if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            //playeranim.Walk(true);
        //else
            //playeranim.Walk(false);

    }
    public void Jump()
    {
        moveDirection.y = jumpForce;
        anim.SetTrigger("Jump");
    }
    public void Knockback(Vector3 direction)
    {
        knockBackCounter = knockBackTime;
        moveDirection = direction * knockBackForce;
        moveDirection.y = knockBackForce;
    }

}﻿



