using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class Player : MonoBehaviour
    {
        Animator playerAnim;
        public Rigidbody playerRigid;
        float w_speed, wb_speed, olw_speed, rn_speed, ro_speed;
        bool walking;
        bool walkingBack;
        Transform playerTrans;

        void Start()
        {
            playerAnim = GetComponent<Animator>();
            playerTrans = base.GetComponent<Transform>();
            w_speed = 160f;
            wb_speed = 160f;
            olw_speed = 160;
            rn_speed = 160;
            ro_speed = 110;

        }
        void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.W))
            {
                playerRigid.velocity = transform.forward * w_speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                playerRigid.velocity = -transform.forward * wb_speed * Time.deltaTime;
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                playerAnim.SetTrigger("walk");
                playerAnim.ResetTrigger("idle");
                walking = true;
                //steps1.SetActive(true);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                playerAnim.ResetTrigger("walk");
                playerAnim.SetTrigger("idle");
                walking = false;
                //steps1.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                playerAnim.SetTrigger("walkback");
                playerAnim.ResetTrigger("idle");
                walkingBack = true;
                //steps1.SetActive(true);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                playerAnim.ResetTrigger("walkback");
                playerAnim.SetTrigger("idle");
                walkingBack = false;
                //steps1.SetActive(false);
            }


            if (Input.GetKey(KeyCode.A))
            {
                playerTrans.Rotate(0, -ro_speed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                playerTrans.Rotate(0, ro_speed * Time.deltaTime, 0);
            }

            // Handle animations for turning
            if (Input.GetKeyDown(KeyCode.A) && !walking && !walkingBack)
            {
                playerAnim.SetTrigger("left");
                playerAnim.ResetTrigger("idle");
            }
            if (Input.GetKeyUp(KeyCode.A) && !walking && !walkingBack)
            {
                playerAnim.ResetTrigger("left");
                playerAnim.SetTrigger("idle");
            }

            if (Input.GetKeyDown(KeyCode.D) && !walking && !walkingBack)
            {
                playerAnim.SetTrigger("right");
                playerAnim.ResetTrigger("idle");
            }
            if (Input.GetKeyUp(KeyCode.D) && !walking && !walkingBack)
            {
                playerAnim.ResetTrigger("right");
                playerAnim.SetTrigger("idle");
            }





            if (walking == true)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    //steps1.SetActive(false); 
                    //steps2.SetActive(true); 
                    w_speed = w_speed + rn_speed;
                    playerAnim.SetTrigger("run");
                    playerAnim.ResetTrigger("walk");
                }
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    //steps1.SetActive(true); 
                    //steps2.SetActive(false); 
                    w_speed = olw_speed;
                    playerAnim.ResetTrigger("run");
                    playerAnim.SetTrigger("walk");
                }
            }
        }


    }
}