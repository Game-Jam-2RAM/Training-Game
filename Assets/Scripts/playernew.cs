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
        float currentSpeed;

        bool isWalking = false;
        bool isWalkingBack = false;
        bool isRotating = false;

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
        }

        void FixedUpdate()
        {
            HandleMovement();
        }

        void HandleMovement()
        {
            moveDirection = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                moveDirection += transform.forward * currentSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveDirection -= transform.forward * backSpeed * Time.deltaTime;
            }

            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection);
        }

        void HandleRotation()
        {
            float turn = 0f;
            if (Input.GetKey(KeyCode.A))
                turn = -rotationSpeed;
            if (Input.GetKey(KeyCode.D))
                turn = rotationSpeed;

            playerTrans.Rotate(0, turn * Time.deltaTime, 0);
            isRotating = (turn != 0);
        }

        void HandleAnimation()
        {
            // Walking Forward
            if (Input.GetKeyDown(KeyCode.W))
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
            if (Input.GetKeyDown(KeyCode.S))
            {
                animator.SetTrigger("walkback");
                animator.ResetTrigger("idle");
                animator.ResetTrigger("left");
                animator.ResetTrigger("right");
                isWalkingBack = true;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                animator.ResetTrigger("walkback");
                isWalkingBack = false;
                
                if (!isRotating)
                {
                    animator.SetTrigger("idle");
                }
            }

            // Rotation Animations
            if (Input.GetKey(KeyCode.A) && !isWalking && !isWalkingBack)
            {
                animator.SetTrigger("left");
                animator.ResetTrigger("idle");
                animator.ResetTrigger("right");
            }
            if (Input.GetKeyUp(KeyCode.A) && !isWalking && !isWalkingBack)
            {
                animator.ResetTrigger("left");
                animator.SetTrigger("idle");
            }

            if (Input.GetKey(KeyCode.D) && !isWalking && !isWalkingBack)
            {
                animator.SetTrigger("right");
                animator.ResetTrigger("idle");
                animator.ResetTrigger("left");
            }
            if (Input.GetKeyUp(KeyCode.D) && !isWalking && !isWalkingBack)
            {
                animator.ResetTrigger("right");
                animator.SetTrigger("idle");
            }

            // Handle rotation while walking stops
            if (isRotating && !isWalking && !isWalkingBack)
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
            if (isWalking && Input.GetKeyDown(KeyCode.LeftShift))
            {
                currentSpeed = walkSpeed + runBoost;
                animator.SetTrigger("run");
                animator.ResetTrigger("walk");
            }
            if ((isWalking && Input.GetKeyUp(KeyCode.LeftShift)) || (!Input.GetKey(KeyCode.LeftShift) && isWalking))
            {
                currentSpeed = walkSpeed;
                animator.ResetTrigger("run");
                animator.SetTrigger("walk");
            }
        }
    }
}