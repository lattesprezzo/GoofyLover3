﻿using System.Collections;
using TMPro.EditorUtilities;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;


//[RequireComponent(typeof(InputRegistering))] // Good practise
//[RequireComponent (typeof(Rigidbody))]

public class SimplePlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;
    private Vector2 movement;
    private Vector3 playervelocity;

    //..... Jumping .....//

    bool isOnAir; // Saattaa olla mys OnWater joten tehdn oma ilmassaoloehto
    bool isFalling;
    bool isJumping;
    bool isOnGround;
    bool isJumpPressed;
    [SerializeField] float initialJumpVelocity;
    [SerializeField] float maxJumpHeight;
    [SerializeField] float maxJumpTime;
    [SerializeField] float timeToReachTop;
    [SerializeField] float jumpForce;
    [SerializeField] float fallingForce;

    // -------- Gravity variables --------- //
    [SerializeField] float gravity;
    readonly float groundedGravity = -0.05f;

    Material playerSkin;

    private CharacterController _controller;
    private InputAction input; // input Class generoidaan Inspectorissa

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        timeToReachTop = maxJumpTime / 2;
        gravity = (-4 * maxJumpTime) / timeToReachTop;
        initialJumpVelocity = (2 * maxJumpHeight) / timeToReachTop;
    }
    void Start()
    {

        StartCoroutine(GroundChecker());

    }



    private void GravityControl()
    {
        if (isOnGround)
        {
            isFalling = false;
            if (playervelocity.y >= 0) playervelocity.y += groundedGravity;
        

        }
        else
        {
            playervelocity.y += gravity * Time.deltaTime;
       

        }
    }
    // ------------- Events register -------------//
    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            movement = ctx.ReadValue<Vector2>();
        }
        if (ctx.performed)
        {

            Debug.Log("Moving " + movement);
        }
        if (ctx.canceled)
        {
            movement = Vector2.zero;
        }
    }
    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            isJumpPressed = ctx.ReadValueAsButton(); // Voisi olla my�s vain isJumpPressed = true;
        }
        if (ctx.canceled)
        {
            isJumpPressed = ctx.ReadValueAsButton(); // Voisi olla my�s vain isJumpPressed = false;
        }
    }

    // ------------ Move ----------- //

    void Move()
    {
        Vector3 move = new(movement.x, playervelocity.y, movement.y);
        _controller.Move(speed * Time.deltaTime * move);
    }

    // ------------ JUMP ------------ //

    void JumpControl()
    {
        if (isJumpPressed)
        {
            isJumping = true;
            playervelocity.y = initialJumpVelocity;

            // playervelocity.y += jumpForce * Time.deltaTime; ;
            // Debug.Log("Jumping pressed. Playervelocity.y = " + playervelocity.y);
        }
        else if (!isJumpPressed || !isOnGround)
        {
            isJumping = false;
            //playervelocity.y -= fallingForce;
        }
    }
    IEnumerator GroundChecker()
    {
        while (true)
        {
            isOnGround = _controller.isGrounded;
           // Debug.Log("Coroutine says: isOnGround ===== " + isOnGround);
            yield return new WaitForSeconds(0.1f); // Ei yrit� checkaa joka framella 
        }
    }
    void Update()
    {
        Move();
        GravityControl();
        JumpControl();
    }
}