using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class playernew : MonoBehaviour
    {
        Animator animator;
        CharacterController controller;
        Transform playerTrans;

        Vector3 moveDirection = Vector3.zero;
        float gravity = 9.81f;

        [Header("Speeds")]
        public float walkSpeed = 5f;
        public float backSpeed = 5f;
        public float runBoost = 5f;
        public float rotationSpeed = 100f;
        public float jumpSpeed = 3f;
        float currentSpeed;
        bool isWalkingForward = false;
        bool isWalkingBack = false;
        bool isRotating = false;
        bool isJumping = false;
        bool isRunning = false;
        bool isRotatingLeft = false;
        bool isRotatingRight = false;
        void Start()
        {
            animator = GetComponent<Animator>();
            controller = GetComponent<CharacterController>();
            playerTrans = transform;
            currentSpeed = walkSpeed;
        }

        void Update()
        {
            isWalkingForward = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
            isWalkingBack = controller.isGrounded && !Input.GetKey(KeyCode.Space)
                            && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow));
            isRunning = Input.GetKey(KeyCode.LeftShift);
            currentSpeed = isWalkingBack ? -backSpeed
                                        : (isWalkingForward ? (walkSpeed + (isRunning ? runBoost : 0))
                                                    : 0);
            isRotatingLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
            isRotatingRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
            isRotating = controller.isGrounded && !Input.GetKey(KeyCode.Space) && (isRotatingLeft || isRotatingRight);
            isRotatingLeft &= isRotating;
            isRotatingRight &= isRotating;
            if (!isJumping && Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
            }
            HandleAnimation();
            HandleMovement();
            if (isRotating) HandleRotation();


            if (isJumping && controller.isGrounded && moveDirection.y <= 0f)
            {
                isJumping = false;
                animator.ResetTrigger("jump"); // <- important
            }
        }

        void HandleMovement()
        {
            // Jump
            if (isJumping)
            {
                moveDirection.y = jumpSpeed;
                moveDirection += transform.forward * currentSpeed;
                isJumping = false;
            }

            Vector3 horizontalMove = transform.forward * currentSpeed;
            // Apply horizontal movement
            moveDirection.x = horizontalMove.x;
            moveDirection.z = horizontalMove.z;

            // Apply gravity then move the character
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }


        void HandleRotation()
        {
            float turn = isRotatingLeft ? -rotationSpeed : rotationSpeed;
            playerTrans.Rotate(0, turn * Time.deltaTime, 0);
        }

        void clearAnimationTriggers()
        {
            animator.SetTrigger("idle");
            animator.ResetTrigger("walk");
            animator.ResetTrigger("run");
            animator.ResetTrigger("walkback");
            animator.ResetTrigger("left");
            animator.ResetTrigger("right");
            animator.ResetTrigger("jump");
        }
        void HandleAnimation()
        {
            clearAnimationTriggers();

            // jump animation - trigger immediately when jumping starts
            if (isJumping || !controller.isGrounded)
            {
                animator.SetTrigger("jump");
                animator.ResetTrigger("idle");
                return;
            }

            // running animation
            if (isRunning && isWalkingForward)
            {
                animator.SetTrigger("run");
                animator.ResetTrigger("idle");
                return;
            }

            // Walking Forward
            else if (isWalkingForward)
            {
                animator.SetTrigger("walk");
                animator.ResetTrigger("idle");
            }

            // walking backward
            else if (isWalkingBack)
            {
                animator.SetTrigger("walkback");
                animator.ResetTrigger("idle");
            }

            // Left Rotation
            if (isRotatingLeft)
            {
                animator.SetTrigger("left");
                animator.ResetTrigger("idle");
            }
            // Right Rotation
            else if (isRotatingRight)
            {
                animator.SetTrigger("right");
                animator.ResetTrigger("idle");
            }
        }
    }
}