using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Player2 : MonoBehaviour
    {
        Animator animator;
        CharacterController controller;
        public Transform enemyTransform;
        Transform playerTrans;

        Vector3 moveDirection = Vector3.zero;
        float gravity = 9.81f;

        [Header("Speeds")]
        public float walkSpeed = 5f;
        public float backSpeed = 5f;
        public float runBoost = 5f;
        public float rotationSpeed = 100f;
        float currentSpeed;
        bool isWalkingForward = false;
        bool isWalkingBack = false;
        bool isRotating = false;
        bool isRunning = false;
        bool isRotatingLeft = false;
        bool isRotatingRight = false;
        bool isAttacking = false;
        AudioSource audioSource;
        public AudioClip walkSound;
        public AudioClip runSound;
        public AudioClip walkBackSound;
        public AudioClip jumping;

        void Start()
        {
            animator = GetComponent<Animator>();
            controller = GetComponent<CharacterController>();
            playerTrans = transform;
            currentSpeed = walkSpeed;
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            float distanceToEnemy = Vector3.Distance(playerTrans.position, enemyTransform.position);
            isAttacking = distanceToEnemy <= 8f;
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
            HandleAnimation();
            HandleMovement();
            if (isRotating) HandleRotation();
        }
        void PlaySound(AudioClip clip)
        {
            if (audioSource.clip == clip && audioSource.isPlaying)
                return;

            audioSource.clip = clip;
            if (clip == jumping)
            {
                audioSource.loop = false;
                audioSource.Play();
            }
            else
            {
                audioSource.loop = true;
                audioSource.Play();
            }
        }


        void HandleMovement()
        {
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
            animator.ResetTrigger("back");
            animator.ResetTrigger("left");
            animator.ResetTrigger("right");
            animator.ResetTrigger("attack");
        }
        void HandleAnimation()
        {
            clearAnimationTriggers();
            if (isAttacking)
            {
                animator.SetTrigger("attack");
                animator.ResetTrigger("idle");
                PlaySound(jumping); // TODO: Replace with attack sound
                return;
            }

            // running animation
            if (isRunning && isWalkingForward)
            {
                animator.SetTrigger("run");
                animator.ResetTrigger("idle");
                PlaySound(runSound);
                return;
            }

            // Walking Forward
            else if (isWalkingForward)
            {
                animator.SetTrigger("walk");
                animator.ResetTrigger("idle");
                PlaySound(walkSound);
            }

            // walking backward
            else if (isWalkingBack)
            {
                animator.SetTrigger("back");
                animator.ResetTrigger("idle");
                PlaySound(walkBackSound);
            }
            else
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
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