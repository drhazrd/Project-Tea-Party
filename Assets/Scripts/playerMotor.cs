using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class playerMotor : MonoBehaviour {

    public float moveSpeed;
    public float jumpForce;
    float distFromGround;
    CharacterController controller;
    private Vector2 inputVector;
    private Vector3 moveDirection;
    float gravityScale = 0.5f;
    Animator anim;
    Transform cameraPivot;
    float rotateSpeed = 30f;
    GameObject model;
    public float knockBackForce;
    public float knockBackTime;
    private float knockBackCounter;
    private bool canMove;
    bool isMoving;
    private PlayerControls controls;
    PlayerInput pInput;

    private void Awake()
    {
        controls = new PlayerControls();
        pInput = GetComponent<PlayerInput>();
    }

    void Start () {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        cameraPivot = this.gameObject.transform.GetChild(0);
        model = this.gameObject.transform.GetChild(1).gameObject;
	}

	void Update () {
        canMove = GameManager._gameManager.playerCanMove;
        if (knockBackCounter <= 0)
        {
            float yStore = moveDirection.y;
            if (canMove)
            {
                moveDirection = (transform.forward * inputVector.y) + (transform.right * inputVector.x);
                moveDirection = moveDirection.normalized * moveSpeed;
            }
            else { return; }
            moveDirection.y = yStore;
            if (controller.isGrounded)
            {
                moveDirection.y = 0f;


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

        if (inputVector.x != 0 || inputVector.y != 0)
        {
            transform.rotation = Quaternion.Euler(0f, cameraPivot.rotation.eulerAngles.y, 0);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            model.transform.rotation = Quaternion.Slerp(model.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            anim.SetBool("Move", true);
        }
        else
        {
            anim.SetBool("Move", false);
        }

		anim.SetBool ("isGrounded", controller.isGrounded);
		anim.SetFloat ("runSpeed", (Mathf.Abs (inputVector.x))+ (Mathf.Abs(inputVector.y)));

	}
    public void GetMoveInput(CallbackContext cxt)
    {
        inputVector = cxt.ReadValue<Vector2>();
    }
    public void Jump()
    {
        moveDirection.y = jumpForce;
        anim.SetTrigger("Jump");
    }
    public void Knockback(Vector3 direction)
    {
        knockBackCounter = knockBackTime;
        moveDirection = new Vector3 (direction.x,0, direction.z)* knockBackForce;
        //moveDirection.y = knockBackForce;
    }
    public void Warp(Vector3 position, Vector3 rotation)
    {
        controller.enabled = false;
        transform.position = position;
        transform.eulerAngles = rotation;
        controller.enabled = true;
    }
}﻿



