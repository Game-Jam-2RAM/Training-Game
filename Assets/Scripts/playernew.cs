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
        public float jumpSpeed = 8f;
        float currentSpeed;
        
        float jumpBufferTime = 0.2f;
        float jumpBufferCounter = 0f;

        bool isWalking = false;
        bool isWalkingBack = false;
        bool isRotating = false;
        bool isJumping = false;

        void Start()
        {
            animator = GetComponent<Animator>();
            controller = GetComponent<CharacterController>();
            playerTrans = transform;
            currentSpeed = walkSpeed;
        }

        void Update()
        {
            HandleRotation();
            HandleAnimation();
            if ((Input.GetKeyDown(KeyCode.Space) && isWalking) || (Input.GetKeyDown(KeyCode.Space) && !isWalking && !isWalkingBack && !isRotating))
            {
                jumpBufferCounter = jumpBufferTime; // start buffer timer
            }
            else
            {
                jumpBufferCounter -= Time.deltaTime;
            }
            if (isJumping && controller.isGrounded && moveDirection.y <= 0f)
            {
                isJumping = false;
            }


        }

        void FixedUpdate()
        {
            HandleMovement();
        }


        void HandleMovement()
        {
            // Reset x and z movement
            Vector3 horizontalMove = Vector3.zero;

            // Allow forward and backward movement only when grounded
            if (controller.isGrounded)
            {
                // Grounded movement
                if (Input.GetKey(KeyCode.W))
                    horizontalMove = transform.forward * currentSpeed;
                else if (Input.GetKey(KeyCode.S))
                    horizontalMove = -transform.forward * backSpeed;

                // Jump
                if (jumpBufferCounter > 0f && controller.isGrounded && !isJumping)
                {
                    isJumping = true;
                    StartCoroutine(DelayedJump(0.5f)); // Adjust delay (e.g., 0.3 seconds)
                }


            }
            else
            {
                // Retain forward momentum while airborne
                if (Input.GetKey(KeyCode.W))
                    horizontalMove = transform.forward * currentSpeed;
            }

            // Apply horizontal movement
            moveDirection.x = horizontalMove.x;
            moveDirection.z = horizontalMove.z;

            // Apply gravity
            moveDirection.y -= gravity * Time.deltaTime;

            // Move the character
            controller.Move(moveDirection * Time.deltaTime);
        }
        IEnumerator DelayedJump(float delay)
        {
            yield return new WaitForSeconds(delay);

            moveDirection.y = jumpSpeed;

            if (Input.GetKey(KeyCode.W))
            {
                moveDirection += transform.forward * currentSpeed;
            }

            jumpBufferCounter = 0f;
        }




        void HandleRotation()
        {
            float turn = 0f;
            if (Input.GetKey(KeyCode.A) && controller.isGrounded)
                turn = -rotationSpeed;
            if (Input.GetKey(KeyCode.D) && controller.isGrounded)
                turn = rotationSpeed;

            playerTrans.Rotate(0, turn * Time.deltaTime, 0);
            isRotating = (turn != 0);
        }

        void HandleAnimation()
        {
            if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded && !isWalkingBack && !isRotating)
            {
                animator.SetTrigger("jump");
                animator.ResetTrigger("idle");
                animator.ResetTrigger("walk");
                animator.ResetTrigger("run");
                animator.ResetTrigger("walkback");
                animator.ResetTrigger("left");
                animator.ResetTrigger("right");

            }
            


            // Walking Forward
            if (Input.GetKeyDown(KeyCode.W) )
            {
                animator.SetTrigger("walk");
                animator.ResetTrigger("idle");
                animator.ResetTrigger("left");
                animator.ResetTrigger("right");
                isWalking = true;
                currentSpeed = walkSpeed;
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                animator.ResetTrigger("walk");
                isWalking = false;

                // Only go to idle if not rotating
                if (!isRotating)
                {
                    animator.SetTrigger("idle");
                }
            }

            // Walking Backward
            if (Input.GetKeyDown(KeyCode.S) && controller.isGrounded)
            {
                animator.SetTrigger("walkback");
                animator.ResetTrigger("idle");
                animator.ResetTrigger("left");
                animator.ResetTrigger("right");
                isWalkingBack = true;
            }
            if (Input.GetKeyUp(KeyCode.S) && controller.isGrounded)
            {
                animator.ResetTrigger("walkback");
                isWalkingBack = false;

                if (!isRotating)
                {
                    animator.SetTrigger("idle");
                }
            }

            // Rotation Animations
            if (Input.GetKey(KeyCode.A) && !isWalking && !isWalkingBack && controller.isGrounded)
            {
                animator.SetTrigger("left");
                animator.ResetTrigger("idle");
                animator.ResetTrigger("right");
            }
            if (Input.GetKeyUp(KeyCode.A) && !isWalking && !isWalkingBack && controller.isGrounded)
            {
                animator.ResetTrigger("left");
                animator.SetTrigger("idle");
            }

            if (Input.GetKey(KeyCode.D) && !isWalking && !isWalkingBack && controller.isGrounded)
            {
                animator.SetTrigger("right");
                animator.ResetTrigger("idle");
                animator.ResetTrigger("left");
            }
            if (Input.GetKeyUp(KeyCode.D) && !isWalking && !isWalkingBack && controller.isGrounded)
            {
                animator.ResetTrigger("right");
                animator.SetTrigger("idle");
            }

            // Handle rotation while walking stops
            if (isRotating && !isWalking && !isWalkingBack && controller.isGrounded)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    animator.SetTrigger("left");
                    animator.ResetTrigger("idle");
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    animator.SetTrigger("right");
                    animator.ResetTrigger("idle");
                }
            }

            // Running (Shift)
            if (isWalking && Input.GetKeyDown(KeyCode.LeftShift) && controller.isGrounded)
            {
                currentSpeed = walkSpeed + runBoost;
                animator.SetTrigger("run");
                animator.ResetTrigger("walk");
            }
            if ((isWalking && Input.GetKeyUp(KeyCode.LeftShift) && controller.isGrounded) || (!Input.GetKey(KeyCode.LeftShift) && isWalking && controller.isGrounded))
            {
                currentSpeed = walkSpeed;
                animator.ResetTrigger("run");
                animator.SetTrigger("walk");
            }
        }
    }
}